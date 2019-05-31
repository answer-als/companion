using System;
using System.IO;
using Xamarin.Forms;
using Plugin.AudioRecorder;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Net.Http;
using Plugin.LocalNotifications;

namespace Companion
{
    public partial class SpeechImageView : ContentView
    {
        public AudioRecorderService recorder;
        public AudioPlayer player;
        bool homeClicked = false;
        bool alreadyTried = false;
        ushort seconds = 0;

        HttpClient _client;
        string hash;
        byte[] image;

        public SpeechImageView()
        {
            InitializeComponent();
            PlayButton.Source = ImageSource.FromResource("Companion.Icons.play_icon.png");
            _client = new HttpClient();

            recorder = new AudioRecorderService
            {
                StopRecordingOnSilence = true, // default will stop recording after 2 seconds
                StopRecordingAfterTimeout = true,  // stop recording after a max timeout (defined below)
                TotalAudioTimeout = TimeSpan.FromSeconds(120), // audio will stop recording after 120 seconds
                AudioSilenceTimeout = TimeSpan.FromSeconds(5), // audio will stop recording after 5 seconds of silence
                SilenceThreshold = 0.15F // value between 0 and 1 that determines what makes a silent audio input
            };
            App.GlobalRecorder = recorder;

            player = new AudioPlayer();
            player.FinishedPlaying += Player_FinishedPlaying;
            recorder.AudioInputReceived += Recorder_AudioInputReceived;
            App.GlobalPlayer = player;

            App.IsRecording = false;
            App.IsPlaying = false;
            TaskImage.Source = ImageSource.FromStream(() => new MemoryStream(App.CurrentImage));
            DoneButton.IsVisible = false;
            RetryButton.IsVisible = false;
            PlayButton.IsVisible = false;

            //LayoutChanged += (sender, e) => { Device.BeginInvokeOnMainThread(() => { TaskImage.Source = ImageSource.FromStream(() => new MemoryStream(App.CurrentImage)); }); };
        }

        public async void EndRecording(object sender, EventArgs e)
        {
            homeClicked = sender.ToString().Contains("ImageButton");
            await recorder.StopRecording();
        }

        void Player_FinishedPlaying(object sender, EventArgs e)
        {
            DoneButton.IsEnabled = true;
            RecordButton.IsEnabled = true;
            RetryButton.IsEnabled = true;
            status.Text = "Play";
        }

        void Recorder_AudioInputReceived(object sender, string audioFile)
        {
            // If silence is true, it means that a thread from under the hood called this function from a Silence Timeout
            // if silence is false, it means the user + main UI thread triggered the recording to stop
            bool silence = !MainThread.IsMainThread;

            Device.BeginInvokeOnMainThread(() => {
                App.IsRecording = false;
                RecordButton.CornerRadius = 20;
                status.Text = "Record";

                var under2 = seconds < 2;

                if (silence)
                {
                    // Silence timeout stopped the recording and it will be empty!
                    App.CurrentPage = "Alert";
                    Application.Current.MainPage.DisplayAlert("Recording Stopped!", "Your voice could not be heard. Please try again, and speak louder into your device.", "Try Again");
                    App.CurrentPage = "Speech";
                    Timer.Text = "00:00";
                    return;
                }

                if (under2)
                {
                    // Do nothing
                    Timer.Text = "00:00";
                }
                else
                {
                    App.RecordedButNotSaved = true;
                    RecordButton.Opacity = 0.5;
                    RecordButton.IsEnabled = false;
                    Timer.Opacity = 0.8;
                    DoneButton.IsVisible = true;
                    RetryButton.IsVisible = true;

                    status.Text = "Play";
                    status.TextColor = Color.Green;
                    RecordButton.IsVisible = false;
                    PlayButton.IsVisible = true;
                }
                seconds = 0;
            });
        }


        async void DoneButton_Clicked(object sender, EventArgs e)
        {
            SpeechTaskPage rent = (SpeechTaskPage)this.Parent;
            rent.DisableHomeButton();
            AnalyzingAudio();
            await Task.Delay(2000);
            // TODO: Write code to analyze audio (basic algorithm on Trello)
            //          > If audio passes tests/checks
            //              • Conversion to m4a?
            //          > If audio fails tests/checks
            //              - Pop-up display should now show particular fail message instead of "Analyzing Audio"/animation
            //              - Pop-up display should have Retry button, which refreshes the current phrase view

            bool audio_success = true;
            if (audio_success)
            {
                HTTPPutUploading();
                await PutRecordingToServer();
                if (App.SuccessfulPUT)
                {
                    // Reset these global variables so the next task attempt has correct setup
                    App.SuccessfulPUT = false;
                    App.RecordedButNotSaved = false;
                    App.SpeechTaskLastCompleted = DateTime.Now;
                    App.SpeechTaskType = "Breath";
                    App.SpeechTaskDataReceived = false;

                    await HTTPPutSuccess();

                    // Local Notifications For Next Task
                    CrossLocalNotifications.Current.Show("Speech Task Available", "The next Speech Task is ready for you to complete!", 1, DateTime.Now.AddSeconds(15)); //DateTime.Now.AddDays(7));
                    CrossLocalNotifications.Current.Show("Speech Task Available", "The next Speech Task is ready for you to complete!", 2, DateTime.Now.AddSeconds(120)); //DateTime.Now.AddDays(9));
                    CrossLocalNotifications.Current.Show("Speech Task Is Due!", "The next Speech Task is due. Please login and complete it.", 3, DateTime.Now.AddDays(1)); //DateTime.Now.AddDays(11));

                    NavigationPage page = new NavigationPage(new TaskPage());
                    Application.Current.MainPage = page;
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    HTTPPutFail();
                }
            }
            rent.EnableHomeButton();
        }

        void DisplayLoadingScreen()
        {
            TaskImage.IsVisible = false;
            Instructions.IsVisible = false;
            Timer.IsVisible = false;
            PlayButton.IsVisible = false;
            RecordButton.IsVisible = false;
            status.IsVisible = false;
            RetryButton.IsVisible = false;
            DoneButton.IsVisible = false;

            LoadingScreen.IsVisible = true;
        }

        void AnalyzingAudio()
        {
            DisplayLoadingScreen();
            LoadingScreen.Analyzing();
        }

        void HTTPPutUploading()
        {
            DisplayLoadingScreen();
            LoadingScreen.UploadingAudio();
        }

        public void LoadingImage()
        {
            DisplayLoadingScreen();
            LoadingScreen.LoadingImage();
        }

        async Task HTTPPutSuccess()
        {
            Device.BeginInvokeOnMainThread(LoadingScreen.Success);
            await Task.Delay(2000);
        }

        void HTTPPutFail()
        {
            HTTPDonePUT();

            App.CurrentPage = "Alert";
            Application.Current.MainPage.DisplayAlert("Audio Failed to Upload!", "Please check your connection to the internet and try again.", "OK");
            App.CurrentPage = "Speech";
        }

        void HTTPGetFail()
        {
            DisplayLoadingScreen();
            LoadingScreen.FailedToLoad();
        }

        public void HTTPDonePUT()
        {
            LoadingScreen.IsVisible = false;

            TaskImage.IsVisible = true;
            Instructions.IsVisible = true;
            Timer.IsVisible = true;
            PlayButton.IsVisible = true;
            status.IsVisible = true;
            RetryButton.IsVisible = true;
            DoneButton.IsVisible = true;
        }

        public void HTTPDoneGET()
        {
            LoadingScreen.IsVisible = false;

            TaskImage.IsVisible = true;
            Instructions.IsVisible = true;
            Timer.IsVisible = true;
            RecordButton.IsVisible = true;
            status.IsVisible = true;
        }

        public async Task PutRecordingToServer()
        {
            Stream audio = recorder.GetAudioFileStream();
            var uri = new Uri(string.Format(CompanionServer.recording_url + App.UserID + "/" + App.CurrentSentenceHash, string.Empty));

            try
            {
                var content = new StreamContent(audio);
                content.Headers.Remove("Content-Type");
                content.Headers.Add("Content-Type", "application/octet-stream");
                var request = new HttpRequestMessage(HttpMethod.Put, uri)
                {
                    Content = content
                };

                var response = await _client.SendAsync(request);
                Console.WriteLine("HTTP PUT Response: " + response);
                if (response.IsSuccessStatusCode)
                {
                    App.SuccessfulPUT = true;
                }
                else
                {
                    App.SuccessfulPUT = false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

        async public void RetryButton_Clicked(object sender, EventArgs e)
        {
            // Refresh Image Interface
            PlayButton.IsVisible = false;
            RecordButton.IsVisible = true;
            TaskImage.IsVisible = true;
            LoadingScreen.IsVisible = false;

            RecordButton.Opacity = 1;
            RecordButton.IsEnabled = true;
            status.Opacity = 1;
            status.TextColor = Color.Red;
            status.Text = "Record";
            Timer.Opacity = 1;
            Timer.Text = "00:00";
            seconds = 0;
            DoneButton.IsVisible = false;
            RetryButton.IsVisible = false;
            App.RecordedButNotSaved = false;

            // Reload a new image
            await GetImageFromServer();
        }

        public async Task GetImageFromServer()
        {
            LoadingImage();

            var uri = new Uri(string.Format(CompanionServer.image_url + App.UserID, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                Console.WriteLine("HTTP GET Image Response: " + response);

                if (response.IsSuccessStatusCode)
                {
                    App.SuccessfulGET = true;
                    App.SpeechTaskDataReceived = true;

                    var headerValues = response.Headers.ToString().Split('\n');
                    foreach (string val in headerValues)
                    {
                        if (val.Contains("hash"))
                        {
                            hash = val.Split(' ')[1];
                            App.CurrentSentenceHash = hash;
                        }
                    }

                    image = await response.Content.ReadAsByteArrayAsync();
                    App.CurrentImage = image;
                    HTTPDoneGET();
                    TaskImage.Source = ImageSource.FromStream(() => new MemoryStream(image));
                }
                else
                {
                    HTTPGetFail();
                    App.SuccessfulGET = false;
                    App.SpeechTaskDataReceived = false;
                    App.CurrentPage = "Alert";
                    await Application.Current.MainPage.DisplayAlert("Failed to Load New Image!", "Please check your connection to the internet and try again.", "OK");
                    App.CurrentPage = "Speech";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
                HTTPGetFail();
                App.CurrentPage = "Alert";
                await Application.Current.MainPage.DisplayAlert("Failed to Load New Image!", "Please check your connection to the internet and try again.", "OK");
                App.CurrentPage = "Speech";
            }
        }

        void PlayButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (App.IsPlaying)
                {
                    status.Text = "Play";
                    player.Pause();
                    DoneButton.IsEnabled = true;
                    RetryButton.IsEnabled = true;
                }
                else
                {
                    var filePath = recorder.GetAudioFilePath();

                    if (filePath != null)
                    {
                        DoneButton.IsEnabled = false;
                        RetryButton.IsEnabled = false;

                        player.Play(filePath);
                        status.Text = "Pause";
                    }
                }

                App.IsPlaying = !App.IsPlaying;
            }
            catch (Exception exc)
            {
                status.Text = "Failed to play/pause audio!";
                throw exc;
            }
        }

        async void RecordButton_Toggled(object sender, EventArgs e)
        {
            if (App.IsRecording)
            {
                try
                {
                    if ((Convert.ToUInt16(Timer.Text.Split(':')[1]) > 29) || alreadyTried)
                    {
                        EndRecording(sender, e);
                        alreadyTried = false;
                    }
                    else
                    {
                        status.Text = "Try to talk for 30sec! Tap again to STOP";
                        alreadyTried = true;
                    }
                }
                catch (Exception exc)
                {
                    status.Text = "Failed to Stop Recording!";
                    throw exc;
                }
            }
            else
            {
                try
                {
                    App.IsRecording = true;
                    App.RecordedButNotSaved = false;
                    seconds = 0;
                    Device.StartTimer(TimeSpan.FromSeconds(1), TimerElapsed);
                    RecordButton.CornerRadius = 8;
                    await recorder.StartRecording();
                    status.Text = "Stop";
                }
                catch (Exception exc)
                {
                    status.Text = "Failed to Record!";
                    throw exc;
                }
            }
        }

        private bool TimerElapsed()
        {
            if (!App.IsRecording)
            {
                return false;
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                seconds = (ushort)(seconds + 1);
                string secs = seconds.ToString();
                if (seconds < 10 && App.IsRecording)
                {
                    secs = "00:0" + secs;
                }
                else if (seconds < 60 && App.IsRecording)
                {
                    secs = "00:" + secs;
                }
                else if (seconds < 70 && App.IsRecording)
                {
                    secs = "01:0" + secs;
                }
                else if (seconds < 120 && App.IsRecording)
                {
                    secs = "01:" + secs;
                }
                else
                {
                    // Recordings shouldnt last thing long
                    // Total timeout should stop the recording
                }

                Timer.Text = secs;
            });
            return true;
        }
    }
}

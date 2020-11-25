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
    public partial class SpeechSentenceView : ContentView
    {
        public AudioRecorderService recorder;
        public AudioPlayer player;
        bool homeClicked = false;
        ushort seconds = 0;
        int counter = 0;

        // Limit to 1 retry
        int numRetriesRemaining = 1;

        HttpClient _client;
        string sentence, hash;

        public SpeechSentenceView()
        {
            InitializeComponent();

            //// Change to OnPlatform syntax is confusing and docs do not address the Entry element.
            //// Default XAML is set for iOS.
            //// Ref https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/fonts
            if (Device.RuntimePlatform == Device.Android)
            {
                Timer.FontSize = 17;

                VolumeFeedback.ScaleX = 1.1;
                VolumeFeedback.ScaleY = 2;
            }

            PlayButton.Source = ImageSource.FromResource("Companion.Icons.play_icon.png");
            _client = new HttpClient();

            recorder = new AudioRecorderService
            {
                StopRecordingOnSilence = false,
                StopRecordingAfterTimeout = true,  // stop recording after a max timeout (defined below)
                TotalAudioTimeout = TimeSpan.FromSeconds(120), // audio will stop recording after 2 min
                // AudioSilenceTimeout = TimeSpan.FromSeconds(5), // audio will stop recording after 5 seconds of silence
                // SilenceThreshold = 0.15F // value between 0 and 1 that determines what makes a silent audio input
            };
            App.GlobalRecorder = recorder;

            player = new AudioPlayer();
            player.FinishedPlaying += Player_FinishedPlaying;
            recorder.AudioInputReceived += Recorder_AudioInputReceived;
            App.GlobalPlayer = player;

            App.IsRecording = false;
            App.IsPlaying = false;
            DoneButton.IsVisible = false;
            RetryButton.IsVisible = false;
            PlayButton.IsVisible = false;
            VolumeFeedback.IsVisible = false;


            Console.WriteLine("Routing Audio Playback to Speaker - Phrase");
            DependencyService.Get<IRecordingInterface>().RouteAudioToSpeaker();

        }

        public async void EndRecording(object sender, EventArgs e)
        {
            homeClicked = sender.ToString().Contains("ImageButton");
            App.IsRecording = false;
            await recorder.StopRecording();
        }

        void Player_FinishedPlaying(object sender, EventArgs e)
        {
            DoneButton.IsEnabled = true;
            RecordButton.IsEnabled = true;

            if (numRetriesRemaining > 0)
            {
                RetryButton.IsEnabled = true;
            }
            else
            {
                RetryButton.IsEnabled = false;
            }

            status.Text = "Play";
        }

        void Recorder_AudioInputReceived(object sender, string audioFile)
        {
            // If silence is true, it means that a thread from under the hood called this function from a Silence Timeout
            // if silence is false, it means the user + main UI thread triggered the recording to stop
            bool silence = !MainThread.IsMainThread;

            Device.BeginInvokeOnMainThread(() => {
                App.IsRecording = false;
                RecordButton.CornerRadius = 25;
                status.Text = "Record";

                var under2 = seconds < 2;

                // Make sure that this doesnt get called. Remove '&& false' when using TimeoutAfterSilence
                if (silence && false)
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
            });
        }


        async void DoneButton_Clicked(object sender, EventArgs e)
        {
            SpeechTaskPage rent = (SpeechTaskPage)this.Parent;
            rent.DisableHomeButton();

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
                    App.SpeechTaskCompleted = true;
                    App.SpeechTaskLastCompleted = DateTime.Now;
                    App.SpeechTaskType = "Image";
                    App.SpeechTaskDataReceived = false;

                    await HTTPPutSuccess();

                    // Local Notifications For Next Task
                    CrossLocalNotifications.Current.Show("Speech Task Available", "The next Speech Task is ready for you to complete!", 1, DateTime.Now.AddDays(7));
                    CrossLocalNotifications.Current.Show("Speech Task Available", "The next Speech Task is ready for you to complete!", 2, DateTime.Now.AddDays(9));
                    CrossLocalNotifications.Current.Show("Speech Task Is Due!", "The next Speech Task is due. Please login and complete it.", 3, DateTime.Now.AddDays(11));


                    //// This is where the app exits. Modify to cycle through each task prior to exit.
                    // After successfully sending the data, end the session
                    //                    System.Diagnostics.Process.GetCurrentProcess().Kill();

                    App.SpeechTasksRemaining--;

                    if (App.SpeechTasksRemaining <= 0)
                    {
                        //App.SpeechTasksRemaining = 3;
                        await Application.Current.MainPage.DisplayAlert("Tasks Complete", "No more speech tasks for this week. Come back in 7 days.", "OK");

                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                    
//                    await Task.Delay(1000);

                    //// Remove this page and instruction pages. Should end up at speech task page.
                    //// Ref https://stackoverflow.com/questions/24856116/how-to-popasync-more-than-1-page-in-xamarin-forms-navigation
                    for (var counter = 1; counter < 3; counter++)
                    {
//                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                    }

//                    await Navigation.PopAsync();
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
            SentenceView.IsVisible = false;
            Instructions.IsVisible = false;
            Timer.IsVisible = false;
            PlayButton.IsVisible = false;
            RecordButton.IsVisible = false;
            status.IsVisible = false;
            RetryButton.IsVisible = false;
            DoneButton.IsVisible = false;
            VolumeFeedback.IsVisible = false;

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

        async void LoadingSentence()
        {
            DisplayLoadingScreen();
            LoadingScreen.LoadingSentence();
            await Task.Delay(1000);
        }

        async Task HTTPPutSuccess()
        {
            Device.BeginInvokeOnMainThread(LoadingScreen.Success);
            await Task.Delay(3000);
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

            SentenceView.IsVisible = true;
            Instructions.IsVisible = true;
            Timer.IsVisible = true;
            PlayButton.IsVisible = true;
            status.IsVisible = true;
            RetryButton.IsVisible = true;
            DoneButton.IsVisible = true;
            VolumeFeedback.IsVisible = true;
        }

        public void HTTPDoneGET()
        {
            LoadingScreen.IsVisible = false;

            SentenceView.IsVisible = true;
            Instructions.IsVisible = true;
            Timer.IsVisible = true;
            RecordButton.IsVisible = true;
            status.IsVisible = true;
            VolumeFeedback.IsVisible = true;
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
            // Refresh Sentence
            PlayButton.IsVisible = false;
            RecordButton.IsVisible = true;
            SentenceView.IsVisible = true;
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

            numRetriesRemaining--;

            if (numRetriesRemaining > 0)
            {
                RetryButton.IsEnabled = true;
            }
            else
            {
                RetryButton.IsEnabled = false;
            }

            // Reload a new Sentence
            await GetSentenceFromServer();

        }

        public async Task GetSentenceFromServer()
        {
            LoadingSentence();

            var uri = new Uri(string.Format(CompanionServer.sentence_url + App.UserID, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                Console.WriteLine("HTTP GET Sentence Response: " + response);

                if (response.IsSuccessStatusCode)
                {
                    App.SuccessfulGET = true;
                    App.SpeechTaskDataReceived = true;

                    var headerValues = response.Headers.ToString().Split('\n');
                    foreach (string val in headerValues)
                    {
                        if (val.Contains("Hash") || val.Contains("hash"))
                        {
                            hash = val.Split(' ')[1];
                            App.CurrentSentenceHash = hash;
                        }
                    }

                    sentence = await response.Content.ReadAsStringAsync();
                    App.CurrentSentence = sentence;
                    HTTPDoneGET();
                    SentenceView.sent.Text = sentence;
                }
                else
                {
                    HTTPGetFail();
                    App.SuccessfulGET = false;
                    App.SpeechTaskDataReceived = false;
                    App.CurrentPage = "Alert";
                    await Application.Current.MainPage.DisplayAlert("Failed to Load New Sentence!", "Please check your connection to the internet and try again.", "OK");
                    App.CurrentPage = "Speech";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
                HTTPGetFail();
                App.CurrentPage = "Alert";
                await Application.Current.MainPage.DisplayAlert("Failed to Load New Sentence!", "Please check your connection to the internet and try again.", "OK");
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

                    if (numRetriesRemaining > 0)
                    {
                        RetryButton.IsEnabled = true;
                    }
                    else
                    {
                        RetryButton.IsEnabled = false;
                    }
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
                    EndRecording(sender, e);
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
                    // NOTE: The DisplayVolume function and 'counter' variable are dependent on the 50ms cycle
                    Device.StartTimer(TimeSpan.FromMilliseconds(50), DisplayVolume);
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

        private bool DisplayVolume()
        {
            if (!App.IsRecording)
            {
                VolumeFeedback.Progress = 0;
                return false;
            }

            float vol;
            vol = AudioRecorderService.volumeLevel;

            if ((vol > 0.15F) && (vol < 0.85F))
            {
                VolumeFeedback.ProgressColor = Color.Lime;
            }
            else
            {
                VolumeFeedback.ProgressColor = Color.Red;
            }

            VolumeFeedback.ProgressTo(vol, 50, Easing.CubicOut);
            //VolumeFeedback.Progress = vol;

            if (vol > 0.15F)
            {
                if (counter > 50)
                {
                    status.Text = "Background Noise Detected!";
                }
                counter += 1;
            }
            else
            {
                status.Text = "Stop";
                counter = 0;
            }

            return true;
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
                if (App.IsRecording)
                {
                    if (seconds < 10)
                    {
                        secs = "00:0" + secs;
                    }
                    else if (seconds < 60)
                    {
                        secs = "00:" + secs;
                    }
                    else if (seconds < 70)
                    {
                        secs = "01:0" + (seconds - 60);
                    }
                    else if (seconds < 120)
                    {
                        secs = "01:" + (seconds - 60);
                    }
                    else
                    {
                        // Recordings shouldnt last thing long
                        // Total timeout should stop the recording
                        secs = "02:00";
                    }
                    // This is a work-around for a bug with the Timer display
                    if (secs.Equals("1"))
                    {
                        secs = "Error";
                    }
                    Timer.Text = secs;
                }
            });
            return true;
        }
    }
}

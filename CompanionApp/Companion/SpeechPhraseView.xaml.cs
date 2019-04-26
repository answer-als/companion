using System;
using System.IO;
using Xamarin.Forms;
using Plugin.AudioRecorder;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Companion
{
    public partial class SpeechPhraseView : ContentView
    {
        public AudioRecorderService recorder;
        public AudioPlayer player;
        bool playing = false;
        bool homeClicked = false;
        ushort seconds = 0;
        // TODO: Create a SpeechImageView and implement a minRecording of 30sec to describe picture
        //          - See underMin/overMin code below (commented out in Recorder_AudioInputReceived)
        //int minRecording = 30;

        HttpClient _client;

        public SpeechPhraseView()
        {
            InitializeComponent();
            PlayButton.Source = ImageSource.FromResource("Companion.Icons.play_icon.png");
            _client = new HttpClient();

            recorder = new AudioRecorderService
            {
                StopRecordingOnSilence = true, // default will stop recording after 2 seconds
                StopRecordingAfterTimeout = true,  // stop recording after a max timeout (defined below)
                TotalAudioTimeout = TimeSpan.FromSeconds(120), // audio will stop recording after 15 seconds
                AudioSilenceTimeout = TimeSpan.FromSeconds(5), // audio will stop recording after 3 seconds of silence
                SilenceThreshold = 0.15F // value between 0 and 1 that determines what makes a silent audio input
            };
            player = new AudioPlayer();
            player.FinishedPlaying += Player_FinishedPlaying;
            recorder.AudioInputReceived += Recorder_AudioInputReceived;

            App.IsRecording = false;
            DoneButton.IsVisible = false;
            RetryButton.IsVisible = false;
            PlayButton.IsVisible = false;
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
                //var underMin = seconds < minRecording;
                //var overMin = seconds > minRecording;

                if (silence)
                {
                    // Silence timeout stopped the recording and it will be empty!
                    Application.Current.MainPage.DisplayAlert("Recording Stopped!", "Your voice could not be heard. Please try again, and speak louder into your device.", "Try Again");
                    Timer.Text = "00:00";
                    return;
                }


                if (under2)
                {
                    // Do nothing
                    Timer.Text = "00:00";
                }
                //else if (underMin)
                //{
                //    if (!homeClicked)
                //    {
                //        Application.Current.MainPage.DisplayAlert("Recording Issue", "Recording is too short! Each recording has to be at least " + minRecording.ToString() + "s long.", "Try Again");
                //    }
                //    homeClicked = false;
                //    Timer.Text = "00:00";
                //}
                //else if (overMin)
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
            // TOD: Create "Analyzing Audio" pop-up animation display during analysis (at least 2 sec)
            // TOD: Write code to analyze audio (basic algorithm on Trello)
            //          > If audio passes tests/checks
            //              - Remove this pop-up display
            //              • Conversion to m4a and HTTP PUT of m4a audio stream to azure
            //              • Display Alert with confetti/celebration saying "Nice Job! Speech task accomplished!"
            //                  + This celebraion Alert should have button with text "Ok" that leads to TaskPage
            //                      • Update Preferences with date/time completed
            //                      • TaskPage should update date/time completed on Speech task button
            //          > If audio fails tests/checks
            //              - Pop-up display should now show particular fail message instead of "Analyzing Audio"/animation
            //              - Pop-up display should have Retry button, which refreshes the current phrase view

            bool audio_success = true;
            if (audio_success)
            {
                await PutRecordingToServer();
                App.RecordedButNotSaved = false;
                App.SpeechTaskCompleted = true;
                App.SpeechTaskLastCompleted = DateTime.Now;
                // TODO: Change speech tasks for next week
                //if (App.SpeechTaskState.Equals("Phrase"))
                //{
                //    App.SpeechTaskState = "Image";
                //}
                //else if (App.SpeechTaskState.Equals("Image"))
                //{
                //    App.SpeechTaskState = "Breath";
                //} else if (App.SpeechTaskState.Equals("Breath"))
                //{
                //    App.SpeechTaskState = "Phrase";
                //}

                // Return back home if audio is good!
                // TODO: Erase All other pages to avoid lingering?
                NavigationPage page = new NavigationPage(new TaskPage());
                Application.Current.MainPage = page;
                await Navigation.PopToRootAsync();
            }
        }

        public async Task PutRecordingToServer()
        {
            Stream audio = recorder.GetAudioFileStream();
            var uri = new Uri(string.Format(CompanionServer.recording_url, string.Empty));

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
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("HTTP PUT Response: " + response);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

        //async void FinishButton_Clicked(object sender, EventArgs e)
        //{
        //        // After the very first time around, dont show the Speech Task instructions
        //        // If this isnt the first time around, User has specified to keep instructions in Menu, so let it be
        //        if (!App.SpeechTaskCompleted)
        //        {
        //            App.ShowSpeechInstructions = false;
        //        }

        //        App.SpeechTaskState = "Done";
        //        App.RecordedButNotSaved = false;
        //        App.SpeechTaskCompleted = true;
        //        App.SpeechTaskLastCompleted = DateTime.Now;

        //        NavigationPage page = new NavigationPage(new TaskPage());
        //        Application.Current.MainPage = page;
        //        await Navigation.PopToRootAsync();
        //}

        public void RetryButton_Clicked(object sender, EventArgs e)
        {
            PlayButton.IsVisible = false;
            RecordButton.IsVisible = true;

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
            //FinishButton.IsVisible = false;
            App.RecordedButNotSaved = false;
        }


        void PlayButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (playing)
                {
                    status.Text = "Play";
                    player.Pause();
                    //FinishButton.IsEnabled = true;
                    DoneButton.IsEnabled = true;
                    RetryButton.IsEnabled = true;
                } 
                else
                {
                    var filePath = recorder.GetAudioFilePath();

                    if (filePath != null)
                    {
                        //FinishButton.IsEnabled = false;
                        DoneButton.IsEnabled = false;
                        RetryButton.IsEnabled = false;

                        player.Play(filePath);
                        status.Text = "Pause";
                    }
                }

                playing = !playing;
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

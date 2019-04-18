using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Plugin.AudioRecorder;

namespace Companion
{
    public partial class SpeechPhrase1 : ContentView
    {
        AudioRecorderService recorder;
        AudioPlayer player;
        private bool Recording = false;

        public SpeechPhrase1()
        {
            InitializeComponent();
            recorder = new AudioRecorderService
            {
                StopRecordingOnSilence = true, // default will stop recording after 2 seconds
                StopRecordingAfterTimeout = true,  // stop recording after a max timeout (defined below)
                TotalAudioTimeout = TimeSpan.FromSeconds(15), // audio will stop recording after 15 seconds
                AudioSilenceTimeout = TimeSpan.FromSeconds(3), // audio will stop recording after 3 seconds of silence
                SilenceThreshold = 0.15F // value between 0 and 1 that determines what makes a silent audio input
            };

            player = new AudioPlayer();
            player.FinishedPlaying += Player_FinishedPlaying;
            recorder.AudioInputReceived += Recorder_AudioInputReceived;
        }

        void Player_FinishedPlaying(object sender, EventArgs e)
        {
            // Do something with UI
            DoneButton.IsEnabled = true;
            RecordButton.IsEnabled = true;
        }

        void Recorder_AudioInputReceived(object sender, string audioFile)
        {
            // Do something with the audio file...Conversion and HTTP PUT to server?
            status.Text = "Audio Input Received + Recording Stopped!";
            Recording = false;
            RecordButton.CornerRadius = 30;
        }


        void DoneButton_Clicked(object sender, EventArgs e)
        {
            if (!Recording)
            {
                try
                {
                    var filePath = recorder.GetAudioFilePath();

                    if (filePath != null)
                    {
                        DoneButton.IsEnabled = false;
                        RecordButton.IsEnabled = false;

                        player.Play(filePath);
                    }
                }
                catch (Exception exc)
                {
                    status.Text = "Failed to play audio!";
                    throw exc;
                }

            }
        }

        void RetryButton_Clicked(object sender, EventArgs e)
        {
            // TODO: Refresh sppech phrase interface
        }

            async void RecordButton_Toggled(object sender, EventArgs e)
        {
            if (Recording)
            {
                try
                {
                    await recorder.StopRecording();
                    Recording = false;
                    RecordButton.CornerRadius = 20;
                    status.Text = "Record";
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
                    Recording = true;
                    Timer.IsVisible = true;
                    RecordButton.CornerRadius = 8;
                    recorder.StopRecordingOnSilence = true;
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
    }
}

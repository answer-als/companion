using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class UploadingAudioView : ContentView
    {
        public UploadingAudioView()
        {
            InitializeComponent();
        }

        public void Success()
        {
            UploadingLabel.Text = "Uploaded Successfully!";
        }

        public void UploadingAudio()
        {
            UploadingLabel.Text = "Uploading Audio Recording...";
        }

        public void UploadingProfile()
        {
            UploadingLabel.Text = "Uploading Profile...";
        }

        public void Analyzing()
        {
            UploadingLabel.Text = "Analyzing Audio...";
        }

        public void LoadingSentence()
        {
            UploadingLabel.Text = "Loading Sentence...";
        }

        public void LoadingImage()
        {
            UploadingLabel.Text = "Loading Image...";
        }

        public void FailedToLoad()
        {
            UploadingLabel.Text = "Failed To Load Task!";
        }
    }
}

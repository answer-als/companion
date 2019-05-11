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
            UploadingLabel.Text = "Audio Uploaded Successfully!";
        }

        public void Uploading()
        {
            UploadingLabel.Text = "Uploading Audio Recording...";
        }

        public void Analyzing()
        {
            UploadingLabel.Text = "Analyzing Audio...";
        }
    }
}

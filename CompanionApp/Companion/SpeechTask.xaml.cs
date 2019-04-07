using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Companion
{
    public partial class SpeechTask : ContentPage
    {
        private bool Recording = true;

        public SpeechTask()
        {
            InitializeComponent();
        }

        void OnToggleRecordButton(object sender, EventArgs e)
        {
            if (Recording)
            {
                Recording = false;
                RecordButton.CornerRadius = 10;
            }
            else
            {
                Recording = true;
                RecordButton.CornerRadius = 30;
            }
        }
    }
}

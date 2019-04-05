using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class TaskPage : ContentPage
    {
        public TaskPage()
        {
            InitializeComponent();
        }

        void OnSpeechClicked(object sender, EventArgs e)
        {
            // TODO: Navigate to a RecordingPage
        }

        async void OnNextClicked(object sender, EventArgs e)
        {
            // This is just a placeholder for the next task button we add
            await Navigation.PopAsync();
        }
    }
}

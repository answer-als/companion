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
            Navigation.PushAsync(new SpeechTask());
        }

        async void OnNextClicked(object sender, EventArgs e)
        {
            // Placeholder function for debugging
            await Navigation.PopAsync();
        }
    }
}

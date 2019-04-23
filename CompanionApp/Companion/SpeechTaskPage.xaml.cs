using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using Xamarin.Forms;

namespace Companion
{
    public partial class SpeechTaskPage : ContentPage
    {

        public SpeechTaskPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            HomeButton.Source = ImageSource.FromResource("Companion.Icons.home_icon.png");
        }

        async void HomeButton_Clicked(object sender, EventArgs e)
        {
            // If User selects HOME BUTTON during a recording, make sure to stop the recording
            // Display Alert to user asking them "Are you sure you want to end the task?"
            if (App.IsRecording)
            {
                App.IsRecording = false;
                MainPhraseView.EndRecording(sender, e);

                bool result = await DisplayAlert("Recording interrupted!", "Are you sure you want to leave the current task without finishing? Your recording will be discarded!", "Yes", "No");

                if (result)
                {
                    NavigationPage page = new NavigationPage(new TaskPage());
                    Application.Current.MainPage = page;
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    if (!App.RecordedButNotSaved)
                    {
                        MainPhraseView.RetryButton_Clicked(sender, e);
                    }
                }
            }
            else if (App.RecordedButNotSaved)
            {
                bool result = await DisplayAlert("Task Not Finished!", "Are you sure you want to leave the current task without finishing? Your recording will be discarded!", "Yes", "No");

                if (result)
                {
                    NavigationPage page = new NavigationPage(new TaskPage());
                    Application.Current.MainPage = page;
                    await Navigation.PopToRootAsync();
                }
            }
            else if (App.SpeechTaskState.Equals("Phrase1Done") || App.SpeechTaskState.Equals("Phrase2Done"))
            {
                bool result = await DisplayAlert("Task Not Finished!", "Are you sure you want to leave the current task without finishing?", "Yes", "No");

                if (result)
                {
                    NavigationPage page = new NavigationPage(new TaskPage());
                    Application.Current.MainPage = page;
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                MainPhraseView.player.Pause();
                NavigationPage page = new NavigationPage(new TaskPage());
                Application.Current.MainPage = page;
                await Navigation.PopToRootAsync();
            }
        }
    }
}

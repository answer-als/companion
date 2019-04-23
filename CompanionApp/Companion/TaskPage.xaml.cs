using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Companion
{
    public partial class TaskPage : ContentPage
    {
        string last;

        // TODO: Determine when to ask for the HTTPGET for which 3 phrases to use
        public TaskPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MenuButton.Source = ImageSource.FromResource("Companion.Icons.menu_icon.png");
            UserIcon.Source = ImageSource.FromResource("Companion.Icons.user_icon.png");

            // TODO: Figure out where to display this
            if (App.SpeechTaskState.Equals("Phrase1Done") || App.SpeechTaskState.Equals("Phrase2Done"))
            {
                Speech_LastCompletedDate.Text = "In Progress";
            }
            else
            {
                last = App.SpeechTaskLastCompleted.ToString().Split(' ')[0];
                if (last.Contains("2000"))
                {
                    Speech_LastCompletedDate.Text = " ";
                }
                else
                {
                    Speech_LastCompletedDate.Text = "✓ " + last;
                }
            }

            string welcomeMessage = Preferences.Get("UserID", "Error");
            if (welcomeMessage.Equals("Error") || welcomeMessage.Equals("Sign Out"))
            {
                // If this ever pops up, something is wrong!
                welcomeMessage = "ERROR!";
            }
            else
            {
                welcomeMessage = welcomeMessage + ", welcome!";
            }
            Welcome.Text = welcomeMessage;
        }

        async void Speech_Clicked(object sender, EventArgs e)
        {
            // Check user preferences to see if they have chosen to display task intstructions
            // If not, navigate directly to task
            if (App.ShowSpeechInstructions)
            {
                await Navigation.PushAsync(new SpeechInstructionsPage());
            }
            else
            {
                await Navigation.PushAsync(new SpeechTaskPage());
            }
        }

        async void MenuButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuPage());
        }

        async void Next_Clicked(object sender, EventArgs e)
        {
            // Placeholder function for debugging
            await DisplayAlert("Success", "Recorded audio looks good. Would you like to Retry or Proceed", "Retry", "Proceed");
        }

        async void Questionnaire_Clicked(object sender, EventArgs e)
        {
            // Placeholder function for debugging
            await Navigation.PushAsync(new SpeechInstructionsPage());
        }
    }
}

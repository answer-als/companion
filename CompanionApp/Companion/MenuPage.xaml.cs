using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Companion
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BackButton.Source = ImageSource.FromResource("Companion.Icons.back_icon.png");
            UserIcon.Source = ImageSource.FromResource("Companion.Icons.bigger_user_icon.png");
            App.CurrentPage = "Menu";

            SpeechIntroSwitch.IsToggled = App.ShowSpeechInstructions;
            User.Text = App.UserID;
        }

        async void SignOut_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("UserID", "Sign Out");
            NavigationPage page = new NavigationPage(new MainPage());
            Application.Current.MainPage = page;
            await Navigation.PopToRootAsync();
        }

        void SpeechIntro_Toggled(object sender, EventArgs e)
        {
            App.ShowSpeechInstructions = SpeechIntroSwitch.IsToggled;
        }

        async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        void Quit_Clicked(object sender, EventArgs e)
        {
            // Refresh some of the preferences for debugging
            //Preferences.Remove("SpeechLastCompleted");
            //Preferences.Remove("SpeechCompleted");
            //Preferences.Remove("SpeechTaskState");
            //Preferences.Remove("QuestionnaireCompleted");
            //Preferences.Remove("QuestionnaireLastCompleted");

            // Terminate AALS Companion App session
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        void ResetTasks_Clicked(object sender, EventArgs e)
        {
            // Tasks get "locked" for a week in between completion of last task
            // We "unlock" Tasks by changing the completed date to before the range
            App.SpeechTaskLastCompleted = DateTime.Now.AddDays(-9);
        }
    }
}

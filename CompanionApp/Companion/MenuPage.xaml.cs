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

            if (Device.RuntimePlatform == Device.Android)
            {
                MyGrid.Padding = new Thickness(5, 15, 5, 15);
                NotYou.Margin = new Thickness(0, 3, 0, 0);
                SpeechTaskIntro.FontSize = (double)NamedSize.Large;
                Back.FontSize = (double)NamedSize.Large;
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                MyGrid.Padding = new Thickness(5, 35, 5, 20);
                NotYou.Margin = new Thickness(0, 4, 0, 0);
                SpeechTaskIntro.FontSize = (double)NamedSize.Default;
                Back.FontSize = (double)NamedSize.Medium;
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                MyGrid.Padding = new Thickness(5, 35, 5, 20);
                NotYou.Margin = new Thickness(0, 3, 0, 0);
                SpeechTaskIntro.FontSize = (double)NamedSize.Default;
                Back.FontSize = (double)NamedSize.Large;
            }

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

        //void Quit_Clicked(object sender, EventArgs e)
        //{
        //    // Refresh some of the preferences for debugging
        //    //Preferences.Remove("SpeechLastCompleted");
        //    //Preferences.Remove("SpeechCompleted");
        //    //Preferences.Remove("SpeechTaskState");
        //    //Preferences.Remove("QuestionnaireCompleted");
        //    //Preferences.Remove("QuestionnaireLastCompleted");

        //    // Terminate AALS Companion App session
        //    System.Diagnostics.Process.GetCurrentProcess().Kill();
        //}

        //void ResetTasks_Clicked(object sender, EventArgs e)
        //{
        //    // Tasks get "locked" for a week in between completion of last task
        //    // We "unlock" Tasks by changing the completed date to before the range
        //    App.SpeechTaskLastCompleted = DateTime.Now.AddDays(-9);
        //}
    }
}

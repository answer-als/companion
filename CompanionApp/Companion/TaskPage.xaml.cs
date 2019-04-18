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
            NavigationPage.SetHasNavigationBar(this, false);
            MenuButton.Source = ImageSource.FromResource("Companion.menu_icon.png");
            UserIcon.Source = ImageSource.FromResource("Companion.user_icon.png");
            Welcome.Text = App.UserID + ", welcome!";
        }

        void Speech_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SpeechIntro());
        }

        async void Next_Clicked(object sender, EventArgs e)
        {
            // Placeholder function for debugging
            await Navigation.PopAsync();
        }

        async void BackButton_Clicked(object sender, EventArgs e)
        {
            // Placeholder function for debugging
            await Navigation.PopAsync();
        }

        async void MenuButton_Clicked(object sender, EventArgs e)
        {
            // Placeholder function for debugging
            await Navigation.PopAsync();
        }

        async void NotYou_Clicked(object sender, EventArgs e)
        {
            // Placeholder function for debugging
            await Navigation.PopAsync();
        }

        async void Questionnaire_Clicked(object sender, EventArgs e)
        {
            // Placeholder function for debugging
            await Navigation.PopAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Companion
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Logo.Source = ImageSource.FromResource("Companion.aals_logo.png");
        }

        protected override void OnAppearing() 
        {
            base.OnAppearing();

            // Refresh login instructions
            LoginErrorLabel.Text = " ";
        }

        async void NavigateToTasks(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskPage());
        }

        void OnFinishUserID(object sender, EventArgs e)
        {
            // Switch focus to the password Entry item
            if (ValidUserID(userIDEntry.Text))
            {
                LoginErrorLabel.Text = " ";
                Preferences.Set("UserID", userIDEntry.Text);
                passwordEntry.Focus();
            }
        }

        private bool ValidUserID(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                LoginErrorLabel.Text = "Please enter your User ID.";
                return false;
            }

            // TODO: Actually implement checks (proper length, format, etc?)
            // What should a well-formed UserID look like?
            if (userID.Length < 15 || userID.Length > 4)
            {
                return true;
            }

            LoginErrorLabel.Text = "Improper User ID! Please try again.";
            return false;
        }

        void OnLoginClick(object sender, EventArgs e)
        {
        // User ID check
        if (ValidUserID(userIDEntry.Text))
        {
            App.UserID = userIDEntry.Text;

            // Password Verification
            if (passwordEntry.Text.Equals(App.Password))
            {
                LoginErrorLabel.Text = " ";
                NavigateToTasks(sender, e);
            }
            else
            {
                LoginErrorLabel.Text = "Password Incorrect! Please try again.";
            }
        }
        else
        {
            LoginErrorLabel.Text = "Improper User ID! Please try again.";
        }
    }
    }
}
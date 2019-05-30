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
        private int textChanges = 0;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Logo.Source = ImageSource.FromResource("Companion.aals_logo.png");
            App.CurrentPage = "Login";
        }

        protected override void OnAppearing() 
        {
            base.OnAppearing();
            App.LoginVisits += 1;
            // Refresh login instructions
            string previousUser = Preferences.Get("UserID", "Sign Out");
            if (previousUser.Equals("Sign Out") || previousUser.Equals("Error"))
            {
                userIDEntry.Text = "";
                userIDEntry.Placeholder = "Enter participant ID";
                passwordEntry.Text = "";
                passwordEntry.Placeholder = "Enter password";
            }
            else
            {
                userIDEntry.Text = previousUser;
                passwordEntry.Text = App.Password;
            }
        }

        async void NavigateToTasks(object sender, EventArgs e)
        {
            if (App.QuestionnaireCompleted)
            {
                await Navigation.PushAsync(new TaskPage());
            }
            else
            {
                await Navigation.PushAsync(new QuestionnairePage());
            }
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
            if (userID.Equals("Error") || userID.Equals("Sign Out"))
            {
                LoginErrorLabel.Text = "Please enter a valid User ID.";
                return false;
            }
            return true;
        }

        void LoginText_Changed(object sender, EventArgs e)
        {
            if ((textChanges == 1) && (App.LoginVisits == 1))
            {
                passwordEntry.Text = "";
                passwordEntry.Placeholder = "Enter password";
            }

            textChanges += 1;
        }

        void OnLoginClick(object sender, EventArgs e)
        {
            // User ID check
            if (ValidUserID(userIDEntry.Text))
            {
                // Password Verification
                if (passwordEntry.Text.Equals(App.Password))
                {
                    LoginErrorLabel.Text = " ";
                    Preferences.Set("UserID", userIDEntry.Text);
                    NavigateToTasks(sender, e);
                }
                else
                {
                    LoginErrorLabel.Text = "Password Incorrect! Please try again.";
                }
            }
        }
    }
}
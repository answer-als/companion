using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Companion
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing() 
        {
            base.OnAppearing();

            // Refresh login instructions
            RefreshLoginFont();
        }

        void RefreshLoginFont()
        {
            LoginErrorLabel.TextColor = Color.Black;
            LoginErrorLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            LoginErrorLabel.Text = "Login";
        }

        void LoginErrorFontChange()
        {
            LoginErrorLabel.TextColor = Color.Red;
            LoginErrorLabel.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
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
                RefreshLoginFont();
                passwordEntry.Focus();
            }
        }

        private bool ValidUserID(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                LoginErrorFontChange();
                LoginErrorLabel.Text = "Please enter your User ID.";
                return false;
            }

            // TODO: Actually implement checks (proper length, format, etc?)
            if (userID.Equals("AALS"))
            {
                return true;
            }

            LoginErrorFontChange();
            LoginErrorLabel.Text = "User ID incorrect! Please try again.";
            return false;
        }

        void OnSubmit(object sender, EventArgs e)
        {
        // User ID check
        if (ValidUserID(userIDEntry.Text))
        {
            App.UserID = userIDEntry.Text;

            // Password Verification
            if (passwordEntry.Text.Equals(App.Password))
            {
                RefreshLoginFont();
                NavigateToTasks(sender, e);
            }
            else
            {
                LoginErrorFontChange();
                LoginErrorLabel.Text = "Password Incorrect! Please try again.";
            }
        }
        else
        {
            LoginErrorFontChange();
            LoginErrorLabel.Text = "User ID Incorrect! Please try again.";
        }
    }
    }
}
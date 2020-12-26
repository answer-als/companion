using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Companion
{
    public partial class MainPage : ContentPage
    {
        private int textChanges = 0;

        public MainPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                //                LogoLabel.FontSize = (double)NamedSize.Medium;
                SupportUrl.CommandParameter = "https://support.google.com/accessibility/android/answer/6006972?hl=en";
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
//                LogoLabel.FontSize = (double)NamedSize.Large;
                SupportUrl.CommandParameter = "https://support.apple.com/en-us/HT202828";
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
//                LogoLabel.FontSize = (double)NamedSize.Medium;
            }

            BindingContext = this;

            NavigationPage.SetHasNavigationBar(this, false);
            Logo.Source = ImageSource.FromResource("Companion.aals_logo.png");
            App.CurrentPage = "Login";
        }

        public ICommand ClickCommand => new Command<string>((url) =>
        {
            OpenUrl(url);
        });

        async void OpenUrl(string tempUrl)
        {
            string url = "";

            if (Device.RuntimePlatform == Device.Android)
            {
                url = "https://support.google.com/accessibility/android/answer/6006972?hl=en";
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                url = "https://support.apple.com/en-us/HT202828";
            }

            await Launcher.TryOpenAsync(url);
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
//                passwordEntry.Text = "";
//                passwordEntry.Placeholder = "Enter password";
            }
            else
            {
                userIDEntry.Text = previousUser;
//                passwordEntry.Text = App.Password;
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
            /*
            if (App.QuestionnaireLastCompleted == DateTime.MinValue)
            {
                App.QuestionnaireCompleted = false;
                await Navigation.PushAsync(new QuestionnairePage());
            }
            else if (DateTime.Now.CompareTo(App.QuestionnaireLastCompleted.AddMonths(1) ) >= 0)
            {
                // Require FRS task monthly
                DateTime today = DateTime.Now;
                DateTime dueDate = App.QuestionnaireLastCompleted.AddMonths(1);

                // Ref https://docs.microsoft.com/en-us/dotnet/api/system.datetime.compareto?view=net-5.0
                if (today.CompareTo(dueDate) >= 0)
                {
                }
                App.QuestionnaireCompleted = false;
                await Navigation.PushAsync(new QuestionnairePage());
            }
            else if (App.QuestionnaireCompleted)
            {
                await Navigation.PushAsync(new TaskPage());
            }
            */
        }

        void OnFinishUserID(object sender, EventArgs e)
        {
            // Switch focus to the password Entry item
            if (ValidUserID(userIDEntry.Text))
            {
                LoginErrorLabel.Text = " ";
                Preferences.Set("UserID", userIDEntry.Text);
                //passwordEntry.Focus();
            }
        }

        private bool ValidUserID(string userID)
        {
            LoginErrorLabel.TextColor = Color.White;

            if (string.IsNullOrEmpty(userID))
            {
                LoginErrorLabel.Text = "Please enter your User ID.";
                return false;
            }
            else if (userID.Equals("Error") || userID.Equals("Sign Out"))
            {
                LoginErrorLabel.Text = "Please enter a valid User ID.";
                return false;
            }
//            else if (userID.Equals("XOOOOX") == true)
//            {
//                return true;
//            }
            else if (Regex.IsMatch(userID, "^[X][O]{4}[X]") == true)
            {
                return true;
            }
            else if (Regex.IsMatch(userID, "^[A-Z][0-9]{4}[A-Z]") == false)
            {
                LoginErrorLabel.Text = "I don't recognize that User ID. Please try again.";
                return false;
            }

            return true;
        }

        void LoginText_Changed(object sender, EventArgs e)
        {
            if ((textChanges == 1) && (App.LoginVisits == 1))
            {
//                passwordEntry.Text = "";
//                passwordEntry.Placeholder = "Enter password";
            }

            textChanges += 1;
        }

        void OnLoginClick(object sender, EventArgs e)
        {
            // User ID check
            if (ValidUserID(userIDEntry.Text))
            {
                LoginErrorLabel.Text = " ";
                Preferences.Set("UserID", userIDEntry.Text);
                App.LoggedIn = true;
                NavigateToTasks(sender, e);
                /*
                // Password Verification
                if (passwordEntry.Text.Equals(App.Password))
                {
                    LoginErrorLabel.Text = " ";
                    Preferences.Set("UserID", userIDEntry.Text);
                    App.LoggedIn = true;
                    NavigateToTasks(sender, e);
                }
                else
                {
                    LoginErrorLabel.Text = "Password Incorrect! Please try again.";
                }
                */
            }
        }
    }
}
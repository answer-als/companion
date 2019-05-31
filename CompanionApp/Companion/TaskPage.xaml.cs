using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Permissions;

namespace Companion
{
    public partial class TaskPage : ContentPage
    {
        public TaskPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MenuButton.Source = ImageSource.FromResource("Companion.Icons.menu_icon.png");
            UserIcon.Source = ImageSource.FromResource("Companion.Icons.user_icon.png");
            App.CurrentPage = "Home";

            CheckPermissions();

            App.RecordedButNotSaved = false;
            App.IsRecording = false;
            App.SuccessfulGET = false;
            App.SuccessfulPUT = false;

            // Welcome message
            WelcomeUser();
        }

        async void CheckPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Microphone);
                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Microphone);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Plugin.Permissions.Abstractions.Permission.Microphone))
                        status = results[Plugin.Permissions.Abstractions.Permission.Microphone];
                }

                if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    return;
                }

                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
                {
                    await DisplayAlert("Access to Microphone Denied", "Can not continue. Restart and try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Permission Check Exception Thrown! {0}", ex);
            }
        }

        protected override void OnAppearing()
        {
            // Has it been a week or more since last task completion?
            SpeechTaskAvailability();

            // Update Speech Task Last Completed Display
            UpdateCompletedDate_Speech();

            // Update Questionnaire Last Completed Display
            //UpdateCompletedDate_Questionnaire();

            base.OnAppearing();
        }

        async void SpeechTaskAvailability()
        {
            if (App.SpeechTaskCompleted)
            {
                // TODO: remove this interval and || OR statement in IF conditional
                TimeSpan interval = DateTime.Now - App.SpeechTaskLastCompleted;
                int nextTask = App.SpeechTaskLastCompleted.AddDays(7).DayOfYear;
                if (DateTime.Now.DayOfYear == nextTask || (interval.TotalSeconds > 15))
                {
                    SpeechButton.IsEnabled = true;
                    SpeechFrame.HasShadow = true;
                }
                else
                {
                    SpeechButton.IsEnabled = false;
                    SpeechFrame.HasShadow = false;
                    int nextAvailable = nextTask - DateTime.Now.DayOfYear;
                    await Application.Current.MainPage.DisplayAlert("No Tasks Available", "Please come back in " + nextAvailable + " days.", "OK");
                }
            }
        }

        private void WelcomeUser()
        {
            string welcomeMessage = Preferences.Get("UserID", "Error");
            if (welcomeMessage.Equals("Error") || welcomeMessage.Equals("Sign Out"))
            {
                // If this ever pops up, something is wrong!
                welcomeMessage = "Username Error";
            }
            else
            {
                welcomeMessage += ", welcome!";
            }
            Welcome.Text = welcomeMessage;
        }

        private void UpdateCompletedDate_Speech()
        {
            string last = App.SpeechTaskLastCompleted.ToString().Split(' ')[0];
            TimeSpan interval = DateTime.Now - App.SpeechTaskLastCompleted;
            if (last.Contains("0001"))
            {
                // This occurs only on the very first run of the app. Default year on launch is 0001
                Speech_LastCompletedDate.Text = " ";
            }
            else
            {
                // interval.Days < 3 just to ensure that its not the same day of the week from the week prior
                if ((App.SpeechTaskLastCompleted.DayOfWeek == DateTime.Now.DayOfWeek) && (interval.Days < 3))
                {
                    Speech_LastCompletedDate.Text = "✓  Today";
                }
                else if ((App.SpeechTaskLastCompleted.DayOfWeek == DateTime.Now.AddDays(-1).DayOfWeek) && (interval.Days < 3))
                {
                    Speech_LastCompletedDate.Text = "✓  Yesterday";
                }
                else
                {
                    Speech_LastCompletedDate.Text = "✓ " + last;
                }
            }
        }

        async void Speech_Clicked(object sender, EventArgs e)
        {
            // Check user preferences to see if they have chosen to display task intstructions
            // If not, navigate directly to task
            try
            {
                if (App.ShowSpeechInstructions)
                {
                    await Navigation.PushAsync(new SpeechInstructionsPage());
                }
                else
                {
                    await Navigation.PushAsync(new SpeechTaskPage());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }
        }

        async void MenuButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuPage());
        }
    }
}

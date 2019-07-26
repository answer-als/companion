using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Permissions;
using Plugin.LocalNotifications;
using System.Threading.Tasks;
using System.Net.Http;

namespace Companion
{
    public partial class TaskPage : ContentPage
    {
        HttpClient _client;

        public TaskPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MonthlyReminder.Parent = this;

            MenuButton.Source = ImageSource.FromResource("Companion.Icons.menu_icon.png");
            UserIcon.Source = ImageSource.FromResource("Companion.Icons.user_icon.png");
            App.CurrentPage = "Home";

            _client = new HttpClient();
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

            // Monthly Reminder to be shown at the beginning of each new month
            string lastMonth = App.Month.ToString().Split(' ')[0];
            if (lastMonth.Contains("0001") || !(DateTime.Now.Month == App.Month.Month && DateTime.Now.Year == App.Month.Year))
            {
                // This occurs on the first run, and when its a different month since last log in
                Body.IsVisible = false;
                MonthlyReminder.IsVisible = true;
            }

            base.OnAppearing();
        }

        public void LeavePopUp()
        {
            App.Month = DateTime.Now;
            MonthlyReminder.IsVisible = false;
            Body.IsVisible = true;
        }

        async void SpeechTaskAvailability()
        {
            if (App.SpeechTaskCompleted)
            {
                int nextTask = App.SpeechTaskLastCompleted.AddDays(7).DayOfYear;
                if (DateTime.Now.DayOfYear > nextTask)
                {
                    SpeechButton.IsEnabled = true;
                    SpeechFrame.HasShadow = true;
                    SpeechPassButton.IsEnabled = true;
                    SpeechPassFrame.HasShadow = true;
                }
                else
                {
                    SpeechButton.IsEnabled = false;
                    SpeechFrame.HasShadow = false;
                    SpeechPassButton.IsEnabled = false;
                    SpeechPassFrame.HasShadow = false;
                    int nextAvailable = nextTask - DateTime.Now.DayOfYear;
                    await Application.Current.MainPage.DisplayAlert("No Tasks Available Yet", "Please come back in " + nextAvailable + " days.", "OK");
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
                welcomeMessage = "Welcome, " + welcomeMessage + "!";
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

        async void SpeechPass_Clicked(object sender, EventArgs e)
        {
            // Before sending a 0-byte array as an empty PASS signifier, remind users that they
            // can just submit whatever possible even if its not recording the entire task as intended.
            // Something is better than nothing!
            bool pass = await Application.Current.MainPage.DisplayAlert("Are you sure?", "You are about to skip this task until next week. Even if you do not feel capable of completing the entire task, we encourage you to try as much as you can.", "Pass Task", "Go To Task");
            if (pass)
            {
                // Although it was Passed, we consider the Passed Task as Completed
                App.SpeechTaskCompleted = true;
                await PutRecordingToServer();
                if (App.SuccessfulPUT)
                {
                    App.SpeechTaskLastCompleted = DateTime.Now;
                    switch (App.SpeechTaskType)
                    {
                        case "Image":
                            App.SpeechTaskType = "Breath";
                            break;
                        case "Breath":
                            App.SpeechTaskType = "Sentence";
                            break;
                        case "Sentence":
                            App.SpeechTaskType = "Image";
                            break;
                    }

                    // Local Notifications For Next Task
                    CrossLocalNotifications.Current.Show("Speech Task Available", "The next Speech Task is ready for you to complete!", 1, DateTime.Now.AddDays(7));
                    CrossLocalNotifications.Current.Show("Speech Task Available", "The next Speech Task is ready for you to complete!", 2, DateTime.Now.AddDays(9));
                    CrossLocalNotifications.Current.Show("Speech Task Is Due!", "The next Speech Task is due. Please login and complete it.", 3, DateTime.Now.AddDays(11));

                    SpeechButton.IsEnabled = false;
                    SpeechFrame.HasShadow = false;
                    SpeechPassButton.IsEnabled = false;
                    SpeechPassFrame.HasShadow = false;
                    Speech_LastCompletedDate.Text = "✓  Today";

                }
                else
                {
                   await Application.Current.MainPage.DisplayAlert("PASS Failed", "Please check your connection to the internet and try again.", "OK");
                }
            }
            else
            {
                Speech_Clicked(sender, e);
            }
        }

        async void MenuButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuPage());
        }

        public async Task PutRecordingToServer()
        {
            Stream pass = new MemoryStream(); ;
            var uri = new Uri(string.Format(CompanionServer.recording_url + App.UserID + "/Pass", string.Empty));

            try
            {
                var content = new StreamContent(pass);
                content.Headers.Remove("Content-Type");
                content.Headers.Add("Content-Type", "application/octet-stream");
                var request = new HttpRequestMessage(HttpMethod.Put, uri)
                {
                    Content = content
                };

                var response = await _client.SendAsync(request);
                Console.WriteLine("HTTP PUT Response: " + response);
                if (response.IsSuccessStatusCode)
                {
                    App.SuccessfulPUT = true;
                }
                else
                {
                    App.SuccessfulPUT = false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(@"ERROR {0}", ex.Message);
            }
        }
    }
}

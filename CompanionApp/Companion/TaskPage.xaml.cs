using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Companion
{
    public partial class TaskPage : ContentPage
    {
        string sentence, hash;
        byte[] image;
        HttpClient _client;
        //double DaysBetweenTasks = 6.8;
        // TODO: This is for debugging only. Makes tasks enabled after only 30seconds
        double DaysBetweenTasks = 0.000347;

        public TaskPage()
        {
            _client = new HttpClient();
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MenuButton.Source = ImageSource.FromResource("Companion.Icons.menu_icon.png");
            UserIcon.Source = ImageSource.FromResource("Companion.Icons.user_icon.png");

            // Welcome message
            WelcomeUser();

            // Handle HTTP GET request for next Speech Task
            LoadSpeechTaskFromServer();

            // Has it been a week or more since last task completion?
            SpeechTaskAvailability();

            // Update Speech Task Last Completed Display
            UpdateCompletedDate_Speech();
            // Update Questionnaire Last Completed Display
            //UpdateCompletedDate_Questionnaire();

            App.FirstTimeLoading = false;
        }

        protected override void OnAppearing()
        {
            // TODO: Do this for OnResume() as well
            // Has it been a week or more since last task completion?
            SpeechTaskAvailability();

            // Update Speech Task Last Completed Display
            UpdateCompletedDate_Speech();
            // Update Questionnaire Last Completed Display
            //UpdateCompletedDate_Questionnaire();
            base.OnAppearing();
        }

        void SpeechTaskAvailability()
        {
            if (App.SpeechTaskCompleted)
            {
                TimeSpan interval = DateTime.Now - App.SpeechTaskLastCompleted;
                if (interval.TotalDays > DaysBetweenTasks)
                {
                    SpeechButton.IsEnabled = true;
                }
                else
                {
                    SpeechButton.IsEnabled = false;
                }
            }
        }

        async private void LoadSpeechTaskFromServer()
        {
            if (App.FirstTimeLoading)
            {
                // TODO: Check Server Response to make sure GET was a Success. If fail, update bool and Display "Server communication issues. Make sure youre connected"
                await GetSentenceFromServer();
                App.SpeechTaskDataReceived = true;
                return;
            }

            if (App.SpeechTaskType.Equals("Sentence"))
            {
                if (!App.SpeechTaskDataReceived)
                {
                    await GetSentenceFromServer();
                    App.SpeechTaskDataReceived = true;
                    return;
                }
            }
            else if (App.SpeechTaskType.Equals("Image"))
            {
                // TODO: Remove true
                if (!App.SpeechTaskDataReceived || true)
                {
                    // TODO: Check Server Response to make sure GET was a Success. If fail, update bool and Display "Server connection issues. Make sure youre connected"
                    await GetImageFromServer();
                    App.SpeechTaskDataReceived = true;
                    return;
                }
            }
            else if (App.SpeechTaskType.Equals("Breath"))
            {
                // No need to talk to server
                // Is there a need for this?
            }

        }

        public async Task GetSentenceFromServer()
        {
            var uri = new Uri(string.Format(CompanionServer.sentence_url + App.UserID, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var headerValues = response.Headers.ToString().Split('\n');
                    foreach (string val in headerValues)
                    {
                        if (val.Contains("hash"))
                        {
                            hash = val.Split(' ')[1];
                            App.CurrentSentenceHash = hash;
                        }
                    }
                    sentence = await response.Content.ReadAsStringAsync();
                    App.CurrentSentence = sentence;
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() => { PageTitle.Text = "Bad GET Resp"; });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task GetImageFromServer()
        {
            var uri = new Uri(string.Format(CompanionServer.image_url + App.UserID, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                Console.WriteLine("Image GET Response: " + response);
                if (response.IsSuccessStatusCode)
                {
                    var headerValues = response.Headers.ToString().Split('\n');
                    foreach (string val in headerValues)
                    {
                        if (val.Contains("hash"))
                        {
                            hash = val.Split(' ')[1];
                            App.CurrentImageHash = hash;
                        }
                    }
                    image = await response.Content.ReadAsByteArrayAsync();
                    Console.WriteLine(image);
                    App.CurrentImage = image;
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() => { PageTitle.Text = "Bad GET Resp"; });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
                welcomeMessage = welcomeMessage + ", welcome!";
            }
            Welcome.Text = welcomeMessage;
        }

        private void UpdateCompletedDate_Speech()
        {
            string last = App.SpeechTaskLastCompleted.ToString().Split(' ')[0];
            TimeSpan interval = DateTime.Now - App.SpeechTaskLastCompleted;
            if (last.Contains("0001"))
            {
                Speech_LastCompletedDate.Text = " ";
            }
            else
            {
                if (App.SpeechTaskLastCompleted.DayOfWeek == DateTime.Now.DayOfWeek)
                {
                    Speech_LastCompletedDate.Text = "✓  Today";
                }
                else if (App.SpeechTaskLastCompleted.DayOfWeek == DateTime.Now.AddDays(-1).DayOfWeek)
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
            await DisplayAlert("Test", "This is a test!", "OK", "Retry");
        }

        //async void Questionnaire_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new QuestionnairePage());
        //}

        //private void UpdateCompletedDate_Questionnaire()
        //{
        //    if (App.CurrentQuestion != 1)
        //    {
        //        Questionnaire_LastCompletedDate.Text = "In Progress";
        //    }
        //    else
        //    {
        //        string last = App.QuestionnaireLastCompleted.ToString().Split(' ')[0];
        //        if (last.Contains("0001"))
        //        {
        //            Questionnaire_LastCompletedDate.Text = " ";
        //        }
        //        else
        //        {
        //            Questionnaire_LastCompletedDate.Text = "✓ " + last;
        //        }
        //    }
        //}
    }
}

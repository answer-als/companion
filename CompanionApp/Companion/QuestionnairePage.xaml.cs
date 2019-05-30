using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Text;
using System.Threading.Tasks;

namespace Companion
{
    public partial class QuestionnairePage : ContentPage
    {
        HttpClient _client;

        public QuestionnairePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BackButton.Source = ImageSource.FromResource("Companion.Icons.back_icon.png");
            App.CurrentPage = "Questionnaire";

            if (App.QuestionnaireCompleted)
            {
                BackButton.IsVisible = true;
            } 
            else
            {
                BackButton.IsVisible = false;
                Device.BeginInvokeOnMainThread(() => WelcomePopUp());
            }

            _client = new HttpClient();
            QuestionView1.Parent = this;
            QuestionView22.Parent = this;
            QuestionView2.Parent = this;
            QuestionView3.Parent = this;
            StartCorrectly();
        }

        // Make sure to launch the questionnaire on the page that the user last left off on
        void StartCorrectly()
        {
            switch (App.CurrentQuestion)
            {
                case 1:
                    QuestionView1.IsVisible = true;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question1.png");
                    PreviousButton.Text = " ";
                    break;
                case 2:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView22.IsVisible = true;
                    PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question2.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 3:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = true;
                    QuestionView3.IsVisible = false;
                    PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question3.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 4:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = true;
                    PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question4.png");
                    PreviousButton.Text = "< Back";
                    Next.IsVisible = false;
                    Submit.IsVisible = true;
                    break;
            }
        }

        async void WelcomePopUp() 
        {
            await DisplayAlert("Welcome", "Before we begin, we're going to ask you a few questions so that we can improve our evaluation of your data. It is required that you complete this questionnaire before proceeding to the main application.", "Continue");
        }

        async void Submit_Clicked(object sender, EventArgs e)
        {
            SaveProfile();
            Submit.IsEnabled = false;
            HTTPPutUploading();
            await PUTProfileToServer();
            //App.QuestionnaireLastCompleted = DateTime.Now;
            if (App.SuccessfulPUT)
            {
                App.CurrentQuestion = 1;
                App.QuestionnaireCompleted = true;
                await HTTPPutSuccess();
                await Navigation.PushAsync(new TaskPage());
            }
            else
            {
                HTTPPutFail();
            }
        }

        void HTTPPutUploading()
        {
            QuestionView3.IsVisible = false;
            LoadingScreen.IsVisible = true;
            LoadingScreen.UploadingProfile();
        }

        async Task HTTPPutSuccess()
        {
            Device.BeginInvokeOnMainThread(LoadingScreen.Success);
            await Task.Delay(1800);
        }

        void HTTPPutFail()
        {
            LoadingScreen.IsVisible = false;
            QuestionView3.IsVisible = true;
            Submit.IsEnabled = true;
            Application.Current.MainPage.DisplayAlert("Error", "Your answers were saved, but could not be uploaded. Please make sure you are connected to the network, and try again.", "OK");
        }

        public StringContent MakeProfileContent()
        {
            var data = new
            {
                EducationLevel = App.EducationLevel,
                BirthYear = App.BirthYear,
                Sex = App.Sex,
                Onset = App.OnsetSite,
                EnglishFirstLanguage = App.English1Lang,
                EnglishLearningAge = App.EnglishLearnerAge,
                LanguagesSpoken = App.LangsSpoken,
                LanguagesExposed = App.LangsExposed
            };
            var result = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            return result;
        }

        public async Task PUTProfileToServer()
        {
            var uri = new Uri(string.Format(CompanionServer.profile_url + App.UserID, string.Empty));

            try
            {
                HttpContent content = MakeProfileContent();

                var readable = await content.ReadAsStringAsync();

                content.Headers.Remove("Content-Type");
                content.Headers.Add("Content-Type", "application/json");
                var request = new HttpRequestMessage(HttpMethod.Put, uri)
                {
                    Content = content
                };

                var response = await _client.SendAsync(request);
                Console.WriteLine("HTTP PUT Profile Response: " + response);
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

        void SaveProfile()
        {
            QuestionView1.SaveResponses();
            QuestionView22.SaveResponses();
            QuestionView2.SaveResponses();
            QuestionView3.SaveResponses();
        }

        void Next_Clicked(object sender, EventArgs e)
        {
            switch (App.CurrentQuestion)
            {
                case 1:
                    if (QuestionView1.Completed())
                    {
                        QuestionView1.SaveResponses();
                        QuestionView1.IsVisible = false;
                        QuestionView22.IsVisible = true;
                        PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question2.png");
                        PreviousButton.Text = "< Back";
                        App.CurrentQuestion = 2;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 2:
                    if (QuestionView22.Completed())
                    {
                        QuestionView22.SaveResponses();
                        QuestionView22.IsVisible = false;
                        QuestionView2.IsVisible = true;
                        PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question3.png");
                        App.CurrentQuestion = 3;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 3:
                    if (QuestionView2.Completed())
                    {
                        QuestionView2.SaveResponses();
                        QuestionView2.IsVisible = false;
                        QuestionView3.IsVisible = true;
                        PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question4.png");
                        Next.IsVisible = false;
                        Submit.IsVisible = true;
                        App.CurrentQuestion = 4;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
            }
        }

        public void HideWarning()
        {
            IncompleteWarning.Text = " ";
        }

        void Previous_Clicked(object sender, EventArgs e)
        {
            switch (App.CurrentQuestion)
            {
                case 4:
                    QuestionView2.IsVisible = true;
                    QuestionView3.IsVisible = false;
                    PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question3.png");
                    Next.IsVisible = true;
                    Submit.IsVisible = false;
                    App.CurrentQuestion = 3;
                    break;
                case 3:
                    QuestionView22.IsVisible = true;
                    QuestionView2.IsVisible = false;
                    PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question2.png");
                    App.CurrentQuestion = 2;
                    break;
                case 2:
                    QuestionView1.IsVisible = true;
                    QuestionView22.IsVisible = false;
                    PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question1.png");
                    PreviousButton.Text = " ";
                    App.CurrentQuestion = 1;
                    break;
            }
        }

        async void BackButton_Clicked(object sender, EventArgs e)
        {
            if (App.CurrentQuestion != 1)
            {
                bool result = await DisplayAlert("Questionnaire In Progress!", "Are you sure you want to leave the questionnaire?", "Yes", "No");

                if (result)
                {
                    NavigationPage page = new NavigationPage(new TaskPage());
                    Application.Current.MainPage = page;
                    await Navigation.PopToRootAsync();
                }
            } 
            else
            {
                NavigationPage page = new NavigationPage(new TaskPage());
                Application.Current.MainPage = page;
                await Navigation.PopToRootAsync();
            }
        }
    }
}

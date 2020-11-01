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
        // TODO: Fix Questionnaire Order --> Current Questionnaire Order: 1, 2, 23, 22, 33, 3
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
            QuestionView2.Parent = this;
            QuestionView3.Parent = this;
            QuestionView4.Parent = this;
            QuestionView5.Parent = this;
            QuestionView6.Parent = this;
            QuestionView7.Parent = this;
            QuestionView8.Parent = this;
            QuestionView9.Parent = this;
            QuestionView10.Parent = this;
            QuestionView11.Parent = this;
            QuestionView12.Parent = this;
            QuestionView13.Parent = this;
            QuestionView14.Parent = this;
            QuestionView15.Parent = this;
            StartCorrectly();
        }

        // Make sure to launch the questionnaire on the page that the user last left off on
        void StartCorrectly()
        {
//            PreviousButton.IsVisible = true;
//            PreviousButton.Text = "< Back";

            switch (App.CurrentQuestion)
            {
                case 1:
                    QuestionView1.IsVisible = true;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question1.png");
                    PreviousButton.Text = " ";
                    break;
                case 2:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = true;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question2.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 3:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = true;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question3.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 4:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = true;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question4.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 5:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = true;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question5.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 6:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = true;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    PreviousButton.Text = "< Back";
//                    Next.IsVisible = false;
//                    Submit.IsVisible = true;
                    break;
                case 7:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = true;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 8:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = true;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 9:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = true;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 10:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = true;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 11:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = true;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 12:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = true;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 13:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = true;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    PreviousButton.Text = "< Back";
                    break;
                case 14:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = true;
                    QuestionView15.IsVisible = false;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    PreviousButton.Text = "< Back";
                    break;
//                    Next.IsVisible = false;
//                    Submit.IsVisible = true;
                case 15:
                    QuestionView1.IsVisible = false;
                    QuestionView2.IsVisible = false;
                    QuestionView3.IsVisible = false;
                    QuestionView4.IsVisible = false;
                    QuestionView5.IsVisible = false;
                    QuestionView6.IsVisible = false;
                    QuestionView7.IsVisible = false;
                    QuestionView8.IsVisible = false;
                    QuestionView9.IsVisible = false;
                    QuestionView10.IsVisible = false;
                    QuestionView11.IsVisible = false;
                    QuestionView12.IsVisible = false;
                    QuestionView13.IsVisible = false;
                    QuestionView14.IsVisible = false;
                    QuestionView15.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    PreviousButton.Text = "< Back";
                    Next.IsVisible = false;
                    Submit.IsVisible = true;
                    break;
            }
        }

        async void WelcomePopUp() 
        {
            await DisplayAlert("Questionnaire", "We're going to ask you several questions so that we can assess your current physical condition. The questions are based on a standard questionnaire used by ALS physicians and clinics.\n\nFor each question, choose the option that best describes your abilities today.\n\nPlease answer these questions even if you do not have a motor neuron disease.", "Start");
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
                //await HTTPPutSuccess();
                await Navigation.PushAsync(new TaskPage());
            }
            else
            {
                HTTPPutFail();
            }
        }

        void HTTPPutUploading()
        {
            QuestionView10.IsVisible = false;
            LoadingScreen.IsVisible = true;
            LoadingScreen.UploadingProfile();
        }

        //async Task HTTPPutSuccess()
        //{
        //    Device.BeginInvokeOnMainThread(LoadingScreen.Success);
        //    await Task.Delay(1800);
        //}

        void HTTPPutFail()
        {
            LoadingScreen.IsVisible = false;
            QuestionView6.IsVisible = true;
            Submit.IsEnabled = true;
            Application.Current.MainPage.DisplayAlert("Error", "Your answers were saved, but could not be uploaded. Please make sure you are connected to the network, and try again.", "OK");
        }

        public StringContent MakeProfileContent()
        {
            var data = new
            {
                EducationLevel = App.EducationLevel,
                BirthYear = App.BirthYear,
                Person = App.Person,
                EnglishFirstLanguage = App.English1Lang,
                EnglishLearningAge = App.EnglishLearnerAge,
                Handedness = App.Handedness,
                UserType = App.UserType,
                OnsetSite = App.OnsetSite,
                OnsetDate = App.OnsetDate,
                Diagnosis = App.Diagnosis,
                OtherDiagnosis = App.OtherDiagnosis,
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
            QuestionView2.SaveResponses();
            QuestionView3.SaveResponses();
            QuestionView4.SaveResponses();
            QuestionView5.SaveResponses();
            QuestionView6.SaveResponses();
            QuestionView7.SaveResponses();
            QuestionView8.SaveResponses();
            QuestionView9.SaveResponses();
            QuestionView10.SaveResponses();
            QuestionView11.SaveResponses();
            QuestionView12.SaveResponses();
            QuestionView13.SaveResponses();
            QuestionView14.SaveResponses();
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
                        QuestionView2.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question2.png");
                        PreviousButton.Text = "< Back";
                        App.CurrentQuestion = 2;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 2:
                    if (QuestionView2.Completed())
                    {
                        QuestionView2.SaveResponses();
                        QuestionView2.IsVisible = false;
                        QuestionView3.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question3.png");
                        App.CurrentQuestion = 3;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 3:
                    if (QuestionView3.Completed())
                    {
                        QuestionView3.SaveResponses();
                        QuestionView3.IsVisible = false;

                        // If Control User, then SKIP to Question 6, else go to next question
                        if (App.UserType.Equals("Control"))
                        {
                            QuestionView6.IsVisible = true;
                            //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                            Next.IsVisible = false;
                            Submit.IsVisible = true;
                            App.CurrentQuestion = 6;
                        }
                        else
                        {
                            QuestionView4.IsVisible = true;
                            //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question4.png");
                            App.CurrentQuestion = 4;
                        }
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 4:
                    if (QuestionView4.Completed())
                    {
                        QuestionView4.SaveResponses();
                        QuestionView4.IsVisible = false;
                        QuestionView5.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question5.png");
                        App.CurrentQuestion = 5;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 5:
                    if (QuestionView5.Completed())
                    {
                        QuestionView5.SaveResponses();
                        QuestionView5.IsVisible = false;
                        QuestionView6.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                        //Next.IsVisible = false;
                        //Submit.IsVisible = true;
                        App.CurrentQuestion = 6;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 6:
                    if (QuestionView6.Completed())
                    {
                        QuestionView6.SaveResponses();
                        QuestionView6.IsVisible = false;
                        QuestionView7.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                        App.CurrentQuestion = 7;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 7:
                    if (QuestionView7.Completed())
                    {
                        QuestionView7.SaveResponses();
                        QuestionView7.IsVisible = false;
                        QuestionView8.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                        App.CurrentQuestion = 8;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 8:
                    if (QuestionView8.Completed())
                    {
                        QuestionView8.SaveResponses();
                        QuestionView8.IsVisible = false;
                        QuestionView9.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                        App.CurrentQuestion = 9;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 9:
                    if (QuestionView9.Completed())
                    {
                        QuestionView9.SaveResponses();
                        QuestionView9.IsVisible = false;
                        QuestionView10.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                        App.CurrentQuestion = 10;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 10:
                    if (QuestionView10.Completed())
                    {
                        QuestionView10.SaveResponses();
                        QuestionView10.IsVisible = false;
                        QuestionView11.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                        App.CurrentQuestion = 11;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 11:
                    if (QuestionView11.Completed())
                    {
                        QuestionView11.SaveResponses();
                        QuestionView11.IsVisible = false;
                        QuestionView12.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                        App.CurrentQuestion = 12;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 12:
                    if (QuestionView12.Completed())
                    {
                        QuestionView12.SaveResponses();
                        QuestionView12.IsVisible = false;
                        QuestionView13.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                        App.CurrentQuestion = 13;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 13:
                    if (QuestionView13.Completed())
                    {
                        QuestionView13.SaveResponses();
                        QuestionView13.IsVisible = false;
                        QuestionView14.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                        App.CurrentQuestion = 14;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 14:
                    if (QuestionView14.Completed())
                    {
                        QuestionView14.SaveResponses();
                        QuestionView14.IsVisible = false;
                        QuestionView15.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                        Next.IsVisible = false;
                        Submit.IsVisible = true;
                        App.CurrentQuestion = 15;
                    }
                    else
                    {
                        IncompleteWarning.Text = "Please answer each question.";
                    }
                    break;
                case 15:
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                        Next.IsVisible = false;
                        Submit.IsVisible = true;
                        App.CurrentQuestion = 15;
                    break;
            }
        }

        public void HideWarning()
        {
            IncompleteWarning.Text = " ";
        }

        void Previous_Clicked(object sender, EventArgs e)
        {
            Next.IsVisible = true;
            Submit.IsVisible = false;

            switch (App.CurrentQuestion)
            {
                /*
                case 6:
                    QuestionView6.IsVisible = false;
                    Next.IsVisible = true;
                    Submit.IsVisible = false;

                    // If Control User, then SKIP to Question 3, else go to previous question
                    if (App.UserType.Equals("Control"))
                    {
                        QuestionView3.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question3.png");
                        App.CurrentQuestion = 3;
                    }
                    else
                    {
                        QuestionView5.IsVisible = true;
                        //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question5.png");
                        App.CurrentQuestion = 5;
                    }
                    break;
                    */
                case 15:
                    QuestionView15.IsVisible = false;
                    QuestionView14.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    App.CurrentQuestion = 14;
                    break;
                case 14:
                    QuestionView14.IsVisible = false;
                    QuestionView13.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    App.CurrentQuestion = 13;
                    break;
                case 13:
                    QuestionView13.IsVisible = false;
                    QuestionView12.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    App.CurrentQuestion = 12;
                    break;
                case 12:
                    QuestionView12.IsVisible = false;
                    QuestionView11.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    App.CurrentQuestion = 11;
                    break;
                case 11:
                    QuestionView11.IsVisible = false;
                    QuestionView10.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    App.CurrentQuestion = 10;
                    break;
                case 10:
                    QuestionView10.IsVisible = false;
                    QuestionView9.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    App.CurrentQuestion = 9;
                    break;
                case 9:
                    QuestionView9.IsVisible = false;
                    QuestionView8.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    App.CurrentQuestion = 8;
                    break;
                case 8:
                    QuestionView8.IsVisible = false;
                    QuestionView7.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    App.CurrentQuestion = 7;
                    break;
                case 7:
                    QuestionView7.IsVisible = false;
                    QuestionView6.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question6.png");
                    App.CurrentQuestion = 6;
                    break;
                case 6:
                    QuestionView6.IsVisible = false;
                    QuestionView5.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question5.png");
                    App.CurrentQuestion = 5;
                    break;
                case 5:
                    QuestionView5.IsVisible = false;
                    QuestionView4.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question4.png");
                    App.CurrentQuestion = 4;
                    break;
                case 4:
                    QuestionView4.IsVisible = false;
                    QuestionView3.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question3.png");
                    App.CurrentQuestion = 3;
                    break;
                case 3:
                    QuestionView3.IsVisible = false;
                    QuestionView2.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question2.png");
                    App.CurrentQuestion = 2;
                    break;
                case 2:
                    QuestionView2.IsVisible = false;
                    QuestionView1.IsVisible = true;
                    //PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question1.png");
                    PreviousButton.Text = " ";
                    PreviousButton.IsVisible = false;
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

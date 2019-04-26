using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Companion
{
    public partial class QuestionnairePage : ContentPage
    {
        public QuestionnairePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BackButton.Source = ImageSource.FromResource("Companion.Icons.back_icon.png");

            if (App.QuestionnaireCompleted)
            {
                BackButton.IsVisible = true;
            } 
            else
            {
                BackButton.IsVisible = false;
                Device.BeginInvokeOnMainThread(() => WelcomePopUp());
            }

            // TODO: Rename as 1, 2, 3, 4 and create a new position dots image series to handle 4 questions
            if (App.CurrentQuestion == 1)
            {
                QuestionView1.IsVisible = true;
                QuestionView2.IsVisible = false;
                QuestionView3.IsVisible = false;
                PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question1.png");
                PreviousButton.Text = " ";
            }
            else if (App.CurrentQuestion == 22)
            {
                QuestionView1.IsVisible = false;
                QuestionView2.IsVisible = false;
                QuestionView3.IsVisible = false;
                QuestionView22.IsVisible = true;
                PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question2.png");
                PreviousButton.Text = "< Back";
            }
            else if (App.CurrentQuestion == 2)
            {
                QuestionView1.IsVisible = false;
                QuestionView2.IsVisible = true;
                QuestionView3.IsVisible = false;
                PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question3.png");
                PreviousButton.Text = "< Back";
            }
            else if (App.CurrentQuestion == 3)
            {
                QuestionView1.IsVisible = false;
                QuestionView2.IsVisible = false;
                QuestionView3.IsVisible = true;
                PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question4.png");
                PreviousButton.Text = "< Back";
                Next.IsVisible = false;
                Submit.IsVisible = true;
            }
        }

        async void WelcomePopUp() 
        {
            await DisplayAlert("Welcome", "Before we begin, we're going to ask you a few questions so that we can improve our evaluation of your data. It is required that you completed this questionnaire before proceeding to the main application.", "Continue");
        }

        async void Submit_Clicked(object sender, EventArgs e)
        {
            App.CurrentQuestion = 1;
            App.QuestionnaireCompleted = true;
            App.QuestionnaireLastCompleted = DateTime.Now;
            await DisplayAlert("Success", "Thank you for filling out the survey! Your answers have been saved.", "Continue");
            await Navigation.PushAsync(new TaskPage());
        }

        void Next_Clicked(object sender, EventArgs e)
        {
            if (App.CurrentQuestion == 1)
            {
                QuestionView1.IsVisible = false;
                QuestionView22.IsVisible = true;
                PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question2.png");
                PreviousButton.Text = "< Back";
                App.CurrentQuestion = 22;
            }
            else if (App.CurrentQuestion == 22)
            {
                QuestionView2.IsVisible = true;
                QuestionView22.IsVisible = false;
                PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question3.png");
                App.CurrentQuestion = 2;
            }
            else if (App.CurrentQuestion == 2)
            {
                QuestionView3.IsVisible = true;
                QuestionView2.IsVisible = false;
                PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question4.png");
                Next.IsVisible = false;
                Submit.IsVisible = true;
                App.CurrentQuestion = 3;
            }
        }

        void Previous_Clicked(object sender, EventArgs e)
        {
            if (App.CurrentQuestion == 3)
            {
                QuestionView2.IsVisible = true;
                QuestionView3.IsVisible = false;
                PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question3.png");
                Next.IsVisible = true;
                Submit.IsVisible = false;
                App.CurrentQuestion = 2;
            }
            else if (App.CurrentQuestion == 2)
            {
                QuestionView22.IsVisible = true;
                QuestionView2.IsVisible = false;
                PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question2.png");
                App.CurrentQuestion = 22;
            }
            else if (App.CurrentQuestion == 22)
            {
                QuestionView1.IsVisible = true;
                QuestionView22.IsVisible = false;
                PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.Questionnaire.question1.png");
                PreviousButton.Text = " ";
                App.CurrentQuestion = 1;
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

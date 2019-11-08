﻿using System;
using Xamarin.Forms;

namespace Companion
{
    public partial class SpeechTaskPage : ContentPage
    {

        public SpeechTaskPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            HomeButton.Source = ImageSource.FromResource("Companion.Icons.home_icon.png");
            App.CurrentPage = "Speech";

            MainSentenceView.Parent = this;
            MainImageView.Parent = this;
            MainBreathView.Parent = this;

            DisplayCurrentTask();
        }

        async void DisplayCurrentTask()
        {
            if (App.SpeechTaskType.Equals("Sentence"))
            {
                MainSentenceView.IsVisible = true;
                MainImageView.IsVisible = false;
                MainBreathView.IsVisible = false;
                await MainSentenceView.GetSentenceFromServer();
            }
            else if (App.SpeechTaskType.Equals("Image"))
            {
                MainSentenceView.IsVisible = false;
                MainImageView.IsVisible = true;
                MainBreathView.IsVisible = false;
                await MainImageView.GetImageFromServer();
            }
            else if (App.SpeechTaskType.Equals("Breath"))
            {
                MainSentenceView.IsVisible = false;
                MainImageView.IsVisible = false;
                MainBreathView.IsVisible = true;
            }
        }

        async void HomeButton_Clicked(object sender, EventArgs e)
        {
            // If User selects HOME BUTTON during a recording, make sure to stop the recording
            // Display Alert to user asking them "Are you sure you want to end the task?"
            App.SpeechTaskDataReceived = false;

            if (App.IsRecording)
            {
                App.IsRecording = false;

                if (MainSentenceView.IsVisible)
                {
                    MainSentenceView.EndRecording(sender, e);
                }
                else if (MainImageView.IsVisible)
                {
                    MainImageView.EndRecording(sender, e);
                }
                else if (MainBreathView.IsVisible)
                {
                    MainBreathView.EndRecording(sender, e);
                }

                App.CurrentPage = "Alert";
                bool result = await DisplayAlert("Recording interrupted!", "Are you sure you want to leave the current task without finishing? Your recording will be discarded!", "Yes", "No");
                App.CurrentPage = "Speech";

                if (result)
                {
                    NavigationPage page = new NavigationPage(new TaskPage());
                    Application.Current.MainPage = page;
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    if (!App.RecordedButNotSaved)
                    {
                        if (MainSentenceView.IsVisible)
                        {
                            MainSentenceView.RetryButton_Clicked(sender, e);
                        }
                        else if (MainImageView.IsVisible)
                        {
                            MainImageView.RetryButton_Clicked(sender, e);
                        }
                        else if (MainBreathView.IsVisible)
                        {
                            MainBreathView.RetryButton_Clicked(sender, e);
                        }
                    }
                }
            }
            else if (App.RecordedButNotSaved)
            {
                App.CurrentPage = "Alert";
                bool result = await DisplayAlert("Task Not Finished!", "Are you sure you want to leave the current task without finishing? Your recording will be discarded!", "Yes", "No");
                App.CurrentPage = "Speech";
                if (result)
                {
                    MainSentenceView.player.Pause();
                    MainImageView.player.Pause();
                    MainBreathView.player.Pause();

                    App.RecordedButNotSaved = false;
                    NavigationPage page = new NavigationPage(new TaskPage());
                    Application.Current.MainPage = page;
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                MainSentenceView.player.Pause();
                MainImageView.player.Pause();
                MainBreathView.player.Pause();

                NavigationPage page = new NavigationPage(new TaskPage());
                Application.Current.MainPage = page;
                await Navigation.PopToRootAsync();
            }
        }

        public void DisableHomeButton()
        {
            HomeButton.IsEnabled = false;
        }

        public void EnableHomeButton()
        {
            HomeButton.IsEnabled = true;
        }
    }
}

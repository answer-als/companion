using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using Xamarin.Forms;
using Plugin.Permissions;

namespace Companion
{
    public partial class SpeechTaskPage : ContentPage
    {

        public SpeechTaskPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            HomeButton.Source = ImageSource.FromResource("Companion.Icons.home_icon.png");

            CheckPermissions();
            MainPhraseView.Parent = this;
            MainImageView.Parent = this;
            MainBreathView.Parent = this;

            // TODO: Also call this in OnResume?
            if (App.SpeechTaskType.Equals("Sentence"))
            {
                MainPhraseView.IsVisible = true;
                MainImageView.IsVisible = false;
                MainBreathView.IsVisible = false;
            }
            else if (App.SpeechTaskType.Equals("Image"))
            {
                MainPhraseView.IsVisible = false;
                MainImageView.IsVisible = true;
                MainBreathView.IsVisible = false;
            }
            else if (App.SpeechTaskType.Equals("Breath"))
            {
                MainPhraseView.IsVisible = false;
                MainImageView.IsVisible = false;
                MainBreathView.IsVisible = true;
            }
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

        async void HomeButton_Clicked(object sender, EventArgs e)
        {
            // If User selects HOME BUTTON during a recording, make sure to stop the recording
            // Display Alert to user asking them "Are you sure you want to end the task?"
            if (App.IsRecording)
            {
                App.IsRecording = false;
                MainPhraseView.EndRecording(sender, e);

                bool result = await DisplayAlert("Recording interrupted!", "Are you sure you want to leave the current task without finishing? Your recording will be discarded!", "Yes", "No");

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
                        MainPhraseView.RetryButton_Clicked(sender, e);
                    }
                }
            }
            else if (App.RecordedButNotSaved)
            {
                bool result = await DisplayAlert("Task Not Finished!", "Are you sure you want to leave the current task without finishing? Your recording will be discarded!", "Yes", "No");

                if (result)
                {
                    MainPhraseView.player.Pause();
                    NavigationPage page = new NavigationPage(new TaskPage());
                    Application.Current.MainPage = page;
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                MainPhraseView.player.Pause();
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

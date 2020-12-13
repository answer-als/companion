using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Companion
{
    public partial class SpeechInstructionsPage : ContentPage
    {
        int CurrentInstruction;

        public SpeechInstructionsPage()
        {
            InitializeComponent();

            //// Change to OnPlatform syntax is confusing and docs do not address the Entry element.
            //// Default XAML is set for iOS.
            //// Ref https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/fonts
            if (Device.RuntimePlatform == Device.iOS)
            {
                OuterGrid.Padding = new Thickness(5, 25, 5, 15);
                //PageTitle.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                OuterGrid.Padding = new Thickness(5, 15, 5, 15);
            }

            NavigationPage.SetHasNavigationBar(this, false);
            OnArrivalUI();
        }

        void OnArrivalUI()
        {
            //PreviousButton.Text = " ";
            Instruction1.IsVisible = true;
            Instruction2.IsVisible = false;
            Instruction3.IsVisible = false;
            Next.IsVisible = true;
            LetsBegin.IsVisible = false;
            CurrentInstruction = 1;
            BackButton.Source = ImageSource.FromResource("Companion.Icons.back_icon.png");
            Indicator.Source = ImageSource.FromResource("Companion.PositionDots.SpeechIntro.intro_1.png");
        }

        async void Begin_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new SpeechTaskPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }
        }

        // Get out of Speech task and go back to home Task Page
        async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        // Instruction Navigation (Next goes forward, Previous goes backward)
        void Next_Clicked(object sender, EventArgs e)
        {
            if (CurrentInstruction == 1)
            {
                CurrentInstruction = 2;
                Instruction2.IsVisible = true;
                Instruction1.IsVisible = false;
                PreviousButton.TextColor = Color.FromHex("#006080");
                Indicator.Source = ImageSource.FromResource("Companion.PositionDots.SpeechIntro.intro_2.png");
            }
            else if (CurrentInstruction == 2)
            {
                CurrentInstruction = 3;
                Instruction3.IsVisible = true;
                Instruction2.IsVisible = false;
                Indicator.Source = ImageSource.FromResource("Companion.PositionDots.SpeechIntro.intro_3.png");
                Next.IsVisible = false;
                LetsBegin.IsVisible = true;
            }
        }

        void Previous_Clicked(object sender, EventArgs e)
        {
            if (CurrentInstruction == 3)
            {
                CurrentInstruction = 2;
                Instruction2.IsVisible = true;
                Instruction3.IsVisible = false;
                Indicator.Source = ImageSource.FromResource("Companion.PositionDots.SpeechIntro.intro_2.png");
                Next.IsVisible = true;
                LetsBegin.IsVisible = false;
            }
            else if (CurrentInstruction == 2)
            {
                CurrentInstruction = 1;
                Instruction1.IsVisible = true;
                Instruction2.IsVisible = false;
                PreviousButton.TextColor = Color.FromHex("#e6e6e6");
                Indicator.Source = ImageSource.FromResource("Companion.PositionDots.SpeechIntro.intro_1.png");
            }
        }
    }
}

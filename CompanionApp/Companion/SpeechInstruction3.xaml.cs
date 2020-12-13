using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class SpeechInstruction3 : ContentView
    {
        public SpeechInstruction3()
        {
            InitializeComponent();

            //// Change to OnPlatform syntax is confusing and docs do not address the Entry element.
            //// Default XAML is set for iOS.
            //// Ref https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/fonts
            if (Device.RuntimePlatform == Device.Android)
            {
                MainImage.Margin = new Thickness(60, 20, 60, 0);
                MainImage.HeightRequest = 200;
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                MainImage.Margin = new Thickness(60, 20, 60, 10);
                MainImage.HeightRequest = 200;
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                MainImage.Margin = new Thickness(60, 40, 60, 10);
                MainImage.HeightRequest = 300;
            }

            MainImage.Source = ImageSource.FromResource("Companion.SpeechIntroImages.volume_meter.png");
        }
    }
}

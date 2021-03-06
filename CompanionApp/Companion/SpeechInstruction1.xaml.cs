﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class SpeechInstruction1 : ContentView
    {
        public SpeechInstruction1()
        {
            InitializeComponent();

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

            MainImage.Source = ImageSource.FromResource("Companion.SpeechIntroImages.no_noise.png");
        }


    }
}

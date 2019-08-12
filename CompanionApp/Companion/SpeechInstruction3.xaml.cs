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
            MainImage.Source = ImageSource.FromResource("Companion.SpeechIntroImages.volume_meter.png");
        }
    }
}

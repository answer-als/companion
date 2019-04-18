using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Companion
{
    public partial class SpeechIntro : CarouselPage
    {
        public SpeechIntro()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BackButton1.Source = ImageSource.FromResource("Companion.back_icon.png");
            BackButton2.Source = ImageSource.FromResource("Companion.back_icon.png");
            BackButton3.Source = ImageSource.FromResource("Companion.back_icon.png");
        }

        void Begin_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SpeechTask());
        }

        void Next_Clicked(object sender, EventArgs e)
        {
            var pageCount = Children.Count;
            if (pageCount < 2)
                return;

            var index = Children.IndexOf(CurrentPage);
            index++;
            if (index >= pageCount)
                index = 0;

            CurrentPage = Children[index];
        }

        void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void SettingsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}

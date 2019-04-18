using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using Xamarin.Forms;

namespace Companion
{
    public partial class SpeechTask : ContentPage
    {
        public SpeechTask()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            HomeButton.Source = ImageSource.FromResource("Companion.home_icon.png");
            PositionDots.Source = ImageSource.FromResource("Companion.PositionDots.view_1.png");
        }

        async void HomeButton_Clicked(object sender, EventArgs e)
        {
            NavigationPage page = new NavigationPage(new TaskPage());
            Application.Current.MainPage = page;
            await Navigation.PopToRootAsync();
        }
    }
}

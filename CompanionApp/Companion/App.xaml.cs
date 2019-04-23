using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Companion
{
    public partial class App : Application
    {
        public static string Password { get; set; }
        public static int SpeechPhrase1 { get; set; }
        public static int SpeechPhrase2 { get; set; }
        public static int SpeechPhrase3 { get; set; }
        public static int LoginVisits { get; set; }
        public static bool IsRecording { get; set; }
        public static bool RecordedButNotSaved { get; set; }
        public static string UserID
        {
            get => Preferences.Get("UserID", "Sign Out");
            set => Preferences.Set("UserID", value);
        }
        public static string SpeechTaskState
        {
            get => Preferences.Get("SpeechTaskState", "None");
            set => Preferences.Set("SpeechTaskState", value);
        }
        public static bool ShowSpeechInstructions
        {
            get => Preferences.Get("SpeechIntro", true);
            set => Preferences.Set("SpeechIntro", value);
        }
        public static bool SpeechTaskCompleted
        {
            get => Preferences.Get("SpeechCompleted", false);
            set => Preferences.Set("SpeechCompleted", value);
        }
        public static DateTime SpeechTaskLastCompleted
        {
            get => Preferences.Get("SpeechLastCompleted", new DateTime(2000, 01, 01));
            set => Preferences.Set("SpeechLastCompleted", value);
        }

        public App()
        {
            InitializeComponent();
            LoginVisits = 0;
            Password = "gleason5";
            RecordedButNotSaved = false;
            IsRecording = false;
            SpeechPhrase1 = 9;
            SpeechPhrase2 = 16;
            SpeechPhrase3 = 17;
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Companion
{
    public partial class App : Application
    {
        //static Image NotAnImage;

        public static string Password { get; set; }
        public static int LoginVisits { get; set; }
        public static bool IsRecording { get; set; }
        public static bool RecordedButNotSaved { get; set; }
        public static bool FirstTimeLoading
        {
            get => Preferences.Get("FirstTime", true);
            set => Preferences.Set("FirstTime", value);
        }
        public static string UserID
        {
            get => Preferences.Get("UserID", "Sign Out");
            set => Preferences.Set("UserID", value);
        }
        public static string SpeechTaskType
        {
            get => Preferences.Get("SpeechTaskState", "Sentence"); // Other states include 'Image' and 'Breath'
            set => Preferences.Set("SpeechTaskState", value);
        }
        public static string CurrentSentence
        {
            get => Preferences.Get("CurrentSentence", "No Sentence Yet.");
            set => Preferences.Set("CurrentSentence", value);
        }
        public static string CurrentSentenceHash
        {
            get => Preferences.Get("CurrentSentenceHash", "No Hash Yet.");
            set => Preferences.Set("CurrentSentenceHash", value);
        }
        //public static Image CurrentImage
        //{
        //    get => Preferences.Get("CurrentImage", NotAnImage);
        //    set => Preferences.Set("CurrentSentence", value);
        //}
        public static bool SpeechTaskDataReceived
        {
            get => Preferences.Get("SpeechTaskDataReceived", false);
            set => Preferences.Set("SpeechTaskDataReceived", value);
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
            get => Preferences.Get("SpeechLastCompleted", DateTime.MinValue);
            set => Preferences.Set("SpeechLastCompleted", value);
        }
        public static bool QuestionnaireCompleted
        {
            get => Preferences.Get("QuestionnaireCompleted", false);
            set => Preferences.Set("QuestionnaireCompleted", value);
        }
        public static int CurrentQuestion
        {
            get => Preferences.Get("CurrentQuestion", 1);
            set => Preferences.Set("CurrentQuestion", value);
        }
        public static DateTime QuestionnaireLastCompleted
        {
            get => Preferences.Get("QuestionnaireLastCompleted", DateTime.MinValue);
            set => Preferences.Set("QuestionnaireLastCompleted", value);
        }

        public App()
        {
            InitializeComponent();
            LoginVisits = 0;
            Password = "Gleason5";
            RecordedButNotSaved = false;
            IsRecording = false;
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
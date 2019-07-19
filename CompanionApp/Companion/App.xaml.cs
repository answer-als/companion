using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.AudioRecorder;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Companion
{
    public partial class App : Application
    {
        public static AudioRecorderService GlobalRecorder { get; set; }
        public static AudioPlayer GlobalPlayer { get; set;  }
        public static string Password { get; set; }
        public static int LoginVisits { get; set; }
        public static bool IsRecording { get; set; }
        public static bool IsPlaying { get; set; }
        public static bool SuccessfulPUT { get; set; }
        public static bool SuccessfulGET { get; set; }
        public static bool RecordedButNotSaved { get; set; }
        public static string UserID
        {
            get => Preferences.Get("UserID", "Sign Out");
            set => Preferences.Set("UserID", value);
        }
        public static DateTime Month
        {
            get => Preferences.Get("Month", DateTime.MinValue);
            set => Preferences.Set("Month", value);
        }
        public static string CurrentPage
        {
            get => Preferences.Get("CurrentPage", "Login"); // Other pages include 'Questionnaire', 'Home', 'Menu', and 'Speech'
            set => Preferences.Set("CurrentPage", value);
        }
        public static string SpeechTaskType
        {
            get => Preferences.Get("SpeechTaskState", "Sentence"); // Other states include 'Image' and 'Breath'
            set => Preferences.Set("SpeechTaskState", value);
        }
        public static string CurrentSentence
        {
            get => Preferences.Get("CurrentSentence", "NoSentenceYet");
            set => Preferences.Set("CurrentSentence", value);
        }
        public static string CurrentSentenceHash
        {
            get => Preferences.Get("CurrentSentenceHash", "NoHashYet");
            set => Preferences.Set("CurrentSentenceHash", value);
        }
        public static byte[] CurrentImage { get; set; }
        public static string CurrentImageHash
        {
            get => Preferences.Get("CurrentImageHash", "NoHashYet");
            set => Preferences.Set("CurrentImageHash", value);
        }
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
        //public static DateTime QuestionnaireLastCompleted
        //{
        //    get => Preferences.Get("QuestionnaireLastCompleted", DateTime.MinValue);
        //    set => Preferences.Set("QuestionnaireLastCompleted", value);
        //}
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
        public static string EducationLevel
        {
            get => Preferences.Get("EducationLevel", "DidNotAnswer");
            set => Preferences.Set("EducationLevel", value);
        }
        public static string BirthYear
        {
            get => Preferences.Get("BirthYear", "DidNotAnswer");
            set => Preferences.Set("BirthYear", value);
        }
        public static string Sex
        {
            get => Preferences.Get("Sex", "DidNotAnswer");
            set => Preferences.Set("Sex", value);
        }
        public static string Handedness
        {
            get => Preferences.Get("Handedness", "DidNotAnswer");
            set => Preferences.Set("Handedness", value);
        }
        public static string UserType
        {
            get => Preferences.Get("UserType", "DidNotAnswer"); // Control vs Patient
            set => Preferences.Set("UserType", value);
        }
        public static string OnsetSite
        {
            get => Preferences.Get("OnsetSite", "DidNotAnswer");
            set => Preferences.Set("OnsetSite", value);
        }
        public static string OnsetDate
        {
            get => Preferences.Get("OnsetDate", "DidNotAnswer");
            set => Preferences.Set("OnsetDate", value);
        }
        public static string Diagnosis
        {
            get => Preferences.Get("Diagnosis", "DidNotAnswer");
            set => Preferences.Set("Diagnosis", value);
        }
        public static string OtherDiagnosis
        {
            get => Preferences.Get("OtherDiagnosis", "DidNotAnswer");
            set => Preferences.Set("OtherDiagnosis", value);
        }
        public static string English1Lang
        {
            get => Preferences.Get("English1Lang", "DidNotAnswer");
            set => Preferences.Set("English1Lang", value);
        }
        public static string EnglishLearnerAge
        {
            get => Preferences.Get("EnglishLearnerAge", "DidNotAnswer");
            set => Preferences.Set("EnglishLearnerAge", value);
        }
        public static string LangsSpoken
        {
            get => Preferences.Get("LangsSpoken", "DidNotAnswer");
            set => Preferences.Set("LangsSpoken", value);
        }
        public static string LangsExposed
        {
            get => Preferences.Get("LangsExposed", "DidNotAnswer");
            set => Preferences.Set("LangsExposed", value);
        }

        public App()
        {
            InitializeComponent();
            LoginVisits = 0;
            Password = "Gleason5";
            RecordedButNotSaved = false;
            IsRecording = false;
            SuccessfulGET = false;
            SuccessfulPUT = false;
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            try
            {
                if (IsPlaying)
                {
                    GlobalPlayer.Pause();
                }
                if (IsRecording)
                {
                    GlobalRecorder.StopRecording();
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(@"ERROR {0}", ex.Message);
                Console.WriteLine("Something went wrong when controlling the Global AudioRecorder instance!");
            }
        }

        protected override async void OnResume()
        {
            // No matter what happens, when the app resumes, start at the Task Page (even if in the middle of a task previously)
            // Unless the Questionnaire has yet to be completed! Or unless you are at LoginPage (Sign In/Out)
            if (CurrentPage.Equals("Login") || CurrentPage.Equals("Questionnaire") || CurrentPage.Equals("Alert"))
            {
                // Anything useful that could go here??
            }
            else
            {
                MainPage = new NavigationPage(new TaskPage());
                await MainPage.Navigation.PopToRootAsync();
            }
        }
    }
}
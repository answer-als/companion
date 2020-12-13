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
        public static bool LoggedIn
        {
            get => Preferences.Get("LoggedIn", false);
            set => Preferences.Set("LoggedIn", value);
        }
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
        public static DateTime QuestionnaireLastCompleted
        {
            get => Preferences.Get("QuestionnaireLastCompleted", DateTime.MinValue);
            set => Preferences.Set("QuestionnaireLastCompleted", value);
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
        public static int SpeechTasksRemaining
        {
            get => Preferences.Get("SpeechTasksRemaining", 3);
            set => Preferences.Set("SpeechTasksRemaining", value);
        }
        public static string Person
        {
            get => Preferences.Get("Person", "DidNotAnswer");
            set => Preferences.Set("Person", value);
        }
        public static string Speech
        {
            get => Preferences.Get("WeightUnits", "DidNotAnswer");
            set => Preferences.Set("WeightUnits", value);
        }
        public static string Salivation
        {
            get => Preferences.Get("Salivation", "DidNotAnswer");
            set => Preferences.Set("Salivation", value);
        }
        public static string Swallowing
        {
            get => Preferences.Get("Swallowing", "DidNotAnswer");
            set => Preferences.Set("Swallowing", value);
        }
        public static string Handwriting
        {
            get => Preferences.Get("Handwriting", "DidNotAnswer");
            set => Preferences.Set("Handwriting", value);
        }
        public static string CuttingFood
        {
            get => Preferences.Get("CuttingFood", "DidNotAnswer");
            set => Preferences.Set("CuttingFood", value);
        }
        public static string Dressing
        {
            get => Preferences.Get("Dressing", "DidNotAnswer");
            set => Preferences.Set("Dressing", value);
        }
        public static string TurningInBed
        {
            get => Preferences.Get("TurningInBed", "DidNotAnswer");
            set => Preferences.Set("TurningInBed", value);
        }
        public static string Walking
        {
            get => Preferences.Get("Walking", "DidNotAnswer");
            set => Preferences.Set("Walking", value);
        }
        public static string ClimbingStairs
        {
            get => Preferences.Get("ClimbingStairs", "DidNotAnswer");
            set => Preferences.Set("ClimbingStairs", value);
        }
        public static string Dyspnea
        {
            get => Preferences.Get("Dyspnea", "DidNotAnswer");
            set => Preferences.Set("Dyspnea", value);
        }
        public static string Orthopnea
        {
            get => Preferences.Get("Orthopnea", "DidNotAnswer");
            set => Preferences.Set("Orthopnea", value);
        }
        public static string RespiratoryInsufficiency
        {
            get => Preferences.Get("RespiratoryInsufficiency", "DidNotAnswer");
            set => Preferences.Set("RespiratoryInsufficiency", value);
        }
        public static bool IsCountTask
        {
            get => Preferences.Get("IsCountTask", true);
            set => Preferences.Set("IsCountTask", value);
        }
 
    public App()
        {
            InitializeComponent();
            LoginVisits = 0;
            //// Not needed for v1.2
            Password = "Gleason5";
            RecordedButNotSaved = false;
            IsRecording = false;
            SuccessfulGET = false;
            SuccessfulPUT = false;

            // Make sure that we only visit the Log In page on the first startup.
            if (LoggedIn)
            {
                
                if (App.QuestionnaireLastCompleted == DateTime.MinValue)
                {
                    App.QuestionnaireCompleted = false;
//                    await Navigation.PushAsync(new QuestionnairePage());
                    MainPage = new NavigationPage(new MainPage());
                }
                else if (DateTime.Now.CompareTo(App.QuestionnaireLastCompleted.AddMonths(1)) >= 0)
                {
                    // Require FRS task monthly
                    DateTime today = DateTime.Now;
                    DateTime dueDate = App.QuestionnaireLastCompleted.AddMonths(1);

                    // Ref https://docs.microsoft.com/en-us/dotnet/api/system.datetime.compareto?view=net-5.0
                    if (today.CompareTo(dueDate) >= 0)
                    {
                    }
                    App.QuestionnaireCompleted = false;
//                    await Navigation.PushAsync(new QuestionnairePage());
                    MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    MainPage = new NavigationPage(new TaskPage());
                }
                
                //MainPage = new NavigationPage(new TaskPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
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

        // TODO: Use this as the global option for DisplayAlert when called in the various ContentPages/Views
        //void AlertMessage(string title, string body, string accept, string cancel, string pg)
        //{
        //    CurrentPage = "Alert";
        //    if (cancel.Equals("None"))
        //    {
        //        Current.MainPage.DisplayAlert(title, body, accept);
        //    } else
        //    {
        //        Current.MainPage.DisplayAlert(title, body, accept, cancel);
        //    }
        //    CurrentPage = pg;
        //}
    }
}
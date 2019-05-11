using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView2 : ContentView
    {
        bool loaded = false; 

        public QuestionnaireView2()
        {
            InitializeComponent();

            LoadPrevious();
            YesOption.TextFontSize = 15.0;
            NoOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.EnglishLearnerAge.Equals("DidNotAnswer"))
            {
                LearningAge.Text = App.EnglishLearnerAge;
            }

            if (App.English1Lang.Equals("Yes"))
            {
                YesOption.IsChecked = true;
            }

            if (App.English1Lang.Equals("No"))
            {
                NoOption.IsChecked = true;
            }

            loaded = true;
        }

        public void SaveResponses()
        {
            if (YesOption.IsChecked)
            {
                App.English1Lang = "Yes";
                App.EnglishLearnerAge = "N/A";
                return;
            }

            if (NoOption.IsChecked)
            {
                App.English1Lang = "No";
            }

            if (!LearningAge.Text.Equals(""))
            {
                App.EnglishLearnerAge = LearningAge.Text;
            }
        }

        public bool Completed()
        {
            return YesOption.IsChecked || (NoOption.IsChecked && !LearningAge.Text.Equals(""));
        }

        void AnswerProvided(object sender, EventArgs e)
        {
            if (loaded)
            {
                QuestionnairePage rent = (QuestionnairePage)this.Parent;
                rent.HideWarning();
            }
        }
    }
}

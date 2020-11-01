using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView12 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView12()
        {
            InitializeComponent();

            LoadPrevious();
            NoneOption.TextFontSize = 15.0;
            WalkingOption.TextFontSize = 15.0;
            AdlOption.TextFontSize = 15.0;
            AtRestOption.TextFontSize = 15.0;
            SignificantDifficultyOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.DyspneaNotes.Equals("DidNotAnswer"))
            {
                DyspneaNotes.Text = App.DyspneaNotes;
            }

            if (App.Dyspnea.Equals("None"))
            {
                NoneOption.IsChecked = true;
            }
            else if (App.Dyspnea.Equals("Walking"))
            {
                WalkingOption.IsChecked = true;
            }
            else if (App.Dyspnea.Equals("Adl"))
            {
                AdlOption.IsChecked = true;
            }
            else if (App.Dyspnea.Equals("AtRest"))
            {
                AtRestOption.IsChecked = true;
            }
            else if (App.Dyspnea.Equals("SignificantDifficulty"))
            {
                SignificantDifficultyOption.IsChecked = true;
            }
            
            loaded = true;
        }

        public void SaveResponses()
        {
            if (!DyspneaNotes.Text.Equals(""))
            {
                App.DyspneaNotes = DyspneaNotes.Text;
            }

            if (NoneOption.IsChecked)
            {
                App.Dyspnea = "None";
            }
            else if (WalkingOption.IsChecked)
            {
                App.Dyspnea = "Walking";
            }
            else if (AdlOption.IsChecked)
            {
                App.Dyspnea = "AdlOption";
            }
            else if (AtRestOption.IsChecked)
            {
                App.Dyspnea = "AtRest";
            }
            else if (SignificantDifficultyOption.IsChecked)
            {
                App.Dyspnea = "SignificantDifficulty";
            }
        }

        public bool Completed()
        {
            return (NoneOption.IsChecked || WalkingOption.IsChecked || AdlOption.IsChecked || AtRestOption.IsChecked || SignificantDifficultyOption.IsChecked);
        }

        void AnswerProvided(object sender, EventArgs e)
        {
            if (loaded)
            {
                QuestionnairePage rent = (QuestionnairePage)this.Parent;
                rent.HideWarning();
            }
        }

        /*
        void RadioButton_Clicked(object sender, EventArgs e)
        {
            // Nothing
        }

        void FocusNext(object sender, EventArgs e)
        {
            ExposedLanguages.Focus();
        }
        */
    }
}

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView13 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView13()
        {
            InitializeComponent();

            LoadPrevious();
            NoneOption.TextFontSize = 15.0;
            SomeDifficultyOption.TextFontSize = 15.0;
            ExtraPillowsOption.TextFontSize = 15.0;
            SittingUpOption.TextFontSize = 15.0;
            UnableOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.OrthopneaNotes.Equals("DidNotAnswer"))
            {
                OrthopneaNotes.Text = App.OrthopneaNotes;
            }

            if (App.Orthopnea.Equals("None"))
            {
                NoneOption.IsChecked = true;
            }
            else if (App.Orthopnea.Equals("SomeDifficulty"))
            {
                SomeDifficultyOption.IsChecked = true;
            }
            else if (App.Orthopnea.Equals("ExtraPillows"))
            {
                ExtraPillowsOption.IsChecked = true;
            }
            else if (App.Orthopnea.Equals("SittingUp"))
            {
                SittingUpOption.IsChecked = true;
            }
            else if (App.Orthopnea.Equals("Unable"))
            {
                UnableOption.IsChecked = true;
            }
            
            loaded = true;
        }

        public void SaveResponses()
        {
            if (!OrthopneaNotes.Text.Equals(""))
            {
                App.OrthopneaNotes = OrthopneaNotes.Text;
            }

            if (NoneOption.IsChecked)
            {
                App.Orthopnea = "None";
            }
            else if (SomeDifficultyOption.IsChecked)
            {
                App.Orthopnea = "SomeDifficulty";
            }
            else if (ExtraPillowsOption.IsChecked)
            {
                App.Orthopnea = "ExtraPillows";
            }
            else if (SittingUpOption.IsChecked)
            {
                App.Orthopnea = "SittingUp";
            }
            else if (UnableOption.IsChecked)
            {
                App.Orthopnea = "Unable";
            }
        }

        public bool Completed()
        {
            return (NoneOption.IsChecked || SomeDifficultyOption.IsChecked || ExtraPillowsOption.IsChecked || SittingUpOption.IsChecked || UnableOption.IsChecked);
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

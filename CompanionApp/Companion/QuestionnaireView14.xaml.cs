using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView14 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView14()
        {
            InitializeComponent();

            LoadPrevious();
            NoneOption.TextFontSize = 15.0;
            IntermittentOption.TextFontSize = 15.0;
            ContinuousOption.TextFontSize = 15.0;
            NightAndDayOption.TextFontSize = 15.0;
            MechanicalOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.RespiratoryInsufficiencyNotes.Equals("DidNotAnswer"))
            {
                RespiratoryInsufficiencyNotes.Text = App.RespiratoryInsufficiencyNotes;
            }

            if (App.RespiratoryInsufficiency.Equals("None"))
            {
                NoneOption.IsChecked = true;
            }
            else if (App.RespiratoryInsufficiency.Equals("Intermittent"))
            {
                IntermittentOption.IsChecked = true;
            }
            else if (App.RespiratoryInsufficiency.Equals("Continuous"))
            {
                ContinuousOption.IsChecked = true;
            }
            else if (App.RespiratoryInsufficiency.Equals("NightAndDay"))
            {
                NightAndDayOption.IsChecked = true;
            }
            else if (App.RespiratoryInsufficiency.Equals("Mechanical"))
            {
                MechanicalOption.IsChecked = true;
            }
            
            loaded = true;
        }

        public void SaveResponses()
        {
            if (!RespiratoryInsufficiencyNotes.Text.Equals(""))
            {
                App.RespiratoryInsufficiencyNotes = RespiratoryInsufficiencyNotes.Text;
            }

            if (NoneOption.IsChecked)
            {
                App.RespiratoryInsufficiency = "None";
            }
            else if (IntermittentOption.IsChecked)
            {
                App.RespiratoryInsufficiency = "Intermittent";
            }
            else if (ContinuousOption.IsChecked)
            {
                App.RespiratoryInsufficiency = "Continuous";
            }
            else if (NightAndDayOption.IsChecked)
            {
                App.RespiratoryInsufficiency = "NightAndDay";
            }
            else if (MechanicalOption.IsChecked)
            {
                App.RespiratoryInsufficiency = "Mechanical";
            }
        }

        public bool Completed()
        {
            return (NoneOption.IsChecked || IntermittentOption.IsChecked || ContinuousOption.IsChecked || NightAndDayOption.IsChecked || MechanicalOption.IsChecked);
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

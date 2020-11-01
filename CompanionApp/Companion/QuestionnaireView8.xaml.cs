using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView8 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView8()
        {
            InitializeComponent();

            LoadPrevious();
            NormalOption.TextFontSize = 15.0;
            IndependentOption.TextFontSize = 15.0;
            IntermittentAssistanceOption.TextFontSize = 15.0;
            NeedsAttendantOption.TextFontSize = 15.0;
            TotalDependenceOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.DressingNotes.Equals("DidNotAnswer"))
            {
                DressingNotes.Text = App.DressingNotes;
            }

            if (App.Dressing.Equals("Normal"))
            {
                NormalOption.IsChecked = true;
            }
            else if (App.Dressing.Equals("Independent"))
            {
                IndependentOption.IsChecked = true;
            }
            else if (App.Dressing.Equals("IntermittentAssistance"))
            {
                IntermittentAssistanceOption.IsChecked = true;
            }
            else if (App.Dressing.Equals("NeedsAttendant"))
            {
                NeedsAttendantOption.IsChecked = true;
            }
            else if (App.Dressing.Equals("TotalDependence"))
            {
                TotalDependenceOption.IsChecked = true;
            }
            
            loaded = true;
        }

        public void SaveResponses()
        {
            if (!DressingNotes.Text.Equals(""))
            {
                App.DressingNotes = DressingNotes.Text;
            }

            if (NormalOption.IsChecked)
            {
                App.Dressing = "Normal";
            }
            else if (IndependentOption.IsChecked)
            {
                App.Dressing = "Independent";
            }
            else if (IntermittentAssistanceOption.IsChecked)
            {
                App.Dressing = "IntermittentAssistance";
            }
            else if (NeedsAttendantOption.IsChecked)
            {
                App.Dressing = "NeedsAttendant";
            }
            else if (TotalDependenceOption.IsChecked)
            {
                App.Dressing = "TotalDependence";
            }
        }

        public bool Completed()
        {
            return (NormalOption.IsChecked || IndependentOption.IsChecked || IntermittentAssistanceOption.IsChecked || NeedsAttendantOption.IsChecked || TotalDependenceOption.IsChecked);
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

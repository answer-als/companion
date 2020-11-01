using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView4 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView4()
        {
            InitializeComponent();

            LoadPrevious();
            NormalOption.TextFontSize = 15.0;
            SlightOption.TextFontSize = 15.0;
            ModerateOption.TextFontSize = 15.0;
            MarkedExcessOption.TextFontSize = 15.0;
            MarkedDroolingOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.SalivationNotes.Equals("DidNotAnswer"))
            {
                SalivationNotes.Text = App.SalivationNotes;
            }

            if (App.Salivation.Equals("Normal"))
            {
                NormalOption.IsChecked = true;
            }
            else if (App.Salivation.Equals("Slight"))
            {
                SlightOption.IsChecked = true;
            }
            else if (App.Salivation.Equals("Moderate"))
            {
                ModerateOption.IsChecked = true;
            }
            else if (App.Salivation.Equals("MarkedExcess"))
            {
                MarkedExcessOption.IsChecked = true;
            }
            else if (App.Salivation.Equals("MarkedDrooling"))
            {
                MarkedDroolingOption.IsChecked = true;
            }

            loaded = true;
        }

        public void SaveResponses()
        {
            if (!SalivationNotes.Text.Equals(""))
            {
                App.SalivationNotes = SalivationNotes.Text;
            }

            if (NormalOption.IsChecked)
            {
                App.Salivation = "Normal";
            }
            else if (SlightOption.IsChecked)
            {
                App.Salivation = "Slight";
            }
            else if (ModerateOption.IsChecked)
            {
                App.Salivation = "Moderate";
            }
            else if (MarkedExcessOption.IsChecked)
            {
                App.Salivation = "MarkedExcess";
            }
            else if (MarkedDroolingOption.IsChecked)
            {
                App.Salivation = "MarkedDrooling";
            }
        }

        public bool Completed()
        {
            return (NormalOption.IsChecked || SlightOption.IsChecked || ModerateOption.IsChecked || MarkedExcessOption.IsChecked || MarkedDroolingOption.IsChecked);
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

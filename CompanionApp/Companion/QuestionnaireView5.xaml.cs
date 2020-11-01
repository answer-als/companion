using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView5 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView5()
        {
            InitializeComponent();

            LoadPrevious();
            NormalOption.TextFontSize = 15.0;
            EarlyOption.TextFontSize = 15.0;
            DietaryOption.TextFontSize = 15.0;
            SupplementalOption.TextFontSize = 15.0;
            NpoOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.SwallowingNotes.Equals("DidNotAnswer"))
            {
                SwallowingNotes.Text = App.SwallowingNotes;
            }

            if (App.Swallowing.Equals("Normal"))
            {
                NormalOption.IsChecked = true;
            }
            else if (App.Swallowing.Equals("Early"))
            {
                EarlyOption.IsChecked = true;
            }
            else if (App.Swallowing.Equals("Dietary"))
            {
                DietaryOption.IsChecked = true;
            }
            else if (App.Swallowing.Equals("Supplemental"))
            {
                SupplementalOption.IsChecked = true;
            }
            else if (App.Swallowing.Equals("NPO"))
            {
                NpoOption.IsChecked = true;
            }

            loaded = true;
        }

        public void SaveResponses()
        {
            if (!SwallowingNotes.Text.Equals(""))
            {
                App.SwallowingNotes = SwallowingNotes.Text;
            }

            if (NormalOption.IsChecked)
            {
                App.Swallowing = "Normal";
            }
            else if (EarlyOption.IsChecked)
            {
                App.Salivation = "Early";
            }
            else if (DietaryOption.IsChecked)
            {
                App.Salivation = "Dietary";
            }
            else if (SupplementalOption.IsChecked)
            {
                App.Salivation = "Supplemental";
            }
            else if (NpoOption.IsChecked)
            {
                App.Salivation = "NPO";
            }
        }

        public bool Completed()
        {
            return (NormalOption.IsChecked || EarlyOption.IsChecked || DietaryOption.IsChecked || SupplementalOption.IsChecked || NpoOption.IsChecked);
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

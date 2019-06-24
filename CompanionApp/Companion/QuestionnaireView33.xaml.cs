using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView33 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView33()
        {
            InitializeComponent();

            LoadPrevious();
            ALSOption.TextFontSize = 15.0;
            PLSOption.TextFontSize = 15.0;
            OtherOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (App.Diagnosis.Equals("ALS"))
            {
                ALSOption.IsChecked = true;
                OtherDiagnosis.Text = "N/A";
            }

            if (App.Diagnosis.Equals("PLS"))
            {
                PLSOption.IsChecked = true;
                OtherDiagnosis.Text = "N/A";
            }

            if (App.Diagnosis.Equals("Other"))
            {
                OtherOption.IsChecked = true;
            }

            if (!App.OtherDiagnosis.Equals("DidNotAnswer"))
            {
                OtherDiagnosis.Text = App.OtherDiagnosis;
            }

            loaded = true;
        }

        public void SaveResponses()
        {
            if (ALSOption.IsChecked)
            {
                App.Diagnosis = "ALS";
                App.OtherDiagnosis = "N/A";
                return;
            }

            if (PLSOption.IsChecked)
            {
                App.Diagnosis = "PLS";
                App.OtherDiagnosis = "N/A";
            }

            if (OtherOption.IsChecked)
            {
                App.Diagnosis = "Other";
            }

            if (!OtherDiagnosis.Text.Equals(""))
            {
                App.OtherDiagnosis = OtherDiagnosis.Text;
            }
        }

        public bool Completed()
        {
            return ALSOption.IsChecked || PLSOption.IsChecked || (OtherOption.IsChecked && !OtherDiagnosis.Text.Equals(""));
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

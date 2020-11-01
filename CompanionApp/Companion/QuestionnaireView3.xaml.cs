using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView3 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView3()
        {
            InitializeComponent();

            LoadPrevious();
            NormalOption.TextFontSize = 15.0;
            DetectableOption.TextFontSize = 15.0;
            IntelligibleOption.TextFontSize = 15.0;
            NonVocalOption.TextFontSize = 15.0;
            LossOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.SpeechNotes.Equals("DidNotAnswer"))
            {
                SpeechNotes.Text = App.SpeechNotes;
            }

            if (App.Speech.Equals("Normal"))
            {
                NormalOption.IsChecked = true;
            }
            else if (App.Speech.Equals("Detectable"))
            {
                DetectableOption.IsChecked = true;
            }
            else if (App.Speech.Equals("Intelligible"))
            {
                IntelligibleOption.IsChecked = true;
            }
            else if (App.Speech.Equals("NonVocal"))
            {
                NonVocalOption.IsChecked = true;
            }
            else if (App.Speech.Equals("Loss"))
            {
                LossOption.IsChecked = true;
            }

            loaded = true;
        }

        public void SaveResponses()
        {
            if (NormalOption.IsChecked)
            {
                App.Speech = "Normal";
            }
            else if (DetectableOption.IsChecked)
            {
                App.Speech = "Detectable";
            }
            else if (IntelligibleOption.IsChecked)
            {
                App.Speech = "Intelligible";
            }
            else if (NonVocalOption.IsChecked)
            {
                App.Speech = "NonVocal";
            }
            else if (LossOption.IsChecked)
            {
                App.Speech = "Loss";
            }

            if (!SpeechNotes.Equals("DidNotAnswer"))
            {
                App.SpeechNotes = SpeechNotes.Text;
            }
        }

        public bool Completed()
        {
            return (NormalOption.IsChecked || DetectableOption.IsChecked || IntelligibleOption.IsChecked || NonVocalOption.IsChecked || LossOption.IsChecked);
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

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView6 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView6()
        {
            InitializeComponent();

            LoadPrevious();
            NormalOption.TextFontSize = 15.0;
            SlowOption.TextFontSize = 15.0;
            NotAllLegibleOption.TextFontSize = 15.0;
            AbleToGripOption.TextFontSize = 15.0;
            UnableToGripOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.HandwritingNotes.Equals("DidNotAnswer"))
            {
                HandwritingNotes.Text = App.HandwritingNotes;
            }

            if (App.Handwriting.Equals("Normal"))
            {
                NormalOption.IsChecked = true;
            }
            else if (App.Handwriting.Equals("Slow"))
            {
                SlowOption.IsChecked = true;
            }
            else if (App.Handwriting.Equals("NotAllLegible"))
            {
                NotAllLegibleOption.IsChecked = true;
            }
            else if (App.Handwriting.Equals("AbleToGrip"))
            {
                AbleToGripOption.IsChecked = true;
            }
            else if (App.Handwriting.Equals("UnableToGrip"))
            {
                UnableToGripOption.IsChecked = true;
            }
            
            loaded = true;
        }

        public void SaveResponses()
        {
            if (!HandwritingNotes.Text.Equals(""))
            {
                App.HandwritingNotes = HandwritingNotes.Text;
            }

            if (NormalOption.IsChecked)
            {
                App.Handwriting = "Normal";
            }
            else if (SlowOption.IsChecked)
            {
                App.Handwriting = "Slow";
            }
            else if (NotAllLegibleOption.IsChecked)
            {
                App.Handwriting = "NotAllLegible";
            }
            else if (AbleToGripOption.IsChecked)
            {
                App.Handwriting = "AbleToGrip";
            }
            else if (UnableToGripOption.IsChecked)
            {
                App.Handwriting = "UnableToGrip";
            }
        }

        public bool Completed()
        {
            return (NormalOption.IsChecked || SlowOption.IsChecked || NotAllLegibleOption.IsChecked || AbleToGripOption.IsChecked || UnableToGripOption.IsChecked);
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

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView9 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView9()
        {
            InitializeComponent();

            LoadPrevious();
            NormalOption.TextFontSize = 15.0;
            SlowOption.TextFontSize = 15.0;
            TurnAloneOption.TextFontSize = 15.0;
            InitiateOption.TextFontSize = 15.0;
            HelplessOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.TurningInBedNotes.Equals("DidNotAnswer"))
            {
                TurningInBedNotes.Text = App.TurningInBedNotes;
            }

            if (App.TurningInBed.Equals("Normal"))
            {
                NormalOption.IsChecked = true;
            }
            else if (App.Dressing.Equals("Slow"))
            {
                SlowOption.IsChecked = true;
            }
            else if (App.Dressing.Equals("TurnAlone"))
            {
                TurnAloneOption.IsChecked = true;
            }
            else if (App.Dressing.Equals("Initiate"))
            {
                InitiateOption.IsChecked = true;
            }
            else if (App.Dressing.Equals("Helpless"))
            {
                HelplessOption.IsChecked = true;
            }
            
            loaded = true;
        }

        public void SaveResponses()
        {
            if (!TurningInBedNotes.Text.Equals(""))
            {
                App.TurningInBedNotes = TurningInBedNotes.Text;
            }

            if (NormalOption.IsChecked)
            {
                App.Dressing = "Normal";
            }
            else if (SlowOption.IsChecked)
            {
                App.Dressing = "Slow";
            }
            else if (TurnAloneOption.IsChecked)
            {
                App.Dressing = "TurnAlone";
            }
            else if (InitiateOption.IsChecked)
            {
                App.Dressing = "Initiate";
            }
            else if (HelplessOption.IsChecked)
            {
                App.Dressing = "Helpless";
            }
        }

        public bool Completed()
        {
            return (NormalOption.IsChecked || SlowOption.IsChecked || TurnAloneOption.IsChecked || InitiateOption.IsChecked || HelplessOption.IsChecked);
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

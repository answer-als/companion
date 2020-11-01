using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView10 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView10()
        {
            InitializeComponent();

            LoadPrevious();
            NormalOption.TextFontSize = 15.0;
            EarlyAmbulationOption.TextFontSize = 15.0;
            AssistanceOption.TextFontSize = 15.0;
            NonAmbulatoryOption.TextFontSize = 15.0;
            NoMovementOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.WalkingNotes.Equals("DidNotAnswer"))
            {
                WalkingNotes.Text = App.WalkingNotes;
            }

            if (App.Walking.Equals("Normal"))
            {
                NormalOption.IsChecked = true;
            }
            else if (App.Walking.Equals("EarlyAmbulation"))
            {
                EarlyAmbulationOption.IsChecked = true;
            }
            else if (App.Walking.Equals("Assistance"))
            {
                AssistanceOption.IsChecked = true;
            }
            else if (App.Walking.Equals("NonAmbulatory"))
            {
                NonAmbulatoryOption.IsChecked = true;
            }
            else if (App.Walking.Equals("NoMovement"))
            {
                NoMovementOption.IsChecked = true;
            }
            
            loaded = true;
        }

        public void SaveResponses()
        {
            if (!WalkingNotes.Text.Equals(""))
            {
                App.WalkingNotes = WalkingNotes.Text;
            }

            if (NormalOption.IsChecked)
            {
                App.Walking = "Normal";
            }
            else if (EarlyAmbulationOption.IsChecked)
            {
                App.Walking = "EarlyAmbulation";
            }
            else if (AssistanceOption.IsChecked)
            {
                App.Walking = "Assistance";
            }
            else if (NonAmbulatoryOption.IsChecked)
            {
                App.Walking = "NonAmbulatory";
            }
            else if (NoMovementOption.IsChecked)
            {
                App.Walking = "NoMovement";
            }
        }

        public bool Completed()
        {
            return (NormalOption.IsChecked || EarlyAmbulationOption.IsChecked || AssistanceOption.IsChecked || NonAmbulatoryOption.IsChecked || NoMovementOption.IsChecked);
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

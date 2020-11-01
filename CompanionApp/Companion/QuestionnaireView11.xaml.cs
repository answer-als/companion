using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView11 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView11()
        {
            InitializeComponent();

            LoadPrevious();
            NormalOption.TextFontSize = 15.0;
            SlowOption.TextFontSize = 15.0;
            MildOption.TextFontSize = 15.0;
            AssistanceOption.TextFontSize = 15.0;
            CannotOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.ClimbingStairsNotes.Equals("DidNotAnswer"))
            {
                ClimbingStairsNotes.Text = App.ClimbingStairsNotes;
            }

            if (App.ClimbingStairs.Equals("Normal"))
            {
                NormalOption.IsChecked = true;
            }
            else if (App.ClimbingStairs.Equals("Slow"))
            {
                SlowOption.IsChecked = true;
            }
            else if (App.ClimbingStairs.Equals("Mild"))
            {
                MildOption.IsChecked = true;
            }
            else if (App.ClimbingStairs.Equals("Assistance"))
            {
                AssistanceOption.IsChecked = true;
            }
            else if (App.ClimbingStairs.Equals("Cannot"))
            {
                CannotOption.IsChecked = true;
            }
            
            loaded = true;
        }

        public void SaveResponses()
        {
            if (!ClimbingStairsNotes.Text.Equals(""))
            {
                App.ClimbingStairsNotes = ClimbingStairsNotes.Text;
            }

            if (NormalOption.IsChecked)
            {
                App.ClimbingStairs = "Normal";
            }
            else if (SlowOption.IsChecked)
            {
                App.ClimbingStairs = "Slow";
            }
            else if (AssistanceOption.IsChecked)
            {
                App.ClimbingStairs = "Assistance";
            }
            else if (AssistanceOption.IsChecked)
            {
                App.ClimbingStairs = "Assistance";
            }
            else if (CannotOption.IsChecked)
            {
                App.ClimbingStairs = "Cannot";
            }
        }

        public bool Completed()
        {
            return (NormalOption.IsChecked || SlowOption.IsChecked || MildOption.IsChecked || AssistanceOption.IsChecked || CannotOption.IsChecked);
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

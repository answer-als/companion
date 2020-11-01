using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView7 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView7()
        {
            InitializeComponent();

            LoadPrevious();
            NormalOption.TextFontSize = 15.0;
            SlowOption.TextFontSize = 15.0;
            CutMostOption.TextFontSize = 15.0;
            CutBySomeoneOption.TextFontSize = 15.0;
            NeedsToBeFedOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.CuttingFoodNotes.Equals("DidNotAnswer"))
            {
                CuttingFoodNotes.Text = App.CuttingFoodNotes;
            }

            if (App.CuttingFood.Equals("Normal"))
            {
                NormalOption.IsChecked = true;
            }
            else if (App.CuttingFood.Equals("Slow"))
            {
                SlowOption.IsChecked = true;
            }
            else if (App.CuttingFood.Equals("CutMost"))
            {
                CutMostOption.IsChecked = true;
            }
            else if (App.CuttingFood.Equals("CutBySomeone"))
            {
                CutBySomeoneOption.IsChecked = true;
            }
            else if (App.CuttingFood.Equals("NeedsToBeFed"))
            {
                NeedsToBeFedOption.IsChecked = true;
            }
            
            loaded = true;
        }

        public void SaveResponses()
        {
            if (!CuttingFoodNotes.Text.Equals(""))
            {
                App.CuttingFoodNotes = CuttingFoodNotes.Text;
            }

            if (NormalOption.IsChecked)
            {
                App.CuttingFood = "Normal";
            }
            else if (SlowOption.IsChecked)
            {
                App.CuttingFood = "Slow";
            }
            else if (CutMostOption.IsChecked)
            {
                App.CuttingFood = "CutMost";
            }
            else if (CutBySomeoneOption.IsChecked)
            {
                App.CuttingFood = "CutBySomeone";
            }
            else if (NeedsToBeFedOption.IsChecked)
            {
                App.CuttingFood = "NeedsToBeFed";
            }
        }

        public bool Completed()
        {
            return (NormalOption.IsChecked || SlowOption.IsChecked || CutMostOption.IsChecked || CutBySomeoneOption.IsChecked || NeedsToBeFedOption.IsChecked);
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

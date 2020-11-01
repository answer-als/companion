using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView2 : ContentView
    {
        bool loaded = false; 

        public QuestionnaireView2()
        {
            InitializeComponent();

            LoadPrevious();
            KgOption.TextFontSize = 15.0;
            LbOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.WeightNotes.Equals("DidNotAnswer"))
            {
                WeightNotes.Text = App.WeightNotes;
            }

            if (!App.Weight.Equals("DidNotAnswer"))
            {
                Weight.Text = App.Weight;
            }

            if (App.WeightUnits.Equals("Kg"))
            {
                KgOption.IsChecked = true;
            }
            else if (App.WeightUnits.Equals("Lb"))
            {
                LbOption.IsChecked = true;
            }

            loaded = true;
        }

        public void SaveResponses()
        {
            if (KgOption.IsChecked)
            {
                App.WeightUnits = "Kg";
            }
            else if (LbOption.IsChecked)
            {
                App.WeightUnits = "Lb";
            }

            if (!Weight.Text.Equals(""))
            {
                App.Weight = Weight.Text;
            }

            if (!WeightNotes.Text.Equals(""))
            {
                App.WeightNotes = WeightNotes.Text;
            }
        }

        public bool Completed()
        {
            return KgOption.IsChecked || (LbOption.IsChecked && !Weight.Text.Equals(""));
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

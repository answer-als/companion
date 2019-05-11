using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView22 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView22()
        {
            InitializeComponent();

            LoadPrevious();
            BulbarOption.TextFontSize = 15.0;
            SpinalOption.TextFontSize = 15.0;
            BothOption.TextFontSize = 15.0;
            NoneOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (App.OnsetSite.Equals("Bulbar"))
            {
                BulbarOption.IsChecked = true;
            }

            if (App.OnsetSite.Equals("Spinal"))
            {
                SpinalOption.IsChecked = true;
            }

            if (App.OnsetSite.Equals("Spinal/Bulbar"))
            {
                BothOption.IsChecked = true;
            }

            if (App.OnsetSite.Equals("Control"))
            {
                NoneOption.IsChecked = true;
            }

            loaded = true;
        }

        public void SaveResponses()
        {
            if (SpinalOption.IsChecked)
            {
                App.OnsetSite = "Spinal";
            }

            if (BulbarOption.IsChecked)
            {
                App.OnsetSite = "Bulbar";
            }

            if (BothOption.IsChecked)
            {
                App.OnsetSite = "Spinal/Bulbar";
            }

            if (NoneOption.IsChecked)
            {
                App.OnsetSite = "Control";
            }
        }

        public bool Completed()
        {
            return SpinalOption.IsChecked || BulbarOption.IsChecked || BothOption.IsChecked || NoneOption.IsChecked;
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

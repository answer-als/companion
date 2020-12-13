using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView5 : ContentView
    {
        public QuestionnaireView5()
        {
            InitializeComponent();

            NormalOption.TextFontSize = 15.0;
            EarlyOption.TextFontSize = 15.0;
            DietaryOption.TextFontSize = 15.0;
            SupplementalOption.TextFontSize = 15.0;
            NpoOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.Swallowing = groupView.SelectedItem.ToString();
        }

        public bool Completed()
        {
            return groupView.SelectedItem != null;
        }

        void AnswerProvided(object sender, EventArgs e)
        {
            QuestionnairePage rent = (QuestionnairePage)this.Parent;
            rent.HideWarning();
        }
    }
}

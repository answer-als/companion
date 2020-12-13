using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView12 : ContentView
    {
        public QuestionnaireView12()
        {
            InitializeComponent();

            NoneOption.TextFontSize = 15.0;
            WalkingOption.TextFontSize = 15.0;
            AdlOption.TextFontSize = 15.0;
            AtRestOption.TextFontSize = 15.0;
            SignificantDifficultyOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.Dyspnea = groupView.SelectedItem.ToString();
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

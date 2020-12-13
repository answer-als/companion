using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView13 : ContentView
    {
        public QuestionnaireView13()
        {
            InitializeComponent();

            NoneOption.TextFontSize = 15.0;
            SomeDifficultyOption.TextFontSize = 15.0;
            ExtraPillowsOption.TextFontSize = 15.0;
            SittingUpOption.TextFontSize = 15.0;
            UnableOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.Orthopnea = groupView.SelectedItem.ToString();
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

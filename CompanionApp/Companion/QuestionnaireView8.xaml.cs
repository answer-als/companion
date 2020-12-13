using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView8 : ContentView
    {
        public QuestionnaireView8()
        {
            InitializeComponent();

            NormalOption.TextFontSize = 15.0;
            IndependentOption.TextFontSize = 15.0;
            IntermittentAssistanceOption.TextFontSize = 15.0;
            NeedsAttendantOption.TextFontSize = 15.0;
            TotalDependenceOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.Dressing = groupView.SelectedItem.ToString();
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

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView14 : ContentView
    {
        public QuestionnaireView14()
        {
            InitializeComponent();

            NoneOption.TextFontSize = 15.0;
            IntermittentOption.TextFontSize = 15.0;
            ContinuousOption.TextFontSize = 15.0;
            NightAndDayOption.TextFontSize = 15.0;
            MechanicalOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.RespiratoryInsufficiency = groupView.SelectedItem.ToString();
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

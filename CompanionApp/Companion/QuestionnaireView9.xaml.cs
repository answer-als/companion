using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView9 : ContentView
    {
        public QuestionnaireView9()
        {
            InitializeComponent();

            NormalOption.TextFontSize = 15.0;
            SlowOption.TextFontSize = 15.0;
            TurnAloneOption.TextFontSize = 15.0;
            InitiateOption.TextFontSize = 15.0;
            HelplessOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.TurningInBed = groupView.SelectedItem.ToString();
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

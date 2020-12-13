using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView10 : ContentView
    {
        public QuestionnaireView10()
        {
            InitializeComponent();

            NormalOption.TextFontSize = 15.0;
            EarlyAmbulationOption.TextFontSize = 15.0;
            AssistanceOption.TextFontSize = 15.0;
            NonAmbulatoryOption.TextFontSize = 15.0;
            NoMovementOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.Walking = groupView.SelectedItem.ToString();
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

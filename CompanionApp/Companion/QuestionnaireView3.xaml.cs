using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView3 : ContentView
    {
        public QuestionnaireView3()
        {
            InitializeComponent();

            NormalOption.TextFontSize = 15.0;
            DetectableOption.TextFontSize = 15.0;
            IntelligibleOption.TextFontSize = 15.0;
            NonVocalOption.TextFontSize = 15.0;
            LossOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.Speech = groupView.SelectedItem.ToString();
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

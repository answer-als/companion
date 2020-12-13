using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView4 : ContentView
    {
        public QuestionnaireView4()
        {
            InitializeComponent();

            NormalOption.TextFontSize = 15.0;
            SlightOption.TextFontSize = 15.0;
            ModerateOption.TextFontSize = 15.0;
            MarkedExcessOption.TextFontSize = 15.0;
            MarkedDroolingOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.Salivation = groupView.SelectedItem.ToString();
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

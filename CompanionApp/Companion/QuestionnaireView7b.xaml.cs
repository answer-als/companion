using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView7b : ContentView
    {
        public QuestionnaireView7b()
        {
            InitializeComponent();

            NAOption.TextFontSize = 15.0;
            NormalOption.TextFontSize = 15.0;
            SlowOption.TextFontSize = 15.0;
            CutMostOption.TextFontSize = 15.0;
            CutBySomeoneOption.TextFontSize = 15.0;
            NeedsToBeFedOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.CuttingFood_b = groupView.SelectedItem.ToString();
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

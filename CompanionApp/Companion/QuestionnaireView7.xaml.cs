using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView7 : ContentView
    {
        public QuestionnaireView7()
        {
            InitializeComponent();

            NormalOption.TextFontSize = 15.0;
            SlowOption.TextFontSize = 15.0;
            CutMostOption.TextFontSize = 15.0;
            CutBySomeoneOption.TextFontSize = 15.0;
            NeedsToBeFedOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.CuttingFood = groupView.SelectedItem.ToString();
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

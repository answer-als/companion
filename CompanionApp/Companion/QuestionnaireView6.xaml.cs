using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView6 : ContentView
    {
        public QuestionnaireView6()
        {
            InitializeComponent();

            NormalOption.TextFontSize = 15.0;
            SlowOption.TextFontSize = 15.0;
            NotAllLegibleOption.TextFontSize = 15.0;
            AbleToGripOption.TextFontSize = 15.0;
            UnableToGripOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.Handwriting = groupView.SelectedItem.ToString();
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

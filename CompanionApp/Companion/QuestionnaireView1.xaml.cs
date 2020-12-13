using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView1 : ContentView
    {
        public QuestionnaireView1()
        {
            InitializeComponent();

            PersonOption.TextFontSize = 15.0;
            FamilyOption.TextFontSize = 15.0;
            CaregiverOption.TextFontSize = 15.0;
            NurseOption.TextFontSize = 15.0;
            PhysicanOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.Person = groupView.SelectedItem.ToString();
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

﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView11 : ContentView
    {
        public QuestionnaireView11()
        {
            InitializeComponent();

            NormalOption.TextFontSize = 15.0;
            SlowOption.TextFontSize = 15.0;
            MildOption.TextFontSize = 15.0;
            AssistanceOption.TextFontSize = 15.0;
            CannotOption.TextFontSize = 15.0;
        }

        public void SaveResponses()
        {
            App.ClimbingStairs = groupView.SelectedItem.ToString();
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

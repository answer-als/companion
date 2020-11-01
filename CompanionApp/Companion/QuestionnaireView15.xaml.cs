using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView15 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView15()
        {
            InitializeComponent();

            LoadPrevious();
        }

        void LoadPrevious()
        {            
            loaded = true;
        }

        public void SaveResponses()
        {
        }

        public bool Completed()
        {
            return true;
        }

        void AnswerProvided(object sender, EventArgs e)
        {
            if (loaded)
            {
                QuestionnairePage rent = (QuestionnairePage)this.Parent;
                rent.HideWarning();
            }
        }

        /*
        void RadioButton_Clicked(object sender, EventArgs e)
        {
            // Nothing
        }

        void FocusNext(object sender, EventArgs e)
        {
            ExposedLanguages.Focus();
        }
        */
    }
}

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView3 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView3()
        {
            InitializeComponent();

            LoadPrevious();
            RightOption.TextFontSize = 15.0;
            LeftOption.TextFontSize = 15.0;
            ControlOption.TextFontSize = 15.0;
            PatientOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (App.Handedness.Equals("Right"))
            {
                RightOption.IsChecked = true;
            }

            if (App.Handedness.Equals("Left"))
            {
                LeftOption.IsChecked = true;
            }

            if (App.UserType.Equals("Control"))
            {
                ControlOption.IsChecked = true;
            }

            if (App.UserType.Equals("Patient"))
            {
                PatientOption.IsChecked = true;
            }

            loaded = true;
        }

        public void SaveResponses()
        {
            if (RightOption.IsChecked)
            {
                App.Handedness = "Right";
            }

            if (LeftOption.IsChecked)
            {
                App.Handedness = "Left";
            }

            if (ControlOption.IsChecked)
            {
                App.UserType = "Control";
            }

            if (PatientOption.IsChecked)
            {
                App.UserType = "Patient";
            }
        }

        public bool Completed()
        {
            return (RightOption.IsChecked || LeftOption.IsChecked) && (ControlOption.IsChecked || PatientOption.IsChecked);
        }


        void AnswerProvided(object sender, EventArgs e)
        {
            if (loaded)
            {
                QuestionnairePage rent = (QuestionnairePage)this.Parent;
                rent.HideWarning();
            }
        }
    }
}

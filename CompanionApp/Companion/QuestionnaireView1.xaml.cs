using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView1 : ContentView
    {
        bool loaded = false;

        public QuestionnaireView1()
        {
            InitializeComponent();

            LoadPrevious();
            MaleOption.TextFontSize = 15.0;
            FemaleOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            if (!App.EducationLevel.Equals("DidNotAnswer"))
            {
                EducationLevel.Text = App.EducationLevel;
            }

            if (!App.BirthYear.Equals("DidNotAnswer"))
            {
                BirthYear.Text = App.BirthYear;
            }

            if (App.Sex.Equals("Male"))
            {
                MaleOption.IsChecked = true;
            }

            if (App.Sex.Equals("Female"))
            {
                FemaleOption.IsChecked = true;
            }

            loaded = true;
        }

        public void SaveResponses()
        {
            if (!EducationLevel.Text.Equals(""))
            {
                App.EducationLevel = EducationLevel.Text;
            }

            if (!BirthYear.Text.Equals(""))
            {
                App.BirthYear = BirthYear.Text;
            }

            if (FemaleOption.IsChecked)
            {
                App.Sex = "Female";
            }

            if (MaleOption.IsChecked)
            {
                App.Sex = "Male";
            }
        }

        public bool Completed()
        {
            return (MaleOption.IsChecked || FemaleOption.IsChecked) && !BirthYear.Text.Equals("") && !EducationLevel.Text.Equals("");
        }


        void AnswerProvided(object sender, EventArgs e)
        {
            if (loaded)
            {
                QuestionnairePage rent = (QuestionnairePage)this.Parent;
                rent.HideWarning();
            }
        }

        void FocusNext(object sender, EventArgs e)
        {
            BirthYear.Focus();
        }
    }
}

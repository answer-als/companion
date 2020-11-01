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
            PersonOption.TextFontSize = 15.0;
            FamilyOption.TextFontSize = 15.0;
        }

        void LoadPrevious()
        {
            /*
            if (!App.EducationLevel.Equals("DidNotAnswer"))
            {
                EducationLevel.Text = App.EducationLevel;
            }

            if (!App.BirthYear.Equals("DidNotAnswer"))
            {
                BirthYear.Text = App.BirthYear;
            }
            */
            if (!App.PersonNotes.Equals("DidNotAnswer"))
            {
                PersonNotes.Text = App.PersonNotes;
            }

            if (App.Person.Equals("Person"))
            {
                PersonOption.IsChecked = true;
            }
            else if (App.Person.Equals("Family"))
            {
                FamilyOption.IsChecked = true;
            }
            else if (App.Person.Equals("Caregiver"))
            {
                CaregiverOption.IsChecked = true;
            }
            else if (App.Person.Equals("Nurse"))
            {
                NurseOption.IsChecked = true;
            }
            else if (App.Person.Equals("Physican"))
            {
                PhysicanOption.IsChecked = true;
            }

            loaded = true;
        }

        public void SaveResponses()
        {
            /*
            if (!EducationLevel.Text.Equals(""))
            {
                App.EducationLevel = EducationLevel.Text;
            }
            */
            if (!PersonNotes.Text.Equals(""))
            {
                App.PersonNotes = PersonNotes.Text;
            }
            
            if (PersonOption.IsChecked)
            {
                App.Person = "Person";
            }
            else if (FamilyOption.IsChecked)
            {
                App.Person = "Family";
            }
            else if (CaregiverOption.IsChecked)
            {
                App.Person = "Caregiver";
            }
            else if (NurseOption.IsChecked)
            {
                App.Person = "Nurse";
            }
            else if (PhysicanOption.IsChecked)
            {
                App.Person = "Physican";
            }
        }

        public bool Completed()
        {
            return (PersonOption.IsChecked || FamilyOption.IsChecked || CaregiverOption.IsChecked || NurseOption.IsChecked || PhysicanOption.IsChecked) /*&& !BirthYear.Text.Equals("") && !EducationLevel.Text.Equals("")*/;
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
            //BirthYear.Focus();
        }
    }
}

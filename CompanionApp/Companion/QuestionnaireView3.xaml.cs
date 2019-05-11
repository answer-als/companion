using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class QuestionnaireView3 : ContentView
    {
        public QuestionnaireView3()
        {
            InitializeComponent();

            LoadPrevious();
        }

        void LoadPrevious()
        {
            if (!App.LangsSpoken.Equals("DidNotAnswer"))
            {
                SpokenLanguages.Text = App.LangsSpoken;
            }

            if (!App.LangsExposed.Equals("DidNotAnswer"))
            {
                ExposedLanguages.Text = App.LangsExposed;
            }
        }

        public void SaveResponses()
        {
            if (!SpokenLanguages.Text.Equals(""))
            {
                App.LangsSpoken = SpokenLanguages.Text;
            }

            if (!ExposedLanguages.Text.Equals(""))
            {
                App.LangsExposed = ExposedLanguages.Text;
            }
        }

        void RadioButton_Clicked(object sender, EventArgs e)
        {
            // Nothing
        }

        void FocusNext(object sender, EventArgs e)
        {
            ExposedLanguages.Focus();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Companion
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void OnEntryStart(object sender, EventArgs e)
        {
            incorrect.Text = "";
        }

        async void NavigateToID(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskPage());
        }

        void OnEnterClicked(object sender, EventArgs e)
        {

            // User ID check
            // TODO: Actually implement some checks for proper UserID (ie not empty)
            if (!string.IsNullOrEmpty(userIDEntry.Text) && userIDEntry.Text.Equals("corten"))
            {
                App.UserID = userIDEntry.Text;

                // Password Verification
                if (passwordEntry.Text.Equals(App.Password))
                {
                    NavigateToID(sender, e);
                }
                else
                {
                    incorrect.Text = "Password Incorrect! Please try again.";
                }
            }
            else
            {
                incorrect.Text = "User ID Incorrect! Please try again.";
            }
        }
    }
}
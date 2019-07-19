using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Companion
{
    public partial class PopUpMessage : ContentView
    {
        public PopUpMessage()
        {
            InitializeComponent();
        }

        public void LeaveMessage(object sender, EventArgs e)
        {
            TaskPage rent = (TaskPage)this.Parent;
            rent.LeavePopUp();
        }
    }
}

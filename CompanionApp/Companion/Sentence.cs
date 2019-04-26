using System;

using Xamarin.Forms;

namespace Companion
{
    public class Sentence : ContentView
    {
        public Label sent { get; set; }

        public Sentence()
        {

            sent = new Label
            {
                Text = App.CurrentSentence,
                TextColor = Color.FromHex("#006080"),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            Content = sent;
        }
    }
}

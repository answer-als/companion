using System;

using Xamarin.Forms;

namespace Companion
{
    public class Phrase2 : ContentView
    {
        public Label phrase { get; set; }
        string words = "NOPE";

        public Phrase2()
        {
            switch (App.SpeechPhrase2)
            {
                case 1:
                    words = SpeechPhrases.Phrase1;
                    break;
                case 2:
                    words = SpeechPhrases.Phrase2;
                    break;
                case 3:
                    words = SpeechPhrases.Phrase3;
                    break;
                case 4:
                    words = SpeechPhrases.Phrase4;
                    break;
                case 5:
                    words = SpeechPhrases.Phrase5;
                    break;
                case 6:
                    words = SpeechPhrases.Phrase6;
                    break;
                case 7:
                    words = SpeechPhrases.Phrase7;
                    break;
                case 8:
                    words = SpeechPhrases.Phrase8;
                    break;
                case 9:
                    words = SpeechPhrases.Phrase9;
                    break;
                case 10:
                    words = SpeechPhrases.Phrase10;
                    break;
                case 11:
                    words = SpeechPhrases.Phrase11;
                    break;
                case 12:
                    words = SpeechPhrases.Phrase12;
                    break;
                case 13:
                    words = SpeechPhrases.Phrase13;
                    break;
                case 14:
                    words = SpeechPhrases.Phrase14;
                    break;
                case 15:
                    words = SpeechPhrases.Phrase15;
                    break;
                case 16:
                    words = SpeechPhrases.Phrase16;
                    break;
                case 17:
                    words = SpeechPhrases.Phrase17;
                    break;
                case 18:
                    words = SpeechPhrases.Phrase18;
                    break;
                default:
                    words = "Didnt work";
                    break;
            }

            phrase = new Label
            {
                Text = words,
                TextColor = Color.FromHex("#006080"),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            Content = phrase;
        }
    }
}

using System;
using Companion.iOS;
using Xamarin.Forms;
using Foundation;
using AVFoundation;

[assembly: Dependency(typeof(NativeAudio_iOS))]
namespace Companion.iOS
{
    public class NativeAudio_iOS : IRecordingInterface
    {
        public NativeAudio_iOS()
        {
        }

        public void RouteAudioToSpeaker()
        {
            var audioSession = AVAudioSession.SharedInstance();
            var success = audioSession.SetCategory(AVAudioSession.CategoryPlayAndRecord,
                                                   out NSError error);
            if (success)
            {
                Console.WriteLine("Successfully set audio category to 'PlayAndRecord'");
                success = audioSession.OverrideOutputAudioPort(AVAudioSessionPortOverride.Speaker, out error);
                if (success)
                {
                    Console.WriteLine("Successfully overrode audio to speaker");
                    success = audioSession.SetActive(true, out error);

                    if (success)
                    {
                        Console.WriteLine("Successfully set new audio state to Active");
                    }
                }
            }

            Console.WriteLine("++++++++++++++++Setting up Record mode with Speaker++++++++++++++++");
        }

        public float VolumeLevel()
        {
            var val = Plugin.AudioRecorder.AudioRecorderService.volumeLevel;
            return val;
        }
    }
}

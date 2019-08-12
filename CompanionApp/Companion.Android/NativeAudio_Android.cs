using System;
using Companion.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(NativeAudio_Android))]
namespace Companion.Droid
{
    public class NativeAudio_Android : IRecordingInterface
    {
        public NativeAudio_Android()
        {
        }

        public void RouteAudioToSpeaker()
        {
            // Do nothing...Android audio routes to speaker by default.
        }

        public float VolumeLevel()
        {
            return Plugin.AudioRecorder.AudioRecorderService.volumeMeter;
        }
    }
}
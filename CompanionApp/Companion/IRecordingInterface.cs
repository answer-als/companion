using System;
namespace Companion
{
    public interface IRecordingInterface
    {
        void RouteAudioToSpeaker();
        float VolumeLevel();
    }
}

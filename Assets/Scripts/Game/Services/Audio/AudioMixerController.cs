using UnityEngine;
using UnityEngine.Audio;

namespace Game.Services.Audio
{
    public class AudioMixerController
    {
        private readonly AudioMixer _audioMixer;
        private const string MasterVolumeParameter = "MasterVolume";
        private const string MusicVolumeParameter = "MusicVolume";
        private const string SfxVolumeParameter = "SfxVolume";
        private const string UiVolumeParameter = "UiVolume";

        public AudioMixerController(AudioMixer audioMixer)
        {
            _audioMixer = audioMixer;
        }
        
        public void SetMusicVolume(float volume)
        {
            _audioMixer.SetFloat(MusicVolumeParameter, Mathf.Log10(volume) * 20);
        }

        public void SetSfxVolume(float volume)
        {
            _audioMixer.SetFloat(SfxVolumeParameter, Mathf.Log10(volume) * 20);
        }

        public void SetUiVolume(float volume)
        {
            _audioMixer.SetFloat(UiVolumeParameter, Mathf.Log10(volume) * 20);
        }

        public void SetMasterVolume(float volume)
        {
            _audioMixer.SetFloat(MasterVolumeParameter, Mathf.Log10(volume) * 20);
        }
    }
}
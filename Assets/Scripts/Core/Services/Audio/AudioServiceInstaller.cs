using Core.Audio.Data;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Core.Audio
{
    public class AudioServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var audioMixer = Resources.Load<AudioMixer>("AudioMixer");
            if (audioMixer == null)
            {
                Debug.LogError($"Audio Mixer not found in resources");
                return;
            }

            var audioLibrary = Resources.Load<AudioLibrary>("AudioLibrary");
            if (audioLibrary == null)
            {
                Debug.LogError($"Audio Library not found in resources");
                return;
            }

            Container.Bind<AudioService>().AsSingle().WithArguments(audioMixer, audioLibrary).NonLazy();
            Debug.Log($"Audio Service installed to Project Context");
        }
    }
}

using Zenject;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Services.Audio
{
    public class AudioServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var audioMixer = Resources.Load<AudioMixer>("AudioMixer");
            if (audioMixer == null)
            {
                Debug.LogError($"Audio Library not found: {Resources.Load<AudioLibrary>("AudioLibrary")}");
                return;
            }
            
            var audioLibrary = Resources.Load<AudioLibrary>("AudioLibrary");
            if (audioLibrary == null)
            {
                Debug.LogError($"Audio Library not found: {Resources.Load<AudioLibrary>("AudioLibrary")}");
                return;
            }

            Container.Bind<AudioService>().AsSingle().WithArguments(audioMixer, audioLibrary).NonLazy();
        }
    }
}
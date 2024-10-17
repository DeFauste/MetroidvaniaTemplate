using UnityEngine;

namespace Game.Services.Audio
{
    public class AudioPlayer
    {
        private readonly AudioSource _musicSource;
        private readonly AudioSource _effectsSource;

        public AudioPlayer()
        {
            var audioPlayerGameObject = new GameObject("AudioPlayer");
            Object.DontDestroyOnLoad(audioPlayerGameObject);

            _musicSource = audioPlayerGameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;

            _effectsSource = audioPlayerGameObject.AddComponent<AudioSource>();
        }

        public void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void StopMusic()
        {
            _musicSource.Stop();
        }

        public void PlaySfx(AudioClip clip, float volume = 1f, AudioSource audioSource = null, bool oneShot = true)
        {
            var source = audioSource ?? _effectsSource;
            if (oneShot)
            {
                source.PlayOneShot(clip, volume);
            }
            else
            {
                source.clip = clip;
                source.Play();
            }
        }
    }
}

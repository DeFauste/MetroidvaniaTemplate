using System.Collections.Generic;
using UnityEngine;

namespace Core.Audio.Data
{
    [CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio/Audio Library")]
    public class AudioLibrary : ScriptableObject
    {
        public List<AudioClipData> audioClips;

        public AudioClip GetClipById(string id)
        {
            var clipData = audioClips.Find(audioClip => audioClip.id == id);
            return clipData?.clip;
        }
    }
}

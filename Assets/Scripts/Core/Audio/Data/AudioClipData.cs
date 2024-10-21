using System;
using UnityEngine;

namespace Core.Audio.Data
{
    [Serializable]
    public class AudioClipData
    {
        public string id;
        public AudioClip clip;
        public AudioType type;
    }

    public enum AudioType
    {
        Music,
        SFX,
        UI
    }
}

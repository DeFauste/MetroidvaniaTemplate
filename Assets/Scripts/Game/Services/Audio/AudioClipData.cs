using System;
using UnityEngine;

namespace Game.Services.Audio
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
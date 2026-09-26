using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/AudioDatabase")]
public class AudioDatabaseScriptable : ScriptableObject
{
    public List<AudioClipData> audioClips;

    public AudioClipData GetClip(string name)
    {
        return audioClips.FirstOrDefault(c => c.clipName == name);
    }
}

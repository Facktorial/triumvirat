using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioDatabaseScriptable sfxDatabase;
    [SerializeField] AudioDatabaseScriptable uiDatabase;

    [Space]
    [SerializeField] AudioSource uiAudio;
    [SerializeField] List<AudioSource> sfxSourceList;
    [SerializeField] int sfxSourcesCount;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(string clipName, Vector3 position)
    {
        foreach (var source in sfxSourceList)
        {
            print("Looking for source");
            if (!source.isPlaying)
            {
                print("Playing clip: " + clipName);
                SetSource(source, sfxDatabase.GetClip(clipName), position);
                break;
            }
        }
    }

    public void PlayUI(string clipName)
    {
        SetSource(uiAudio, uiDatabase.GetClip(clipName), Vector3.zero);
    }

    void SetSource(AudioSource source, AudioClipData clipData, Vector3 position)
    {
        print("Playing SFX");

        if (clipData == null)
        {
            print("No clip data");
            return;
        }

        source.pitch = 1;
        source.clip = clipData.clip;
        source.playOnAwake = false;

        if (clipData.randomPitch)
        {
            source.pitch = 1 + Random.Range(0f, 1f);
        }

        source.Play();
    }
}

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

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(string clipName, Vector3 position)
    {
        foreach (var item in sfxSourceList)
        {

        }
    }

    public void PlayUI(string clipName)
    {
        SetSource(uiAudio, uiDatabase.GetClip(clipName), Vector3.zero);
    }

    void SetSource(AudioSource source, AudioClipData clipData, Vector3 position)
    {
        source.clip = clipData.clip;

        if (clipData.randomPitch)
        {
            source.pitch += Random.Range(0f, 1f);
        }
    }
}

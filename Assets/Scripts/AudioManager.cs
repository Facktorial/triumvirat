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

    private void Start()
    {
        SpawnSources();
    }

    public void PlaySFX(string clipName, Vector3 position)
    {
        foreach (var source in sfxSourceList)
        {
            if (!source.isPlaying)
            {
                SetSource(source, sfxDatabase.GetClip(name), position);
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
        source.clip = clipData.clip;

        if (clipData.randomPitch)
        {
            source.pitch += Random.Range(0f, 1f);
        }
    }

    void SpawnSources()
    {
        for (int i = 0; i < sfxSourcesCount; i++)
        {
            GameObject audioSource = new GameObject("sfxSource");
            audioSource.transform.parent = transform;
            audioSource.AddComponent<AudioSource>();
        }
    }
}

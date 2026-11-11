using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public enum SoundType
{
    Ambience,
    Player,
    Monster
}

[System.Serializable]
public class SoundInformation
{
    public string audioName;
    public AudioClip soundClip;
    public float volume = 1f;
    public Vector2 pitchRange = new Vector2(0.9f, 1.1f);
    public bool randomizePitch = true;

    public float hearingRange = 1f;

    [HideInInspector] public Vector3 origin;
}

[System.Serializable]
public class SoundGroups
{
    [HideInInspector] public string groupName;
    public List<SoundInformation> soundInfo = new List<SoundInformation>();
}


public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private SoundGroups[] soundGroups;
    private List<GameObject> audioPrefabs = new List<GameObject>();
    public float masterVolumeMultiplier;
    public GameObject audioObjectPrefab;

    private void Reset()
    {
        string[] names = Enum.GetNames(typeof(SoundType));

        soundGroups = new SoundGroups[names.Length];

        for (int i = 0; i < soundGroups.Length; i++)
        {
            soundGroups[i] = new SoundGroups();
            soundGroups[i].groupName = names[i];
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        
    }

    public static void PlaySound(SoundType sound, int audioIndex, Vector3 spawnPosition)
    {
        GameObject soundPrefab = Instantiate(instance.audioObjectPrefab, spawnPosition, Quaternion.identity);

        //instance.audioPrefabs.Add(soundPrefab);

        //AudioClip clip = instance.soundGroups[(int)sound].soundInfo[audioIndex].soundClip;
        //float volume = instance.soundGroups[(int)sound].soundInfo[audioIndex].volume * instance.masterVolumeMultiplier;

        soundPrefab.GetComponent<AudioObject>().Play(instance.soundGroups[(int)sound].soundInfo[audioIndex]);
    }

    public void Ambience()
    {
        GameObject soundPrefab = Instantiate(audioObjectPrefab, transform.position, Quaternion.identity, transform);

        AudioSource audioSource = soundPrefab.GetComponent<AudioSource>();
        audioPrefabs.Add(soundPrefab);

        // Make it reference the ambience sound group then make it have a chance to play random ambience sounds, randomly changing the pitch each time

        //audioSource.clip = soundGroups[audioGroup].soundInfo[audioIndex].soundClip;
        //audioSource.volume = soundGroups[audioIndex].soundInfo[audioIndex].volume * masterVolumeMultiplier;
        audioSource.Play();

        if (!audioSource.isPlaying)
        {
            // Wait for a moment or something
        }
    }

    Vector3 AmbientAreaPosition()
    {
        // Generate a random horizontal offset
        Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * 8;

        Vector3 randomPoint = transform.position + new Vector3(randomOffset.x, 0f, randomOffset.y);
        return randomPoint;
    }
}

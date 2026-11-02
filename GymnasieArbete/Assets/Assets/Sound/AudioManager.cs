using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class SoundInformation
{
    public string audioName;
    public AudioClip soundClip;
    public float volume = 1f;
}

[System.Serializable]
public class SoundGroups
{
    public string groupName;
    public List<SoundInformation> soundInfo = new List<SoundInformation>();
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    public List<SoundGroups> soundGroups = new List<SoundGroups>();
    private List<GameObject> audioPrefabs = new List<GameObject>();
    public float masterVolumeMultiplier;
    public GameObject audioObjectPrefab;

    public Transform playerTransform;

    private void Awake()
    {
        audioManager = this;
    }

    private void Update()
    {
        
    }

    public void PlayOneShot(int audioGroup, int audioIndex, Transform spawnTransform)
    {
        GameObject soundPrefab = Instantiate(audioObjectPrefab, spawnTransform.position, Quaternion.identity, spawnTransform);

        AudioSource audioSource = soundPrefab.GetComponent<AudioSource>();
        audioPrefabs.Add(soundPrefab);

        audioSource.clip = soundGroups[audioGroup].soundInfo[audioIndex].soundClip;
        audioSource.volume = soundGroups[audioIndex].soundInfo[audioIndex].volume * masterVolumeMultiplier;
        audioSource.Play();

        if (!audioSource.isPlaying)
        {
            audioPrefabs.Remove(soundPrefab);
            Destroy(soundPrefab);
        }
    }

    public void Ambience()
    {
        GameObject soundPrefab = Instantiate(audioObjectPrefab, playerTransform.position, Quaternion.identity, transform);

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
        Vector2 randomOffset = Random.insideUnitCircle * 8;

        Vector3 randomPoint = playerTransform.position + new Vector3(randomOffset.x, 0f, randomOffset.y);
        return randomPoint;
    }
}

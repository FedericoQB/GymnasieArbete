using UnityEngine;

public class AudioObject : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(SoundInformation sound)
    {
        if (sound.randomizePitch)
        {
            source.pitch = Random.Range(sound.pitchRange.x, sound.pitchRange.y);
        }

        source.PlayOneShot(sound.soundClip, sound.volume);

        float duration = sound.soundClip.length / Mathf.Abs(source.pitch);
        Destroy(gameObject, duration);
    }
}

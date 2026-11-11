using UnityEngine;
using UnityEngine.Rendering;

public class AudioObject : MonoBehaviour
{
    private AudioSource source;

    public SoundInformation soundInfo;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        GetComponent<SphereCollider>().isTrigger = true;
    }

    public void Play(SoundInformation sound)
    {
        soundInfo = sound;

        if (sound.randomizePitch)
        {
            source.pitch = Random.Range(sound.pitchRange.x, sound.pitchRange.y);
        }

        source.PlayOneShot(sound.soundClip, sound.volume);
        GetComponent<SphereCollider>().radius = sound.hearingRange;
        sound.origin = transform.position;

        float duration = sound.soundClip.length / Mathf.Abs(source.pitch);
        Destroy(gameObject, duration);
    }
}

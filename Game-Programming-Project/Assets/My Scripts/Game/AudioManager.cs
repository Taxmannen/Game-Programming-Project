using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake()
    {
        if (INSTANCE != null) return;
        else INSTANCE = this;
    }

    public void Play(string name, float volume = 1, float pitch = 1)
    {
        AudioClip audioClip = Resources.Load<AudioClip>("Sounds/" + name);
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;

        audioSource.Play();

        Destroy(audioSource, audioClip.length * 2); 
    }

    public static AudioManager INSTANCE { get; private set; }
}
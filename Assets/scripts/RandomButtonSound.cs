using UnityEngine;

public class RandomButtonSound : MonoBehaviour
{
    public AudioSource audioSource;   // Assign the AudioSource in Inspector
    public AudioClip[] soundClips;    // Drag all your sounds into this array

    // Call this from the button OnClick()
    public void PlayRandomSound()
    {
        if (soundClips.Length == 0) return;

        int randomIndex = Random.Range(0, soundClips.Length);
        audioSource.clip = soundClips[randomIndex];
        audioSource.Play();
    }
}

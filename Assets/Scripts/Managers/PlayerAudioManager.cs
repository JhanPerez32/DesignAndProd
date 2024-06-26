using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Audio
    {
        public string name;
        public AudioClip clip;
        public float volume = 1.0f; // Default volume to 1.0
        [HideInInspector] public AudioSource source; // Hide in Inspector to avoid manual assignment

        public bool randomizePitch = false; // Checkbox to randomize pitch
        [Range(0.1f, 3.0f)] public float minPitch = 0.9f; // Minimum pitch range
        [Range(0.1f, 3.0f)] public float maxPitch = 1.1f; // Maximum pitch range
    }

    [SerializeField] private List<Audio> audioClips;

    void Start()
    {
        // Initialize each AudioSource if not already assigned
        foreach (var audio in audioClips)
        {
            if (audio.source == null)
            {
                audio.source = gameObject.AddComponent<AudioSource>();
                audio.source.clip = audio.clip;
                audio.source.volume = audio.volume;
            }
        }
    }

    // Play an audio clip by name
    public void PlayAudio(string clipName)
    {
        // Find the audio clip with the specified name
        Audio clipToPlay = audioClips.Find(audio => audio.name == clipName);
        if (clipToPlay != null)
        {
            if (clipToPlay.source.clip != clipToPlay.clip)
            {
                clipToPlay.source.clip = clipToPlay.clip;
            }
            clipToPlay.source.volume = clipToPlay.volume; // Set the volume

            // Randomize pitch if the checkbox is checked
            if (clipToPlay.randomizePitch)
            {
                clipToPlay.source.pitch = Random.Range(clipToPlay.minPitch, clipToPlay.maxPitch);
            }
            else
            {
                clipToPlay.source.pitch = 1.0f; // Default pitch
            }

            clipToPlay.source.Play();
        }
        else
        {
            Debug.LogError($"Audio clip with name '{clipName}' not found.");
        }
    }

    // Stop an audio clip by name
    public void StopAudio(string clipName)
    {
        // Find the audio clip with the specified name
        Audio clipToStop = audioClips.Find(audio => audio.name == clipName);

        if (clipToStop != null)
        {
            clipToStop.source.Stop();
        }
        else
        {
            Debug.LogError($"Audio clip with name '{clipName}' not found.");
        }
    }

    // Adjust the volume of an audio clip by name
    public void SetVolume(string clipName, float volume)
    {
        // Find the audio clip with the specified name
        Audio clipToAdjust = audioClips.Find(audio => audio.name == clipName);
        if (clipToAdjust != null)
        {
            clipToAdjust.volume = volume;
            if (clipToAdjust.source.isPlaying)
            {
                clipToAdjust.source.volume = volume;
            }
        }
        else
        {
            Debug.LogError($"Audio clip with name '{clipName}' not found.");
        }
    }
}

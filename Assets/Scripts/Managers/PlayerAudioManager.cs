using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Audio
    {
        public string name;
        public AudioClip clip;
        public AudioSource source; // Add AudioSource here
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
            clipToPlay.source.Play();
        }
        else
        {
            Debug.LogError($"Audio clip with name '{clipName}' not found.");
        }
    }
    public void Play()
    {
        // Find the audio clip with the specified name
  
            Debug.Log("Test");
        
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
}

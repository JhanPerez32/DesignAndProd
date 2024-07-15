using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.DigitalGlitch;

public class GlitchController : MonoBehaviour
{
    public Volume volume;

    [Range(0f, 1f)]
    public float glitchIntensity;

    private DigitalGlitchVolume digitalGlitchVolume;

    private void Start()
    {
        if (volume != null)
        {
            VolumeProfile profile = volume.profile;

            if (profile != null && profile.TryGet(out digitalGlitchVolume))
            {
                // Initially set the intensity
                digitalGlitchVolume.intensity.value = glitchIntensity;
            }
        }
    }

    private void Update()
    {
        if (digitalGlitchVolume != null)
        {
            digitalGlitchVolume.intensity.value = glitchIntensity;
        }
    }
}

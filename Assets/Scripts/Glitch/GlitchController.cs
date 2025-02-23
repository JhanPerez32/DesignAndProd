using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;

public class GlitchController : MonoBehaviour
{
    public Volume volume;

    [Header ("Digital Glitch")]
    [Range (0f, 1f)]
    public float glitchIntensity;

    [Header("Analog Glicth")]
    [Range(0f, 1f)]
    public float scanLineJitterIntensity;
    [Range(0f, 1f)]
    public float verticalJumpIntensity;
    [Range(0f, 1f)]
    public float horizontalShakeIntensity;
    [Range(0f, 1f)]
    public float colorDriftIntensity;

    private DigitalGlitchVolume digitalGlitchVolume;
    private AnalogGlitchVolume analogGlitchVolume;

    private void Start()
    {
        if (volume != null)
        {
            VolumeProfile profile = volume.profile;

            //Setting the Intensities

            //Digital Glitch
            if (profile != null && profile.TryGet(out digitalGlitchVolume))
            {
                digitalGlitchVolume.intensity.value = glitchIntensity;
            }

            //Analog Glitch
            if(profile != null && profile.TryGet(out analogGlitchVolume))
            {
                analogGlitchVolume.scanLineJitter.value = scanLineJitterIntensity;
            }
            if (profile != null && profile.TryGet(out analogGlitchVolume))
            {
                analogGlitchVolume.verticalJump.value = verticalJumpIntensity;
            }
            if (profile != null && profile.TryGet(out analogGlitchVolume))
            {
                analogGlitchVolume.horizontalShake.value = horizontalShakeIntensity;
            }
            if (profile != null && profile.TryGet(out analogGlitchVolume))
            {
                analogGlitchVolume.colorDrift.value = colorDriftIntensity;
            }
        }
    }

    private void Update()
    {
        if (digitalGlitchVolume != null)
        {
            IncreaseDigitalIntensity();
        }

        if(analogGlitchVolume != null)
        {
            IncreaseAnalogIntensity();
        }
    }

    void IncreaseDigitalIntensity()
    {
        digitalGlitchVolume.intensity.value = glitchIntensity;
    }

    void IncreaseAnalogIntensity()
    {
        analogGlitchVolume.scanLineJitter.value = scanLineJitterIntensity;
        analogGlitchVolume.verticalJump.value = verticalJumpIntensity;
        analogGlitchVolume.horizontalShake.value = horizontalShakeIntensity;
        analogGlitchVolume.colorDrift.value = colorDriftIntensity;
    }
}

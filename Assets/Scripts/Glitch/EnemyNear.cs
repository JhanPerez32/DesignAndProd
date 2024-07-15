using Pathways;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNear : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    [SerializeField] GlitchController glitchController;

    [Range (5f, 25f)]
    [SerializeField] float distanceToStartGlitch;

    [Range (0f, 1f)]
    [SerializeField] float maxScanLineIntensity;
    [Range(0f, 1f)]
    [SerializeField] float maxColorDriftIntensity;


    private void Start()
    {
        if (!enemyController)
        {
            enemyController = GetComponent<EnemyController>();
        }

        if (!glitchController)
        {
            glitchController = GetComponent<GlitchController>();
        }
    }

    private void Update()
    {
        float distanceToPlayer = enemyController.distanceToPlayer;

        if (distanceToPlayer < distanceToStartGlitch)
        {
            float normalizedDistance = Mathf.Clamp01(distanceToPlayer / distanceToStartGlitch);

            float scanLineIntensity = maxScanLineIntensity - normalizedDistance;
            float colorDriftIntensity = maxColorDriftIntensity - normalizedDistance * 0.5f; // Adjust this factor as needed

            glitchController.scanLineJitterIntensity = scanLineIntensity;
            glitchController.colorDriftIntensity = colorDriftIntensity;

        }
        else
        {
            glitchController.scanLineJitterIntensity = 0f;
            glitchController.colorDriftIntensity = 0f;
        }
    }
}

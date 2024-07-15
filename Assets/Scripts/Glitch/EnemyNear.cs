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

            float intensity = 1f - normalizedDistance;

            glitchController.glitchIntensity = intensity;

            Debug.Log("Increase Glitch: " + intensity);
        }
        else
        {
            glitchController.glitchIntensity = 0f;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    [SerializeField] private Material sphere;
    private Material _material;

    // Change the skybox material when the player enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider is tagged as 'Player'
        if (other.CompareTag("Player"))
        {
             ChangeMaterial(sphere.name);         
        }
    }

    public void ChangeMaterial(string sphereName)
    {
        string path = "Skybox/" + sphereName;
        Texture t = Resources.Load<Texture>(path);
        if (t != null)
        {
            if (_material == null)
            {
                _material = new Material(Shader.Find("Skybox/Procedural")); // Initialize the material if it's null
            }
            _material.SetTexture("_Tex", t);
            RenderSettings.skybox = _material;
        }
    }
}

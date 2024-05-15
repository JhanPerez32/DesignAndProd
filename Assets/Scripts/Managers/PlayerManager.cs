using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Most of the General Setting for the Player can be place here
    public static PlayerManager Instance { get; private set; }

    [Header("Movement Speed")]
    public float moveSpeed;
    public float jumpHeight;
    public float groundDist;
    public float mouseLookSensitivity;

    private void Awake()
    {
        if(Instance != null & Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}

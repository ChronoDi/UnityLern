using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeadPlayer : MonoBehaviour
{
    private AudioSource _dieSound;

    private void OnEnable()
    {
        _dieSound = GetComponent<AudioSource>();
        _dieSound.Play();
    }
}

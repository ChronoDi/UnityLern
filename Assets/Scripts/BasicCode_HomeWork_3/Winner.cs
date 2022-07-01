using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Winner : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Win()
    {
        gameObject.SetActive(true);
        _audioSource.Play();

    }

    private void Start()
    {
        gameObject.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
    }

}

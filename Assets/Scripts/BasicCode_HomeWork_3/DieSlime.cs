using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DieSlime : MonoBehaviour
{
    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}

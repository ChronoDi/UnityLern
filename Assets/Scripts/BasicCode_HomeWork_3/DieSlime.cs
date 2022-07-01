using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DieSlime : MonoBehaviour
{
    [SerializeField] private float _timeDespawn = 3f;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(_timeDespawn);
        Destroy(gameObject);
    }
}

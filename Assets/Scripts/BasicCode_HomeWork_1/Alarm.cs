using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _deltaVolume;

    private AudioSource _source;
    private float _targetVolume;

    public void StartAlarm()
    {
        _source.Play();
        StartCoroutine(ChangeVolume());
    }

    public void StopAlarm()
    {
        StopCoroutine(ChangeVolume());
        _source.Stop();
    }

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.volume = _maxVolume;
        _targetVolume = _minVolume;
    }

    private IEnumerator ChangeVolume()
    {
        var WaitForFixedUpdate = new WaitForFixedUpdate();

        while (_source.isPlaying)
        {
            _source.volume = Mathf.MoveTowards(_source.volume, _targetVolume, _deltaVolume);

            if (_source.volume == _minVolume)
            {
                _targetVolume = _maxVolume;
            }
            else if (_source.volume == _maxVolume)
            {
                _targetVolume = _minVolume;
            }

            yield return WaitForFixedUpdate;
        }
    }
}

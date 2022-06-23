using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] float _maxVolume;
    [SerializeField] float _minVolume;
    [SerializeField] float _deltaVolume;

    private AudioSource _source;
    private float _targetVolume;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.volume = _maxVolume;
        _targetVolume = _minVolume;
    }

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

    private IEnumerator ChangeVolume()
    {
        int needFps = 30;
        var waitForFewSecond = new WaitForSeconds(1f / needFps);

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

            yield return waitForFewSecond;
        }
    }
}

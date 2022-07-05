using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    private bool _isPlaing = true;

    public const string MasterVolume = "MasterVolume";
    public const string SoundVolume = "SoundVolume";
    public const string MusicVolume = "MusicVolume";

    public void SetMasterVolume(float volume) 
    {
        if (_isPlaing)
            _audioMixer.SetFloat(MasterVolume, volume);
    }

    public void SetMusicVolume(float volume)
    {
        if (_isPlaing)
            _audioMixer.SetFloat(MusicVolume, volume);
        
    }

    public void SetSoundVolume(float volume) 
    {
        if (_isPlaing)
            _audioMixer.SetFloat(SoundVolume, volume);
    }

    public void MuteMusic(bool isSet)
    {
        float volume = isSet ? 0f : -80.0f;
        _isPlaing = isSet;
        _audioMixer.SetFloat(MusicVolume, volume);
    }

    public void MuteSound(bool isSet)
    {
        float volume = isSet ? 0f : -80.0f;
        _isPlaing = isSet;
        _audioMixer.SetFloat(SoundVolume, volume);
    }

}

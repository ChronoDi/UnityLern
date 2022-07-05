using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowSliderValue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _volumeValue;

    public void SetValue(float volume) 
    {
        _volumeValue.text = $"{volume:N0} Db";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCanvas : Button
{
    [SerializeField] private Canvas _currentCanvas;
    [SerializeField] private Canvas _needCanvas;
    [SerializeField] private bool _isNeedHideCurrentCanvas = true;

    public void Change()
    {
        if (_isNeedHideCurrentCanvas)
            _currentCanvas.gameObject.SetActive(false);

        _needCanvas.gameObject.SetActive(true);
    }
}

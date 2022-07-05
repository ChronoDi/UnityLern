using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] Color _upColor = Color.red;
    [SerializeField] Color _downColor = Color.black;
    [SerializeField] float _textSize = 36;

    private Color _currentColor;
    private float _currentTextSize;

    private void Awake()
    {
        _currentColor = _text.color;
        _currentTextSize = _text.fontSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.enableVertexGradient = true;
        _text.colorGradient = new VertexGradient(_upColor, _upColor, _downColor, _downColor);
        _text.fontSize = _textSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.enableVertexGradient = false;
        _text.color = _currentColor;
        _text.fontSize = _currentTextSize;
    }
}

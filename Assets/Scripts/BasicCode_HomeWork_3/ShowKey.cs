using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShowKey : MonoBehaviour
{
    [SerializeField] private Sprite _haveKey;

    private Image _image;

    public bool IsTaken { get; private set; }

    public void ChangeImage()
    {
        _image.sprite = _haveKey;
        IsTaken = true;
    }

    private void Start()
    {
        _image = GetComponent<Image>();
        IsTaken = false;
    }
}

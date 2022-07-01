using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Heart : MonoBehaviour
{
    private Image _image;

    public void ChangeSprite(Sprite sprite) => _image.sprite = sprite;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

}

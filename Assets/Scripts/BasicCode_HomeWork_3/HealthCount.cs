using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCount : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;

    private Heart[] _hearts;

    private void Start()
    {
        _hearts = new Heart[transform.childCount];

        for (int i = 0; i < _hearts.Length; i++)
        {
            _hearts[i] = transform.GetChild(i).GetComponent<Heart>();
        }
    }

    private void Update()
    {
        for (int i = 0; i < _player.CurrentHealth; i++)
        {
            _hearts[i].ChangeSprite(_fullHeart);
        }

        for (int i = _player.CurrentHealth; i < _player.MaxHealth; i++)
        {
            _hearts[i].ChangeSprite(_emptyHeart);
        }
    }
}

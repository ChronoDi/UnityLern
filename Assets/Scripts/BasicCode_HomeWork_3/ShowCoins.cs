using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShowCoins : MonoBehaviour
{
    [SerializeField] private CountCoin _countCoin;

    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = $"{_countCoin.TakenCoins} / {_countCoin.AllCoins}";
    }
}

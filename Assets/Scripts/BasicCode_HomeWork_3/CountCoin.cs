using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountCoin : MonoBehaviour
{
    [SerializeField] private ShowCoins _showCoins;
    [SerializeField] private UnityEvent _allCoinTaken;

    private Item[] _coins;
    private int _takenCoins;
    private int _allCoins;

    private void Start()
    {
        _allCoins = transform.childCount;
        _takenCoins = 0;

        _showCoins.EditText(_takenCoins, _allCoins);

        _coins = gameObject.GetComponentsInChildren<Item>();

        foreach (var coin in _coins)
            coin.Taken += TakeCoin;
    }

    private void OnDisable()
    {
        foreach (var coin in _coins)
            coin.Taken -= TakeCoin;
    }

    public void TakeCoin()
    {
        _takenCoins++;

        _showCoins.EditText(_takenCoins, _allCoins);

        if (_takenCoins == _allCoins)
        {
            _allCoinTaken.Invoke();
        }
    }
}

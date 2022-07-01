using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountCoin : MonoBehaviour
{
    [SerializeField] private UnityEvent _allCoinTaken;

    private bool _isNotified;  

    public int TakenCoins { get; private set; } 
    public int AllCoins { get; private set; } 


    private void Start()
    {
        AllCoins = transform.childCount;
        _isNotified = false;
    }

    private void Update()
    {
        TakenCoins = AllCoins - transform.childCount;

        if (transform.childCount == 0 && _isNotified == false)
        {
            _allCoinTaken.Invoke();
            _isNotified = true;
        }
        
    }
}

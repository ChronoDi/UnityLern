using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealth : MonoBehaviour
{
    [SerializeField] private Target _target;
    [SerializeField] private float _damage;
    [SerializeField] private float _health;

    public void DealDamage()
    {
        _target.TakeHit(_damage);
    }

    public void Heal()
    {
        _target.Heal(_health);
    }
}

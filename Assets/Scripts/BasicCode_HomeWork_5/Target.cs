using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Target : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private DeadPlayer _deadPlayer;
    [SerializeField] private AudioSource _damageSound;
    [SerializeField] private AudioSource _healSound;
    [SerializeField] private UnityEvent<float> _changed;

    private Animator _animator;
    private float _currentHealth;
    private float _currentHealthPercent;

    private const string FromAnimatorTakeHit = "takeHit";

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = _maxHealth;
        _currentHealthPercent = _maxHealth / _currentHealth;
        _changed.Invoke(_currentHealthPercent);
    }


    public void TakeHit(float damage)
    {
        _animator.SetTrigger(FromAnimatorTakeHit);
        _damageSound.Play();

        _currentHealth -= damage;
        _currentHealthPercent = _currentHealth / _maxHealth;

        _changed.Invoke(_currentHealthPercent);

        if (_currentHealth <= 0)
            Die();
    }

    public void Heal(float health)
    {
        if (_currentHealth != _maxHealth)
        {
            _healSound.Play();

            _currentHealth = _currentHealth + health > _maxHealth ? _maxHealth : _currentHealth + health;
            _currentHealthPercent = _currentHealth / _maxHealth;

            _changed.Invoke(_currentHealthPercent);
        }
    }

    private void Die()
    {
        Instantiate(_deadPlayer, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

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
    [SerializeField] private UnityEvent<float, float> _changed;

    private Animator _animator;
    private float _currentHealth;

    private const string FromAnimatorTakeHit = "takeHit";

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = _maxHealth;
        _changed.Invoke(_currentHealth, _maxHealth);
    }


    public void TakeHit(float damage)
    {
        _animator.SetTrigger(FromAnimatorTakeHit);
        _damageSound.Play();
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        _changed.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth == 0)
            Die();
    }

    public void Heal(float health)
    {
        if (_currentHealth != _maxHealth)
        {
            _healSound.Play();
            _currentHealth = Mathf.Clamp(_currentHealth + health, 0, _maxHealth);
            _changed.Invoke(_currentHealth, _maxHealth);
        }
    }

    private void Die()
    {
        Instantiate(_deadPlayer, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

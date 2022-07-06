using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Target : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private DeadPlayer _deadPlayer;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private AudioSource _damageSound;
    [SerializeField] private AudioSource _healSound;
    [SerializeField] private float _deltaHealthBar;

    private Animator _animator;
    private float _currentHealth;
    private float _currentHealthPercent;
    private Coroutine _changeHealthBar;

    private const string FromAnimatorTakeHit = "takeHit";

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = _maxHealth;
        _healthBar.value = _maxHealth / _currentHealth;
    }


    public void TakeHit(float damage)
    {
        _animator.SetTrigger(FromAnimatorTakeHit);
        _damageSound.Play();

        _currentHealth -= damage;
        _currentHealthPercent = _currentHealth / _maxHealth;

        RunCoroutines();

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

            RunCoroutines();
        }
    }

    private void Die()
    {
        Instantiate(_deadPlayer, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator ChangeHealthBar(float needValue)
    {
        while (_healthBar.value != needValue)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, needValue, _deltaHealthBar);
            yield return null;
        }
    }

    private void RunCoroutines()
    {
        if (_changeHealthBar != null)
            StopCoroutine(_changeHealthBar);

        _changeHealthBar = StartCoroutine(ChangeHealthBar(_currentHealthPercent));
    }

}

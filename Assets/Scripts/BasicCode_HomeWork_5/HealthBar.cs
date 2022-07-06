using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _deltaHealthBar;

    private Slider _healthBar;
    private Coroutine _changeHealthBar;

    private void OnEnable()
    {
        _healthBar = GetComponent<Slider>();
    }

    public void RunCoroutines(float currentHealthPercent)
    {
        if (_changeHealthBar != null)
            StopCoroutine(_changeHealthBar);

        _changeHealthBar = StartCoroutine(ChangeHealthBar(currentHealthPercent));
    }

    private IEnumerator ChangeHealthBar(float needValue)
    {
        while (_healthBar.value != needValue)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, needValue, _deltaHealthBar);
            yield return null;
        }
    }
}

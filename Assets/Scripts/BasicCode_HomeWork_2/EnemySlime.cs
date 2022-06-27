using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent (typeof(Animator))]
public class EnemySlime : MonoBehaviour
{
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _pathLength = 2f;
    [SerializeField] private GameObject _tamplate;

    private SpriteRenderer _sprite;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private GameObject _firstPoint;
    private GameObject _secondPoint;
    private Transform _targetPoint;

    private const string FromAnimatorSpeed = "Speed";

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _firstPoint = Instantiate(_tamplate, new Vector2(0, 0), Quaternion.identity);
        _secondPoint = Instantiate(_tamplate, new Vector2(0, 0), Quaternion.identity);

        _targetPoint = _firstPoint.transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.TryGetComponent<Ground> (out Ground ground))
       {
            _firstPoint.transform.position = new Vector2(transform.position.x - _pathLength, transform.position.y);
            _secondPoint.transform.position = new Vector2(transform.position.x + _pathLength, transform.position.y);
        }
    }

    private void Update()
    {
        if (Mathf.Abs(_rigidbody2D.velocity.y) <= _minGroundNormalY)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPoint.position, _speed * Time.deltaTime);

            _animator.SetFloat(FromAnimatorSpeed, _speed);

            if (transform.position == _firstPoint.transform.position)
            {
                _targetPoint = _secondPoint.transform;
                _sprite.flipX = true;
            }
            else if (transform.position == _secondPoint.transform.position)
            {
                _targetPoint = _firstPoint.transform;
                _sprite.flipX = false;
            }
        }
    }
}

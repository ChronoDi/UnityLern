using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private bool _isReversMovement;
    [SerializeField] private float _movementDistance;
    [SerializeField] private float _speed;
    [SerializeField] private Point _tamplate;

    private GameObject _firsPoint;
    private GameObject _secondPoint;
    private Transform _targetPoint;

    private void Start()
    {
        Vector2 firstPointVector = new Vector2(transform.position.x + _movementDistance, transform.position.y);
        Vector2 secondPointVector = new Vector2(transform.position.x - _movementDistance, transform.position.y);

        _firsPoint = Instantiate(_tamplate.gameObject, firstPointVector, Quaternion.identity);
        _secondPoint = Instantiate(_tamplate.gameObject, secondPointVector, Quaternion.identity);

        _targetPoint = _isReversMovement ? _secondPoint.transform : _firsPoint.transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.transform.parent = this.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.transform.parent = null;
        }
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPoint.position, _speed * Time.deltaTime);

        if (transform.position == _firsPoint.transform.position)
            _targetPoint = _secondPoint.transform;
        else if (transform.position == _secondPoint.transform.position)
            _targetPoint = _firsPoint.transform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorOpenner : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Door _door;

    private bool _isOpen;

    public bool IsOpen => _isOpen;

    private void Start()
    {
        _isOpen = _animator.GetBool("isOpen");
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && _door.IsReached)
        {
            ChangeDoorStatus();
        }
    }
    private void ChangeDoorStatus()
    {
        _isOpen = !_isOpen;
        _animator.SetBool("isOpen", _isOpen);
    }
}

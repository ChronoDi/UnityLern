using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorOpenner : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Door _door;

    private const string _fromAnimatorIsOpen = "isOpen";
    public bool IsOpen { get; private set; }

    private void Start()
    {
        IsOpen = _animator.GetBool(_fromAnimatorIsOpen);
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
        IsOpen = !IsOpen;
        _animator.SetBool(_fromAnimatorIsOpen, IsOpen);
    }
}

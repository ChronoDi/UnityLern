using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorOpenner : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Door _door;
    [SerializeField] private ShowKey _key;
    [SerializeField] private DoorText _doorText;
    [SerializeField] private float _showTimeText = 3f;
    [SerializeField] private UnityEvent _openned;

    private const string FromAnimatorIsOpen = "isOpen";

    public bool IsOpen { get; private set; }

    private void Start()
    {
        IsOpen = _animator.GetBool(FromAnimatorIsOpen);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) && _door.IsReached)
        {
            if (_key.IsTaken)
                OpenDoor();
            else
                StartCoroutine(ShowText());
        }
    }

    private IEnumerator ShowText()
    {
        _doorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(_showTimeText);
        _doorText.gameObject.SetActive(false);
    }

    private void OpenDoor()
    {
        IsOpen = true;
        _animator.SetBool(FromAnimatorIsOpen, IsOpen);
        _openned.Invoke();
    }
}

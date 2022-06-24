using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InvisibleWall : MonoBehaviour
{
    [SerializeField] private DoorOpenner _doorStatus;

    private BoxCollider2D _wallCollider;

    private void Start()
    {
        _wallCollider = GetComponent<BoxCollider2D>();    
    }

    private void Update()
    {
        _wallCollider.enabled = !_doorStatus.IsOpen;
    }
}

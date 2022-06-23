using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    [SerializeField] DoorOpenner _doorStatus;

    private BoxCollider2D _wallCollider;

    void Start()
    {
        _wallCollider = GetComponent<BoxCollider2D>();    
    }
    
    void Update()
    {
        _wallCollider.enabled = _doorStatus.IsOpen ? false : true;
    }
}

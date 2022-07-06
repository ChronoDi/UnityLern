using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _jumpHeigth;
    [SerializeField] private float _maxSpeed = 4f;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded;

    private const string FromAnimatorIsGrounded = "isGrounded";
    private const string FromAnimatorSpeed = "speed";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D= GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool IsGounded { get; private set; }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out Ground ground))
        {
            _isGrounded = true;
        }
    }

    private void FixedUpdate()
    {
        _animator.SetBool(FromAnimatorIsGrounded, _isGrounded);
        
        float move = Input.GetAxis("Horizontal");

        _animator.SetFloat(FromAnimatorSpeed, Mathf.Abs(move));
        _rigidbody2D.velocity = new Vector2(move * _maxSpeed, _rigidbody2D.velocity.y);
        _spriteRenderer.flipX = move >= 0 ? false : true;
        
    }

    private void Update()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _isGrounded = false;
            _rigidbody2D.AddForce(new Vector2(0, _jumpHeigth));
        }
    }
    
}

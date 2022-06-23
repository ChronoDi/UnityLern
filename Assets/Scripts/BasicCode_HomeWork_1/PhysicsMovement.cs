using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _jumpHeight;

    protected Vector2 targetVelocity;
    protected bool isGrounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.001f;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(_layerMask);
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        var userInput = Input.GetAxis("Horizontal");

        targetVelocity = new Vector2(userInput, 0);

        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("isGrounded", false);
            _velocity.y = _jumpHeight;
        }
        

        animator.SetFloat("speed", Math.Abs(userInput));
        spriteRenderer.flipX = targetVelocity.x >= 0 ? false : true;
    }

    void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = targetVelocity.x * _speed;

        isGrounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    isGrounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }
}
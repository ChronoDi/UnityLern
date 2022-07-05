using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [SerializeField] private float immortalityTime = 1.5f;
    [SerializeField] private DeadPlayer _deadPlayer;
    [SerializeField] private Dancer _dancer;
    [SerializeField] private Camera _camera;
    [SerializeField] private UnityEvent _died;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;
    private bool _isTakeHit;
 
    private const string FromAnimatorTakeHit = "takeHit";

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();

        MaxHealth = 3;
        CurrentHealth = MaxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemySlime>(out EnemySlime enemySlime) && _isTakeHit == false)
        {
            TakeHit(enemySlime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyHead>(out EnemyHead enemyHead) && _isTakeHit == false)
        {
            JumpOnEnemy();
            enemyHead.Slime.TakeHit();
        }

        if (collision.TryGetComponent<Water> (out Water water) || collision.TryGetComponent<Spikes>(out Spikes spikes))
        {
            Die();
        }
    }

    public void Win()
    {
        _camera.transform.parent = null;
        Instantiate(_dancer, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator StartFlashing(EnemySlime enemySlime)
    {
        _isTakeHit = true;
        float deltaTime = 0.2f;
        var WaitDeltaTime = new WaitForSeconds(deltaTime);

        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), enemySlime.GetComponent<BoxCollider2D>(), true);

        for (float currentTime = 0;  currentTime < immortalityTime; currentTime += deltaTime)
        {
            if (_spriteRenderer.material.color.a == 1f)
                ChangeAlpha(_spriteRenderer.material, 0);
            else
                ChangeAlpha(_spriteRenderer.material, 1f);

            yield return WaitDeltaTime;
        }

        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), enemySlime.GetComponent<BoxCollider2D>(), false);

        _isTakeHit = false;
    }

    private void TakeHit(EnemySlime enemySlime)
    {
        _animator.SetTrigger(FromAnimatorTakeHit);
        _audioSource.Play();

        StartCoroutine(StartFlashing(enemySlime));
        DiscardPlayer(enemySlime);
        TakeHealth();
    }

    private void DiscardPlayer(EnemySlime enemySlime)
    {
        float dropForce = 8f;
        Vector2 distance = (transform.position - enemySlime.transform.position).normalized;
        _rigidbody2D.AddForce(distance * dropForce, ForceMode2D.Impulse);
    }

    private void JumpOnEnemy()
    {
        float jumpForce = 8f;

        _rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private void TakeHealth() 
    {
        CurrentHealth--;

        if (CurrentHealth == 0)
        {
            Die();
        }
    }

    private static void ChangeAlpha(Material material, float alphaValue)
    {
        Color oldColor = material.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
        material.SetColor("_Color", newColor);
    }

    private void Die()
    {
        CurrentHealth = 0;
        _camera.transform.parent = null;
        Instantiate(_deadPlayer, transform.position, Quaternion.identity);
        _died.Invoke();
        Destroy(gameObject);
    }
}

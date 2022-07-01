using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    [SerializeField] private float _liftingHeight = 2f;
    [SerializeField] private float _deltaLiftingHeight = 0.1f;
    [SerializeField] private UnityEvent _itemTaken;

    private AudioSource _takeItemSound;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _takeItemSound = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            StartCoroutine(DoOnDestroy());
        }
    }

    private IEnumerator DoOnDestroy()
    {
        _takeItemSound.Play();
        _boxCollider.enabled = false;
        StartCoroutine(PlayAnimationOnTake());
        yield return new WaitForSeconds(_takeItemSound.clip.length);
        Destroy(gameObject);
    }

    private IEnumerator PlayAnimationOnTake()
    {
        float startY = gameObject.transform.position.y;

        for (float i = startY; i < startY + _liftingHeight; i += _deltaLiftingHeight)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, i);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        _itemTaken.Invoke();
    }
}

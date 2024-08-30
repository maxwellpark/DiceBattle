using UnityEngine;
using UnityEngine.Events;

public class Barrier : MonoBehaviour
{
    public int health;
    public string colliderTag;
    public Cell cell;
    public event UnityAction onDamageTaken;
    public event UnityAction onDestroy;

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _hitClip;
    [SerializeField]
    private AudioClip _destroyClip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(colliderTag))
        {
            if (other.TryGetComponent<Bullet>(out var bullet))
            {
                bullet.SpawnExplosion();
            }
            Destroy(other.gameObject);
            health--;
            onDamageTaken?.Invoke();
            if (health <= 0)
            {
                // TODO: Add audio clips 
                //_audioSource.PlayOneShot(_destroyClip);
                Destroy();
            }
            else
            {
                //_audioSource.PlayOneShot(_hitClip);
            }
        }
    }

    private void Destroy()
    {
        if (gameObject == null)
            return;

        cell.hasBarrier = false;
        onDestroy?.Invoke();
        Destroy(gameObject);
    }
}

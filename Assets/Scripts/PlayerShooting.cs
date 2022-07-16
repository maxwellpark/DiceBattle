using UnityEngine;
using UnityEngine.Events;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private float _zOffset;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private int _maxHealth;
    private int _health;
    private string _bulletColliderTag;

    public event UnityAction onPlayerShoot;
    public event UnityAction onDestroy;

    protected virtual void Start()
    {
        _health = _maxHealth;

        // Set tag to collide with based on tag
        if (gameObject.tag == "Player")
            _bulletColliderTag = "EnemyBullet";
        else if (gameObject.tag == "Enemy")
            _bulletColliderTag = "PlayerBullet";
        else
            throw new System.Exception("Invalid tag");
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    protected virtual void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + _zOffset);
        onPlayerShoot?.Invoke();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_bulletColliderTag))
        {
            _health--;
            if (_health <= 0)
                Destroy();
        }
    }

    protected void Destroy()
    {
        if (gameObject == null)
            return;

        onDestroy?.Invoke();
        Destroy(gameObject);
    }
}

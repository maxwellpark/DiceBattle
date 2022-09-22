using System.Collections;
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
    public int health;
    private string _bulletColliderTag;

    [SerializeField]
    protected float _reloadTimeInSeconds;
    public int magSize;
    public int shotsRemaining;

    protected bool _canShoot = true;

    // Events 
    public event UnityAction onShoot;
    public event UnityAction onEmptyMag;
    public event UnityAction onReloadStart;
    public event UnityAction onReloadEnd;
    public event UnityAction<int> onDamageTaken;
    public event UnityAction onDeath;

    public virtual void SetupShooting(int magSize)
    {
        health = _maxHealth;
        this.magSize = magSize;
        shotsRemaining = this.magSize;
    }

    protected virtual void Start()
    {
        // Set tag to collide with based on tag
        if (gameObject.CompareTag("Player"))
            _bulletColliderTag = "EnemyBullet";
        else if (gameObject.CompareTag("Enemy"))
            _bulletColliderTag = "PlayerBullet";
        else
            throw new System.Exception("Invalid tag");

        onEmptyMag += () => StartCoroutine(Reload());
    }

    protected virtual void Update()
    {
        if (!_canShoot)
            return;

        if (shotsRemaining > 0 && Input.GetKeyDown(KeyCode.Space))
            Shoot();

        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Reload());
    }

    public virtual void SetCanShoot(bool canShoot)
    {
        _canShoot = canShoot;
    }

    protected virtual void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + _zOffset);
        shotsRemaining = Mathf.Max(0, shotsRemaining - 1);
        onShoot?.Invoke();
        if (shotsRemaining <= 0)
            onEmptyMag?.Invoke();
    }

    protected virtual IEnumerator Reload()
    {
        shotsRemaining = 0;
        onReloadStart?.Invoke();
        yield return new WaitForSeconds(_reloadTimeInSeconds);
        shotsRemaining = magSize;
        onReloadEnd?.Invoke();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_bulletColliderTag))
        {
            Destroy(other.gameObject);
            health--;
            onDamageTaken?.Invoke(health);
            if (health <= 0)
            {
                Die();
                return;
            }
        }
    }

    protected void Die()
    {
        if (gameObject == null)
            return;

        onDeath?.Invoke();
    }

    public void ClearBullets()
    {
        var bullets = FindObjectsOfType<Bullet>();
        foreach (var bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
    }

    protected virtual void RegisterEvents()
    {
    }
}

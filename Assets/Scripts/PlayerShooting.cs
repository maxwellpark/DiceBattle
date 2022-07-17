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
    private int _health;
    private string _bulletColliderTag;

    [SerializeField]
    private float _reloadTimeInSeconds;
    public int magSize;
    public int shotsRemaining;

    public event UnityAction onShoot;
    public event UnityAction onReloadStart;
    public event UnityAction onReloadEnd;
    public event UnityAction onDeath;

    public virtual void SetupShooting(int magSize)
    {
        _health = _maxHealth;
        this.magSize = magSize;
        shotsRemaining = this.magSize;
    }

    protected virtual void Start()
    {
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
        if (shotsRemaining > 0 && Input.GetKeyDown(KeyCode.Space))
            Shoot();

        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Reload());
    }

    protected virtual void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + _zOffset);
        shotsRemaining = Mathf.Max(0, shotsRemaining - 1);
        onShoot?.Invoke();
    }

    protected virtual IEnumerator Reload()
    {
        onReloadStart?.Invoke();
        shotsRemaining = 0;
        yield return new WaitForSeconds(_reloadTimeInSeconds);
        shotsRemaining = magSize;
        onReloadEnd?.Invoke();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_bulletColliderTag))
        {
            Destroy(other.gameObject);
            _health--;
            if (_health <= 0)
                Die();
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
}

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
    public int _magSize;
    public int _shotsRemaining;

    public event UnityAction onPlayerShoot;
    public event UnityAction onDestroy;

    public void SetupShooting(int magSize)
    {
        _health = _maxHealth;
        _magSize = magSize;
        _shotsRemaining = _magSize;
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
        if (_shotsRemaining > 0 && Input.GetKeyDown(KeyCode.Space))
            Shoot();

        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Reload());
    }

    protected virtual void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + _zOffset);
        onPlayerShoot?.Invoke();
        _shotsRemaining = Mathf.Max(0, _shotsRemaining - 1);
    }

    protected virtual IEnumerator Reload()
    {
        _shotsRemaining = 0;
        yield return new WaitForSeconds(_reloadTimeInSeconds);
        _shotsRemaining = _magSize;
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

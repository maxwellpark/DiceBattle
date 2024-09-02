using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private float _zOffset = 0.4f;
    [SerializeField]
    private int _maxHealth = 3;
    [HideInInspector]
    public int health;
    private string _bulletColliderTag;

    [SerializeField]
    protected float _reloadTimeInSeconds;
    [HideInInspector]
    public int magSize;
    [HideInInspector]
    public int shotsRemaining;

    [SerializeField]
    private AudioSource _shootSource;
    [SerializeField]
    private AudioClip _shootClip;
    [SerializeField]
    private AudioSource _reloadSource;
    [SerializeField]
    private AudioClip _reloadClip;

    private CharacterManager _charManager;
    private GameObject _bulletPrefab; // Set based on characters selected 

    public bool CanShoot { get; set; } = true;

    //Bool for animator get when entity is hit (set animator parameter)
    public bool beenHit { get; set; } = false;

    // Events 
    public event UnityAction onShoot;
    public event UnityAction onEmptyMag;
    public event UnityAction onReloadStart;
    public event UnityAction onReloadEnd;
    public event UnityAction<int> onDamageTaken;
    public event UnityAction onDeath;

    public virtual void SetupShooting(int magSize, GameObject bulletPrefab)
    {
        health = _maxHealth;
        this.magSize = magSize;
        shotsRemaining = this.magSize;
        _bulletPrefab =  bulletPrefab;
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
        _charManager = FindObjectOfType<CharacterManager>();
    }

    protected virtual void Update()
    {
        if (!CanShoot)
            return;

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
        // TODO: Add audio clips 
        //_shootSource.Play();

        if (shotsRemaining <= 0)
            onEmptyMag?.Invoke();
    }

    protected virtual IEnumerator Reload()
    {
        CanShoot = false;
        shotsRemaining = 0;
        onReloadStart?.Invoke();
        // TODO: Add audio clips 
        //_reloadSource.Play();

        yield return new WaitForSeconds(_reloadTimeInSeconds);
        shotsRemaining = magSize;
        CanShoot = true;
        onReloadEnd?.Invoke();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_bulletColliderTag))
        {
            if (other.TryGetComponent<Bullet>(out var bullet))
            {
                bullet.SpawnExplosion();
               //Debug.Log("HITbyBULLET");
                beenHit = true;

                // courotine to reset beenhit
                StartCoroutine(ResetBeenHit());

            }
          
            Destroy(other.gameObject);
            health--;
            onDamageTaken?.Invoke(health);
            


            if (health <= 0)
            {
                beenHit = false;
                Die();
                return;
            }


        }
       

    }

    private IEnumerator ResetBeenHit()
    {
        // Wait for 1 second (or any duration you choose)
        yield return new WaitForSeconds(0.1f);

        // Reset the bool back to false
        beenHit = false;
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

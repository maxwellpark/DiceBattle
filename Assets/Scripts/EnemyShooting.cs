using System.Collections;
using UnityEngine;

public class EnemyShooting : PlayerShooting
{
    [SerializeField]
    private float _shootDelayInSeconds;
    private bool _canShoot = true;

    protected override void Start()
    {
        base.Start();
        onPlayerShoot += () => StartCoroutine(DelayShooting());
    }

    protected override void Update()
    {
        if (!_canShoot)
            return;

        if (_shotsRemaining <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        Shoot();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    private IEnumerator DelayShooting()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_shootDelayInSeconds);
        _canShoot = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : PlayerShooting
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        //base.Update();
    }

    protected override void Shoot()
    {
        base.Shoot();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}

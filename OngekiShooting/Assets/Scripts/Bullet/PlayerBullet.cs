using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public override void Move()
    {
        Vector3 velocity = new Vector3(0, 0, speed);
        transform.position += velocity * Time.deltaTime;
    }

    public override void DestroyBullet(Collider other)
    {
        if (other.tag == "PlayerBullet") return;
        if (other.tag == "EnemyBullet") return;
        if (other.tag == "ReflectEnemy") return;
        Destroy(gameObject);
    }    
}

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
        if (!DeadObj(other)) return;
        Destroy(gameObject);
    }

    bool DeadObj(Collider other)
    {
        if (isReflect) return other.tag == "Player"|| other.tag == "ReflectBullet";
        return other.tag == "Enemy" || other.tag == "EnemyBullet";
    }
}

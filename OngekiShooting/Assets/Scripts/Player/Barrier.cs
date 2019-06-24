using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ReflectBullet(other);
    }

    private void ReflectBullet(Collider other)
    {
        bool reflectObj = other.tag == "EnemyBullet" || other.tag == "DeadBullet";
        if (!reflectObj) return;
        var bullet = other.GetComponent<Bullet>();
        bullet.SetSpeed(-bullet.GetSpeed());
        bullet.gameObject.tag = "PlayerBullet";
        bullet.isReflect = true;
    }
}

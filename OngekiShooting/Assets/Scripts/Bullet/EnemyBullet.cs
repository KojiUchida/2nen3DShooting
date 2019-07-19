using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    Transform player;
    public bool kind = false;
    Vector3 playerPos;

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>()?.transform;
        if (player == null) return;
        playerPos = player.transform.position;
    }
    public override void Move()
    {
        base.Move();
        Vector3 velocity = new Vector3(0, 0, -speed);
        velocity = Tracking( velocity);
        transform.position += velocity * Time.deltaTime;
    }

    public override void DestroyBullet(Collider other)
    {
        if (!DeadObj(other)) return;
        Destroy(gameObject);
    }

    bool DeadObj(Collider other)
    {
        if (isReflect) return other.tag == "Enemy" || other.tag == "ReflectEnemy" || other.tag == "DeadBullet" || other.tag == "EnemyReflectBullet";
        return other.tag == "Player" || other.tag == "PlayerBullet";
    }

    private Vector3 Tracking(Vector3 velocity)
    {
        if (kind == false || player == null) return velocity;
        velocity = new Vector3(playerPos.x - transform.position.x, 0, -speed);
        return velocity;
    }
}

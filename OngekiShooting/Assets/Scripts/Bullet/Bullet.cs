using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Header("攻撃力")]
    protected int damage = 1;
    [SerializeField, Header("弾の速度")]
    protected float speed = 100;

    [HideInInspector]
    public bool isReflect;

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyBullet(other);
    }

    public virtual void DestroyBullet(Collider other)
    {

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public virtual void Move()
    {

    }

    public float GetSpeed() { return speed; }
    public void SetSpeed(float speed) { this.speed = speed; }

    public int GetDamage() { return damage; }
    public void SetDamage(int damage) { this.damage = damage; }
}

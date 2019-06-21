using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField, Header("攻撃力")]
    public int damage = 1;
    [SerializeField, Header("弾の速度")]
    public float speed = 100;

    private void Start()
    {

    }


    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet") return;
        if (other.tag == "EnemyBullet") return;
        if (other.tag == "ReflectEnemy") return;
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Move()
    {
        Vector3 velocity = new Vector3(0, 0, speed);
        transform.position += velocity * Time.deltaTime;
    }

    public float GetSpeed() { return speed; }
    public void SetSpeed(float speed) { this.speed = speed; }
}

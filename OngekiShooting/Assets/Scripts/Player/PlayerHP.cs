using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp;//ヒットポイント
    public float invincibleTime = 3;//無敵時間
    private float countTime;//時間カウント
    void Start()
    {
        countTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        countTime += Time.deltaTime;
    }

    void Death()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool damageTag = other.tag == "EnemyBullet" || other.tag == "EnemyReflectBullet";
        if (!damageTag) return;
        if (invincibleTime > countTime) return;
        var bullet = other.GetComponent<Bullet>();
        hp -= bullet.GetDamage();
        countTime = 0;
    }

    public void Damage(int damage)
    {
        hp -= damage;
    }
}

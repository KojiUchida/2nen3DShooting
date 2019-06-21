using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadShotAI : AI
{
    [SerializeField, Header("通常弾")]
    protected GameObject enemyBullet;
    [SerializeField, Header("死亡弾")]
    protected GameObject deadBullet;
    [SerializeField, Header("弾の間隔")]
    private float shotInterval = 0.5f;

    float count;

    public override void Init()
    {
        base.Init();
        count = shotInterval;
    }

    public override void Attack()
    {
        Shot();
    }

    private void Shot()
    {
        count -= Time.deltaTime;
        if (count > 0) return;
        count = shotInterval;
        Instantiate(enemyBullet, transform.position, Quaternion.identity);
    }

    public override void Death()
    {
        if (hp > 0) return;
        DeadShot();
        base.Death();
    }

    void DeadShot()
    {
        Instantiate(deadBullet, transform.position, Quaternion.identity);
    }
}

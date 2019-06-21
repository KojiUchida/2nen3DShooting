using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadShotAI : AI
{
    [SerializeField, Header("弾のプレファブ")]
    protected GameObject enemyBullet;

    public override void Death()
    {
        if (hp > 0) return;
        Shot();
        base.Death();
    }

    void Shot()
    {
        Instantiate(enemyBullet, transform.position, Quaternion.identity);
    }
}

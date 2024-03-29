﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAI : AI
{
    [SerializeField, Header("弾のプレファブ")]
    protected GameObject enemyBullet;
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
}

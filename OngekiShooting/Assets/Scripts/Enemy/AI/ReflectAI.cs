﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectAI : AI
{
    [SerializeField, Header("弾のプレファブ")]
    private GameObject enemyBullet;
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
        //PlaySE(ses[1]);
    }

    public override void HitPlayerBullet(Collider other)
    {
        ReflectBullet(other);
        base.HitPlayerBullet(other);
    }

    private void ReflectBullet(Collider other)
    {
        if (other.tag != "PlayerBullet") return;
        var bullet = other.GetComponent<PlayerBullet>();
        bullet.SetSpeed(-bullet.GetSpeed());
        bullet.gameObject.tag = "EnemyReflectBullet";
        bullet.isReflect = true;
        //ses[2].Play();
    }
}

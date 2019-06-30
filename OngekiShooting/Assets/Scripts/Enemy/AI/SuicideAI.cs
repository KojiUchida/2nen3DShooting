using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideAI : AI
{
    [SerializeField, Header("爆発範囲")]
    float explodeRange = 10;
    [SerializeField, Header("爆発ダメージ")]
    int damage = 1;

    Transform player;

    public override void Init()
    {
        base.Init();
        player = FindObjectOfType<PlayerHP>()?.transform;
    }

    public override void Attack()
    {
        Explode();
    }

    private void Explode()
    {
        if (!IsExplode()) return;
        Collider[] targets = Physics.OverlapSphere(transform.position, explodeRange);
        foreach (Collider obj in targets)
        {
            if (obj.tag == "Player") obj.gameObject.GetComponent<PlayerHP>().Damage(damage);
            if (obj.tag == "Enemy") obj.gameObject.GetComponent<AI>().Damage(damage);
        }
        Destroy(gameObject);
    }

    bool IsExplode()
    {
        if (player == null) return false;
        if (!player.gameObject.activeSelf) return false;
        Vector3 vec = player.position - transform.position;
        bool isRange = vec.magnitude < explodeRange;
        bool isDead = hp <= 0;
        return isRange || isDead;
    }
}

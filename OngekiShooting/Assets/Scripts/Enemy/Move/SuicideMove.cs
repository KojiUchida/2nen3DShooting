using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideMove : EnemyMovement
{
    [SerializeField, Header("x位置調整速度")]
    float adjustSpeed = 0.01f;

    Transform player;

    public override void Init()
    {
        player = FindObjectOfType<PlayerMove>().transform;
    }

    public override void Move()
    {
        Vector3 velocity = new Vector3(0, 0, -moveSpeed);
        transform.position += velocity;
        Adjust();
    }

    private void Adjust()
    {
        if (player == null) return;
        float adjust = (player.position.x - transform.position.x) * adjustSpeed;
        Vector3 velocity = new Vector3(adjust, 0, 0);
        transform.position += velocity;
    }
}

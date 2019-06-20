using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMove : EnemyMovement
{
    public override void Move()
    {
        Vector3 velocity = new Vector3(0, 0, -moveSpeed);
        transform.position += velocity;
    }
}

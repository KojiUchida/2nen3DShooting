using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnData
{
    public float spawnTiming;
    public Vector3 position;
    public float speed;
    public EnemyType enemyType;
    public MoveType moveType;

    public EnemySpawnData(float spawnTiming, Vector3 position, float speed, EnemyType enemyType, MoveType moveType)
    {
        this.spawnTiming = spawnTiming;
        this.position = position;
        this.speed = speed;
        this.enemyType = enemyType;
        this.moveType = moveType;
    }
}

public enum EnemyType
{
    Default,
    Suicide,
    Reflect,
}

public enum MoveType
{
    None,
    Default,
    Sucide,
}

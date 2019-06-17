using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnData
{
    public float spawnTiming;
    public Vector3 position;
    public EnemyType enemyType;
    public MoveType moveType;

    public EnemySpawnData(float spawnTiming, Vector3 position, EnemyType enemyType, MoveType moveType)
    {
        this.spawnTiming = spawnTiming;
        this.position = position;
        this.enemyType = enemyType;
        this.moveType = moveType;
    }
}

public enum EnemyType
{

}

public enum MoveType
{
    Suicide,

}

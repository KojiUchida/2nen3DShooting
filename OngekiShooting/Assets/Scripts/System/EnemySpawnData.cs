using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnData
{
    Vector3 position;
    EnemyType enemyType;
    MoveType moveType;

    public EnemySpawnData(Vector3 position, EnemyType enemyType, MoveType moveType)
    {
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

}

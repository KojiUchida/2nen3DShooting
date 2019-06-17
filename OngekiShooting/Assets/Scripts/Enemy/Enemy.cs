using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyManager enemyManager;
    MoveType moveType;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public virtual void Init()
    {
        enemyManager = FindObjectOfType<EnemyManager>();

    }

    public void SetMove(MoveType moveType)
    {
        this.moveType = moveType;
    }

    public virtual void Move()
    {

    }

    public virtual void Shot()
    {

    }

    public virtual void Death()
    {

    }
}

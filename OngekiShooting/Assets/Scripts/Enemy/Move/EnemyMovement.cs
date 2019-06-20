using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    protected float moveSpeed = 1.0f;

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

    }

    public virtual void Move()
    {

    }
}

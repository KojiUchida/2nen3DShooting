using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool bossShootFlag;
    public float speed = 5;
    Rigidbody rigidbody;
    public GameObject player;
    private int behaviour;//行動番号
    public int coolTime = 3;
    private float countTime;

    void Start()
    {
        bossShootFlag = false;
        rigidbody = GetComponent<Rigidbody>();
        behaviour = 0;
        countTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch(behaviour)
        {
            case 0:Move();break;
            case 1:Attack(); break;
            case 2:break;
            case 3:break;
            case 4:break;
        }
        countTime += Time.deltaTime;
        if (behaviour > 4) 
        {
            behaviour = 0;
        }
    }

    void Move()
    {
        behaviour++;
    }

    void Attack()
    {
        bossShootFlag = true;
        if (coolTime < countTime)
        {
            countTime = 0;
            behaviour++;
        }
    }
}

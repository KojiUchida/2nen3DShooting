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
    public GameObject enemyBullet;
    private int attackBehaviour;//行動番号
    private int hpBehaviour;//体力で行動変化
    public float attackTime1 = 3;
    public float attackTime2 = 5;
    public int maxHp = 100;
    public int hp;
    public float rightBulletPos = 10;
    public float leftBulletPos = 10;

    private float countTime;

    void Start()
    {
        bossShootFlag = false;
        rigidbody = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerMove>().gameObject;
        attackBehaviour = 0;
        hpBehaviour = 0;
        countTime = 0;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {

        Dead();
        switch(attackBehaviour)
        {
            case 1:Attack1();break;
            case 2:Attack2();break;
            case 3:Attack3();break;
        }
        switch(hpBehaviour)
        {
            case 0: Move1(); break;
            case 1: Move2(); break;
            case 2: Move3(); break;
        }
        countTime += Time.deltaTime;

        if (hp <= maxHp / 3) 
        {
            attackBehaviour = 2;
            hpBehaviour = 2;
        }
        else if (hp <= (maxHp / 3) * 2) 
        {
            attackBehaviour = 1;
            hpBehaviour = 1;
        }
    }

    void Move1()
    {
        rigidbody.velocity += new Vector3(player.transform.position.x - rigidbody.position.x, 0, 0) * speed * Time.deltaTime;
    }

    void Move2()
    {
        rigidbody.velocity += new Vector3(0 - rigidbody.position.x, 0, 0) * speed * Time.deltaTime;
    }

    void Move3()
    {
        rigidbody.velocity += new Vector3(0 - rigidbody.position.x, 0, 0) * speed * Time.deltaTime;
        //x+Screen.width/2右端
        //x-Screen.width/2左端
    }

    void Attack1()
    {
        if (attackTime1 <= countTime)
        {
            Instantiate(enemyBullet, transform.position + new Vector3(rightBulletPos, 0), Quaternion.identity);
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            Instantiate(enemyBullet, transform.position - new Vector3(leftBulletPos, 0), Quaternion.identity);
            countTime = 0;
        }
    }

    void Attack2()
    {
        if(attackTime2<=countTime)
        {
            countTime = 0;
        }
    }

    void Attack3()
    {

    }

    void Dead()
    {
        if(hp<=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="PlayerBullet")
        {
            hp--;
        }
    }
}

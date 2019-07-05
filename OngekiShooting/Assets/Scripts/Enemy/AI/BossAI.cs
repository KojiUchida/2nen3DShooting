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
    public GameObject bulletPrefab;
    private Bullet bullet;
    private int attackBehaviour;//行動番号
    private int hpBehaviour;//体力で行動変化
    public float attackTime1 = 3;
    public float attackTime2 = 5;
    public int maxHp = 100;
    public int hp;
    public float rightBulletPos = 10;
    public float leftBulletPos = 10;
    private bool right;//右に移動しているかどうか
    private float countTime;

    public GameObject enemy1;
    public GameObject enemy2;

    public GameObject bossBullet;
    public Transform muzzle;
    public float shootsTime = 0.2f;
    private float time;

    void Start()
    {
        bossShootFlag = false;
        rigidbody = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerMove>()?.gameObject;
        attackBehaviour = 0;
        hpBehaviour = 0;
        countTime = 0;
        hp = maxHp;
        bullet = bulletPrefab.GetComponent<Bullet>();
        bullet.SetSpeed(200);
        right = false;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Shoots();
        Dead();
        switch(attackBehaviour)
        {
            case 0:Attack1();break;
            case 1:Attack2();break;
            case 2:Attack3();break;
        }
        switch(hpBehaviour)
        {
            case 0: Move1(); break;
            case 1: Move2(); break;
            case 2: Move3(); break;
        }
        countTime += Time.deltaTime;
        time += Time.deltaTime;

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
        rigidbody.velocity = new Vector3(player.transform.position.x - rigidbody.position.x, 0, 0) * speed * Time.deltaTime;
    }

    void Move2()
    {
        rigidbody.velocity = new Vector3(0 - rigidbody.position.x, 0, 0) * speed * Time.deltaTime;
    }

    void Move3()
    {
        if(right)
        {
            rigidbody.velocity = new Vector3(1, 0, (20 - rigidbody.position.z)*0.11f) * speed * Time.deltaTime;
        }
        else
        {
            rigidbody.velocity = new Vector3(-1, 0, (20 - rigidbody.position.z) * 0.11f) * speed * Time.deltaTime;
        }

        //画面の端にきているかどうか
        if (rigidbody.position.x >= 5) 
        {
            right = false;
        }
        if (rigidbody.position.x <=  -5) 
        {
            right = true;
        }
    }

    void Attack1()
    {
        if (attackTime1 <= countTime)
        {

            Instantiate(bulletPrefab, transform.position + new Vector3(rightBulletPos, 0), Quaternion.identity);
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Instantiate(bulletPrefab, transform.position - new Vector3(leftBulletPos, 0), Quaternion.identity);
            countTime = 0;
        }
    }

    void Attack2()
    {
        if(attackTime2<=countTime)
        {
            countTime = 0;
            if (enemy1 == null || enemy2 == null) return;
            Instantiate(enemy1, transform.position + new Vector3(6, 0), Quaternion.identity);
            Instantiate(enemy2, transform.position + new Vector3(-6, 0), Quaternion.identity);
        }
    }

    void Attack3()
    {

    }

    void Dead()
    {
        if(hp<=0)
        {
            SceneState.isClear = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(attackBehaviour==2)
        {
            ReflectBullet(other);
            if (other.tag == "ReflectBullet")
            {
                hp--;
            }
        }
        else
        {
            if (other.tag == "PlayerBullet")
            {
                hp--;
            }
        }
    }

    private void ReflectBullet(Collider other)
    {
        if (other.tag != "PlayerBullet") return;
        var bullet = other.GetComponent<PlayerBullet>();
        bullet.SetSpeed(-bullet.GetSpeed());
        bullet.gameObject.tag = "EnemyReflectBullet";
        bullet.isReflect = true;
    }

    void Shoots()
    {
        if (time < shootsTime) return;
        Instantiate(bossBullet, muzzle.position, Quaternion.identity);
        time = 0;
    }
}

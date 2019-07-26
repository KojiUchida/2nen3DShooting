using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool bossShootFlag;
    public float speed = 5;
    public int bulletSpeed = 50;
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
    private float barierTime;//バリア用
    private bool barierFlag;//バリア用フラグ
    private float migrationTime;//移動用
    private bool migrationFlag;//移動用フラグ

    [SerializeField, Header("死亡時パーティクル")]
    ParticleSystem deadParticle;


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
        bullet.SetSpeed(bulletSpeed);
        right = false;
        time = 0;
        barierTime = 0;
        barierFlag = false;
        migrationTime = 0;
        migrationFlag = false;

    }

    // Update is called once per frame
    void Update()
    {
        Shoots();
        Dead();
        switch (attackBehaviour)
        {
            case 0: Attack1(); break;
            case 1: Attack2(); break;
                //case 2:Attack3();break;
        }
        switch (hpBehaviour)
        {
            case 0: Move1(); break;
            case 1: Move2(); break;
                //case 2: Move3(); break;
        }
        countTime += Time.deltaTime;
        time += Time.deltaTime;
        barierTime += Time.deltaTime;
        if (hp <= maxHp / 2)
        {
            attackBehaviour = 1;
            hpBehaviour = 1;
        }

        if (barierTime >= 5)
        {
            if (barierFlag) barierFlag = false;
            else barierFlag = true;

            barierTime = 0;
        }
    }

    void Move1()
    {
        rigidbody.velocity = new Vector3(player.transform.position.x - rigidbody.position.x, 0, 0) * speed * Time.deltaTime;
    }

    void Move2()
    {
        migrationTime += Time.deltaTime;
        if (migrationFlag) rigidbody.velocity = new Vector3(player.transform.position.x - rigidbody.position.x, 0, 0) * speed * Time.deltaTime;
        else rigidbody.velocity = Vector3.zero;

        if (migrationTime >= 3)
        {
            if (migrationFlag) migrationFlag = false;
            else migrationFlag = true;

            migrationTime = 0;
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
        if (attackTime2 <= countTime)
        {
            countTime = 0;
            if (enemy1 == null || enemy2 == null) return;
            float x = Random.Range(-6, 6);
            Instantiate(enemy1, transform.position + new Vector3(x, 0), Quaternion.identity);
            x = Random.Range(-6, 6);
            Instantiate(enemy2, transform.position + new Vector3(x, 0), Quaternion.identity);
        }
    }

    void Dead()
    {
        if (hp <= 0)
        {
            Instantiate(deadParticle, transform.position, Quaternion.identity);
            SceneState.isBossDead = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attackBehaviour == 2)
        {
            ReflectBullet(other);
            if (other.tag == "ReflectBullet")
            {
                hp--;
            }
        }
        else
        {
            if (other.tag == "PlayerBullet" || other.tag == "ReflectBullet")
            {
                hp--;
            }
        }
    }

    private void ReflectBullet(Collider other)
    {
        if (other.tag != "PlayerBullet") return;
        if (!barierFlag) return;
        var bullet = other.GetComponent<PlayerBullet>();
        bullet.SetSpeed(-bullet.GetSpeed());
        bullet.gameObject.tag = "EnemyReflectBullet";
        bullet.isReflect = true;
    }

    void Shoots()
    {
        if (time < shootsTime) return;
        if (attackBehaviour == 0) return;
        Instantiate(bossBullet, muzzle.position, Quaternion.identity);
        time = 0;
    }
}

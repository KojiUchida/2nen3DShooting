using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigid;
    public float speed = 5;
    [SerializeField, Header("ステージ横幅")]
    float stageWidth = 5.0f;
    [SerializeField, Header("全ゲージ回復までの時間")]
    public float gaugeTime = 60.0f;
    [SerializeField, Header("1段階目の回復量")]
    int firstHeal = 10;
    [SerializeField, Header("2段階目の回復量")]
    int secondHeal = 20;
    [SerializeField, Header("3段階目の回復量")]
    int maxHeal = 30;

    PlayerHP playerHP;

    [HideInInspector]
    public float gauge;

    float rotVelocity = 0;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        playerHP = GetComponent<PlayerHP>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.isPause) return;
        Gauge();
        Move();
        Reflection();
    }

    private void Gauge()
    {
        gauge += Time.deltaTime;
        gauge = Mathf.Clamp(gauge, 0, gaugeTime);
    }

    void Move()
    {
        Vector3 vel = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, 0);
        rigid.velocity = vel;
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -stageWidth, stageWidth);
        transform.position = pos;
        Tilt(vel);
    }

    void Tilt(Vector3 velocity)
    {
        Vector3 rot = transform.rotation.eulerAngles;

        if (velocity.x > 0)
        {
            rot.z = -30;
            transform.rotation = Quaternion.Euler(rot);
        }
        else if (velocity.x < 0)
        {
            rot.z = 30;
            transform.rotation = Quaternion.Euler(rot);
        }
        else
        {
            rot.z = 0;
            transform.rotation = Quaternion.Euler(rot);
        }
    }

    void Reflection()
    {
        if (!Input.GetKeyDown(KeyCode.X)) return;
        if (gauge / gaugeTime < 1.0f / 3.0f) return;
        gauge -= gaugeTime / 3;

        if (gauge / gaugeTime == 1.0f) MaxReflection();
        else if (gauge / gaugeTime < 2.0f / 3.0f) SecondReflection();
        else FirstReflection();
    }

    void Reflect(Bullet b)
    {
        if (b.tag == "PlayerBullet" || b.tag == "ReflectBullet") return;
        b.tag = "ReflectBullet";
        b.isReflect = true;
        b.SetSpeed(-b.GetSpeed());
    }

    void FirstReflection()
    {
        playerHP.hp += firstHeal;
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (var b in bullets)
        {
            Reflect(b);
        }
    }

    private void SecondReflection()
    {
        playerHP.hp += secondHeal;
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (var b in bullets)
        {
            Reflect(b);
        }
    }

    private void MaxReflection()
    {
        playerHP.hp += maxHeal;
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (var b in bullets)
        {
            Reflect(b);
        }
    }
}

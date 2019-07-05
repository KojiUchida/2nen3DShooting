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
        rigid.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, 0);
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -stageWidth, stageWidth);
        transform.position = pos;
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

    void FirstReflection()
    {
        playerHP.hp += firstHeal;
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (var b in bullets)
        {
            if (b.tag != "PlayerBullet")
            {
                b.tag = "ReflectBullet";
                b.isReflect = true;
                b.SetSpeed(-b.GetSpeed());
            }
        }
    }

    private void SecondReflection()
    {
        playerHP.hp += secondHeal;
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (var b in bullets)
        {
            if (b.tag != "PlayerBullet")
            {
                b.tag = "ReflectBullet";
                b.isReflect = true;
                b.SetSpeed(-b.GetSpeed());
            }
        }
    }

    private void MaxReflection()
    {
        playerHP.hp += maxHeal;
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (var b in bullets)
        {
            if (b.tag != "PlayerBullet")
            {
                b.tag = "ReflectBullet";
                b.isReflect = true;
                b.SetSpeed(-b.GetSpeed());
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public float justGuardTime = 1;//ジャストガードまでの時間
    public static float barrierCountTime;//ジャストガード用カウントタイム
    public GameObject barrierArea;
    private AudioSource[] ses;

    private void Start()
    {
        barrierCountTime = 0;
        barrierArea = GetComponentInChildren<BarrierArea>().gameObject;
        barrierArea.gameObject.SetActive(false);
        ses = GetComponentsInParent<AudioSource>();
    }

    private void Update()
    {
        barrierCountTime += Time.deltaTime;
        //if (barrierCountTime >= 1.0f)
        //{
        //    barrierArea.gameObject.SetActive(false);
        //}
        if (!PlayerMove.barrierFlag)
        {
            barrierArea.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ReflectBullet(other);
    }

    private void ReflectBullet(Collider other)
    {
        bool reflectObj = other.tag == "EnemyBullet" || other.tag == "DeadBullet";
        if (!reflectObj) return;
        JustGuard();
        barrierCountTime = 0;
        var bullet = other.GetComponent<Bullet>();
        bullet.SetSpeed(-bullet.GetSpeed());
        bullet.gameObject.tag = "ReflectBullet";
        bullet.isReflect = true;
    }

    private void JustGuard()
    {
        if (justGuardTime >= barrierCountTime)//ジャストガード成功
        {
            PlayerMove.barrierGauge = 0;//ゲージを0に
            barrierArea.gameObject.SetActive(true);
            PlayerMove.barrierFlag = false;
        }
    }
}

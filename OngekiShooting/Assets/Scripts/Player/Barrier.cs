using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public float justGuardTime = 1;//ジャストガードまでの時間
    public static float barrierCountTime;//ジャストガード用カウントタイム

    private void Start()
    {
        barrierCountTime = 0;
    }

    private void Update()
    {
        barrierCountTime += Time.deltaTime;
        Debug.Log(barrierCountTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        ReflectBullet(other);
    }

    private void ReflectBullet(Collider other)
    {
        bool reflectObj = other.tag == "EnemyBullet" || other.tag == "DeadBullet";
        if (!reflectObj) return;
        if (justGuardTime >= barrierCountTime)//ジャストガード成功
        {
            //内田よろ～
            Debug.Log("ジャストバリア成功！！");
            PlayerMove.barrierGauge = PlayerMove.maxBarrierGauge;//ゲージを最大に
            PlayerMove.barrierFlag = false;
        }
        barrierCountTime = 0;
        var bullet = other.GetComponent<Bullet>();
        bullet.SetSpeed(-bullet.GetSpeed());
        bullet.gameObject.tag = "ReflectBullet";
        bullet.isReflect = true;
    }
}

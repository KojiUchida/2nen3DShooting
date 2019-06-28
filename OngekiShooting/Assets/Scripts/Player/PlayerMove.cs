using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody;
    public float speed = 5;
    public GameObject barrier;
    public static bool barrierFlag;//バリア状態かどうか
    
    public static float maxBarrierGauge = 100;//最大バリアゲージ
    public static float barrierGauge;//現在のバリア用ゲージ
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        barrierFlag = false;
        barrier = GetComponentInChildren<Barrier>().gameObject;
        barrierGauge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.isPause) return;
        rigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, 0);
        if(rigidbody.position.x>5)
        {
            rigidbody.position = new Vector3(5f, 0);
        }
        if(rigidbody.position.x<-5)
        {
            rigidbody.position = new Vector3(-5f, 0);
        }
        BarrierMode();
        if (Input.GetKeyDown(KeyCode.X))
        {
            //if (barrierFlag) { barrierFlag = false; countTime = 0; }
            //else
            //{
            //    barrierFlag = true;
            //    countTime += Time.deltaTime;
            //}

            if(maxBarrierGauge==barrierGauge)
            {
                barrierFlag = true;
            }
        }

        if (barrierFlag)
        {
            barrierGauge--;
            if (barrierGauge <= 0)
            {
                barrierFlag = false;
            }
        }
        else
        {
            barrierGauge++;
            if (barrierGauge > maxBarrierGauge) barrierGauge = maxBarrierGauge;
        }
    }

    void BarrierMode()
    {
        if (barrier == null) return;
        //switch (barrierFlag)
        //{
        //    case true:
        //        barrier.SetActive(true); break;
        //    case false:
        //        barrier.SetActive(false); break;
        //}

        if (barrierFlag)
        {
            barrier.gameObject.SetActive(true);
        }
        else
        {
            Barrier.barrierCountTime = 0;
            barrier.gameObject.SetActive(false);
        }
    }
}

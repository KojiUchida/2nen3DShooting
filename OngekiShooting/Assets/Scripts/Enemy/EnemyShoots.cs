using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoots : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public Transform muzzle;
    public float speed = 100;
    public float shootsTime = 5;//弾を撃ってくる間の時間
    private float countTime;
    void Start()
    {
        countTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Shoots();
    }

    void Shoots()
    {
        countTime += Time.deltaTime;
        if (shootsTime <= countTime)
        {
            GameObject bullets = Instantiate(bullet) as GameObject;
            Vector3 force;
            force = this.gameObject.transform.forward * (-speed);
            bullets.GetComponent<Rigidbody>().AddForce(force);
            bullets.transform.position = muzzle.position;
            countTime = 0;
        }
    }
}

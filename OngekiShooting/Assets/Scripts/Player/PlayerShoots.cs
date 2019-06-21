using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoots : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public Transform muzzle;
    public float speed = 100;
    public float shootsTime = 0.2f;
    private float time;
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Shoots();
        time += Time.deltaTime;
    }

    void Shoots()
    {
        if (!PlayerMove.barrierFlag)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                if (time >= shootsTime)
                {
                    GameObject bullets = Instantiate(bullet) as GameObject;
                    Vector3 force;
                    force = this.gameObject.transform.forward * speed;
                    bullets.GetComponent<Rigidbody>().AddForce(force);
                    bullets.transform.position = muzzle.position;
                    time = 0;
                }
            }
        }
    }
}

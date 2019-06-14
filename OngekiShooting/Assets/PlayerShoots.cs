using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoots : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public Transform muzzle;
    public float speed = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoots();
    }

    void Shoots()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject bullets = Instantiate(bullet) as GameObject;
            Vector3 force;
            force = this.gameObject.transform.forward * speed;
            bullets.GetComponent<Rigidbody>().AddForce(force);
            bullets.transform.position = muzzle.position;
        }
    }
}

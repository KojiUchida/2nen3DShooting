using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "PlayerBullet" || collision.transform.tag == "EnemyBullet")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "PlayerBullet" || other.tag == "EnemyBullet")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}

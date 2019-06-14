using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigidbody;
    public float speed = 5;
    public GameObject player;
    private Rigidbody playerRig;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerRig = player.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        rigidbody.position += new Vector3(playerRig.position.x - rigidbody.position.x, 0,-10) *  Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Kabe")
        {
        }
    }
}

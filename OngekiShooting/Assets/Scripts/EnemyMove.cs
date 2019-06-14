using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigidbody;
    public float speed = 5;
    private bool Right;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Right = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Right)
        {
            rigidbody.velocity += new Vector3(speed, 0, 0);
        }
        else if(!Right)
        {
            rigidbody.velocity += new Vector3(-speed, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Kabe")
        {
            switch (Right)
            {
                case true:Right = false;break;
                case false:Right = true;break;
            }
        }
    }
}

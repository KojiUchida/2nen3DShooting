using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    public float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 velocity = new Vector3(0, 0, -moveSpeed);
        transform.position += velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet") return;
        if (other.tag == "EnemyBullet") return;
        if (other.tag == "Enemy") return;
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

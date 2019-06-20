using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField, Header("攻撃力")]
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") return;
        if (other.tag == "PlayerBullet") return;
        if (other.tag == "EnemyBullet") return;
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

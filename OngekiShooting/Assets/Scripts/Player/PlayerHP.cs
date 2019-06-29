using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp;//ヒットポイント
    public float invincibleTime = 3;//無敵時間

    private bool isDamage;
    Material mat;

    void Start()
    {
        isDamage = false;
        mat = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        Blink();
    }

    void Death()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool damageTag = other.tag == "EnemyBullet" || other.tag == "EnemyReflectBullet";
        if (!damageTag) return;
        if (isDamage) return;
        var bullet = other.GetComponent<Bullet>();
        Damage(bullet.GetDamage());
    }

    public void Damage(int damage)
    {
        if (isDamage) return;
        hp -= damage;
        StartCoroutine(DamageCoroutine());
    }

    IEnumerator DamageCoroutine()
    {
        isDamage = true;
        yield return new WaitForSeconds(invincibleTime);
        isDamage = false;
        mat.color = new Color(1f, 1f, 1f, 1);
    }

    void Blink()
    {
        if (!isDamage) return;
        float level = Mathf.Abs(Mathf.Sin(Time.time * 10f));
        mat.color = new Color(1f, 1f, 1f, level);
    }
}


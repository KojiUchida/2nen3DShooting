using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHP = 100;
    public float invincibleTime = 3;//無敵時間

    [HideInInspector]
    public int hp;//ヒットポイント
    private bool isDamage;
    Material mat;
    private AudioSource[] ses;

    void Start()
    {
        hp = maxHP;
        isDamage = false;
        mat = gameObject.GetComponent<MeshRenderer>().material;
        ses = GetComponents<AudioSource>();
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
            ses[0].Play();
            gameObject.SetActive(false);
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
        if (PlayerMove.barrierFlag) return;
        if (damage > hp) damage = hp;
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


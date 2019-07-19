using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHP = 100;
    public float invincibleTime = 3;//無敵時間
    [SerializeField, Header("死亡時パーティクル")]
    ParticleSystem deadParticle;
    [SerializeField, Header("点滅までの時間(フレーム)")]
    int blinkTime = 5;

    [HideInInspector]
    public int hp;//ヒットポイント
    private bool isDamage;
    MeshRenderer mesh;
    private int blinkCount;

    void Start()
    {
        hp = maxHP;
        isDamage = false;
        mesh = gameObject.GetComponentInChildren<MeshRenderer>();
        blinkCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        hp = Mathf.Clamp(hp, 0, maxHP);
        Death();
        Blink();
    }

    void Death()
    {
        if (hp > 0) return;
        Instantiate(deadParticle, transform.position, Quaternion.identity);
        SceneState.isDead = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool damageTag = other.tag == "EnemyBullet" || other.tag == "DeadBullet" || other.tag == "EnemyReflectBullet";
        if (!damageTag) return;
        if (isDamage) return;
        var bullet = other.GetComponent<Bullet>();
        Damage(bullet.GetDamage());
    }

    public void Damage(int damage)
    {
        if (isDamage) return;
        if (damage > hp) damage = hp;
        hp -= damage;
        StartCoroutine(DamageCoroutine());
    }

    IEnumerator DamageCoroutine()
    {
        isDamage = true;
        yield return new WaitForSeconds(invincibleTime);
        isDamage = false;
        mesh.enabled = true;
    }

    void Blink()
    {
        if (!isDamage) return;
        blinkCount++;
        if (blinkCount > blinkTime)
        {
            mesh.enabled = !mesh.enabled;
            blinkCount = 0;
        }
    }
}


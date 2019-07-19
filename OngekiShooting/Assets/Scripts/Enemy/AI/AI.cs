using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField, Header("最大HP")]
    protected int maxHP = 10;
    [SerializeField, Header("獲得できるスコア")]
    int score = 10;
    [SerializeField, Header("死亡時パーティクル")]
    ParticleSystem deadParticle;
    [SerializeField, Header("点滅の間隔")]
    float blinkDelay = 0.05f;

    Color damageColor;
    protected int hp;
    protected Material mat;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Death();
    }

    public virtual void Init()
    {
        damageColor = new Color(0.1f, 0.1f, 0.1f, 1.0f);
        hp = maxHP;
        mat = GetComponentInChildren<MeshRenderer>().material;
    }

    public virtual void Attack()
    {

    }

    public virtual void Death()
    {
        if (!IsDead()) return;
        GenerateParticle();
        ScoreManager.AddScore(score);
        Destroy(gameObject);
    }

    void GenerateParticle()
    {
        if (deadParticle == null) return;
        Instantiate(deadParticle, transform.position, Quaternion.identity);
    }

    bool IsDead() { return hp <= 0; }

    private void OnTriggerEnter(Collider other)
    {
        HitPlayerBullet(other);
    }

    public virtual void HitPlayerBullet(Collider other)
    {
        if (!DamageObj(other)) return;
        var bullet = other.GetComponent<Bullet>();
        hp -= bullet.GetDamage();
        StartCoroutine(Blink());
    }

    public void Damage(int damage)
    {
        hp -= damage;
    }

    public bool DamageObj(Collider other)
    {
        return other.tag == "PlayerBullet" || other.tag == "ReflectBullet";
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    IEnumerator Blink()
    {
        mat.color = damageColor;
        yield return new WaitForSeconds(blinkDelay);
        mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField, Header("最大HP")]
    protected int maxHP = 10;
    protected int hp;

    protected AudioSource[] ses;
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
        hp = maxHP;
        ses = GetComponents<AudioSource>();
        mat = GetComponentInChildren<MeshRenderer>().material;
    }

    public virtual void Attack()
    {

    }

    public virtual void Death()
    {
        if (!IsDead()) return;
        ses[0].Play();
        Destroy(gameObject);
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
}

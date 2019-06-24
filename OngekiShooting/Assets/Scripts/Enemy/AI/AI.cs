using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField, Header("最大HP")]
    protected int maxHP = 10;
    protected int hp;

    EnemyManager enemyManager;
    MoveType moveType;

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
        enemyManager = FindObjectOfType<EnemyManager>();
        hp = maxHP;
    }

    public void SetMove(MoveType moveType)
    {
        this.moveType = moveType;
    }

    public virtual void Attack()
    {

    }

    public virtual void Death()
    {
        if (!IsDead()) return;
        Destroy(gameObject);
    }

    bool IsDead() { return hp <= 0; }

    private void OnTriggerEnter(Collider other)
    {
        HitPlayerBullet(other);
    }

    private void HitPlayerBullet(Collider other)
    {
        if (other.tag != "PlayerBullet") return;
        var bullet = other.GetComponent<Bullet>();
        hp -= bullet.GetDamage();
    }

    public void Damage(int damage)
    {
        hp -= damage;
    }
}

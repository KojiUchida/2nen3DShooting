using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp;//ヒットポイント
    public float invincibleTime = 3;//無敵時間
    private float countTime;//時間カウント

    private bool isDamage;

    void Start()
    {
        countTime = 0;
        isDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        countTime += Time.deltaTime;
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
        countTime = 0;
    }

    public void Damage(int damage)
    {
        hp -= damage;
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        isDamage = true;
        
        //点滅の計算修正せよ
        float level = Mathf.Abs(Mathf.Sin(Time.time * 10f));
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);//ここを修正せよ（内田！！）
        yield return new WaitForSeconds(invincibleTime);
        isDamage = false;
    }
}


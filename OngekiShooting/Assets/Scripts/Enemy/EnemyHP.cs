using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp;//ヒットポイント
    public float invincibleTime = 3;//無敵時間
    private float countTime;//時間カウント
    void Start()
    {
        countTime =0;
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
            Debug.Log("死んだ～");
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "PlayerBullet")
        {
            if (invincibleTime <= countTime)
            {
                hp -= 1;
                Debug.Log("いたい");
                countTime = 0;
            }
            else
            {
                Debug.Log("むてきだお");
            }
        }
    }
}

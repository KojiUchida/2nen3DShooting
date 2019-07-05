using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigid;
    public float speed = 5;
    [SerializeField, Header("ステージ横幅")]
    float stageWidth = 5.0f;
    [SerializeField, Header("全ゲージ回復までの時間")]
    public float gaugeTime = 60.0f;

    [HideInInspector]
    public float gauge;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.isPause) return;
        gauge += Time.deltaTime;
        gauge = Mathf.Clamp(gauge, 0, gaugeTime);
        rigid.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, 0);
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -stageWidth, stageWidth);
        transform.position = pos;
    }
}

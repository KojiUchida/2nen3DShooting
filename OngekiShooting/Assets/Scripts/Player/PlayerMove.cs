using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody;
    public float speed = 5;
    public GameObject barrier;
    public static bool barrierFlag;//バリア状態かどうか
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        barrierFlag = true;
        barrier = transform.Find("Barrier").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, 0);
        Barrier();
        if(Input.GetKeyDown(KeyCode.X))
        {
            if (barrierFlag) barrierFlag = false;
            else barrierFlag = true;
        }
    }

    void Barrier()
    {
        switch(barrierFlag)
        {
            case true:
                barrier.SetActive(true);break;
            case false:
                barrier.SetActive(false);break;
        }

        if(barrierFlag)
        {
            barrier.gameObject.SetActive(true);
        }
        else barrier.gameObject.SetActive(false);
    }
}

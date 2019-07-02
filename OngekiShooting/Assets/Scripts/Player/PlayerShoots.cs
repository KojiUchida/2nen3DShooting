using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoots : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public Transform muzzle;
    public float shootsTime = 0.2f;
    private float time;
    private AudioSource[] ses;

    void Start()
    {
        time = 0;
        ses = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoots();
        time += Time.deltaTime;
    }

    void Shoots()
    {
        if (PlayerMove.barrierFlag) return;
        if (!Input.GetKey(KeyCode.Z)) return;
        if (time < shootsTime) return;

        Instantiate(bullet, muzzle.position, Quaternion.identity);
        ses[1].Play();
        time = 0;
    }
}

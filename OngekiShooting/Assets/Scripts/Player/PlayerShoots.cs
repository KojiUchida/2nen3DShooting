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
    SoundManager soundManager;

    void Start()
    {
        time = 0;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoots();
        time += Time.deltaTime;
    }

    void Shoots()
    {
        if (!Input.GetKey(KeyCode.Z)) return;
        if (time < shootsTime) return;
        soundManager.PlaySe(2);
        Instantiate(bullet, muzzle.position, Quaternion.identity);
        time = 0;
    }
}

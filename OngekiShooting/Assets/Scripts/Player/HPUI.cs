using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    PlayerHP playerHP;
    Slider[] sliders;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = GetComponentInParent<PlayerHP>();
        sliders = GetComponentsInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        sliders[0].value = (float)playerHP.hp / playerHP.maxHP;
        sliders[1].value = PlayerMove.barrierGauge / PlayerMove.maxBarrierGauge;
    }
}

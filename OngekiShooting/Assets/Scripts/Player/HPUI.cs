using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    [SerializeField, Header("初期の色")]
    Color initColor;
    [SerializeField,Header("1段階目の色")]
    Color firstColor;
    [SerializeField, Header("2段階目の色")]
    Color secondColor;
    [SerializeField, Header("3段階目の色")]
    Color thirdColor;

    PlayerHP playerHP;
    PlayerMove playerMove;
    Slider[] sliders;
    Slider hpSlider;
    Slider gaugeSlider;
    Image gaugeFill;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = GetComponentInParent<PlayerHP>();
        playerMove = GetComponentInParent<PlayerMove>();
        sliders = GetComponentsInChildren<Slider>();
        hpSlider = sliders[0];
        gaugeSlider = sliders[1];
        gaugeFill = gaugeSlider.fillRect.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = (float)playerHP.hp / playerHP.maxHP;
        gaugeSlider.value = playerMove.gauge / playerMove.gaugeTime;
        ChangeGaugeColor();
    }

    void ChangeGaugeColor()
    {
        gaugeFill.color = initColor;
        if (gaugeSlider.value >= 1.0f / 3.0f) gaugeFill.color = firstColor;
        if (gaugeSlider.value >= 2.0f / 3.0f) gaugeFill.color = secondColor;
        if (gaugeSlider.value == 1.0f) gaugeFill.color = thirdColor;
    }
}

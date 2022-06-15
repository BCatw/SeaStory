using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteBlink : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite[] sprites;
    [SerializeField] int blinkFPS;
    [SerializeField] int timer;
    [SerializeField] bool isBlinkOnEnable;
    [SerializeField] bool isBlinking;
    int nowSprite = 0;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (isBlinking) DoBlink();
    }

    public void StartBlink()
    {
        nowSprite = 0;
        isBlinking = true;
        timer = blinkFPS;
    }

    public void DoBlink()
    {
        if (timer > 0)
        {
            timer--;
        }
        else if (timer == 0)
        {
            nowSprite = nowSprite >= (sprites.Length - 1) ? 0 : nowSprite + 1;
            image.sprite = sprites[nowSprite];
            timer = blinkFPS;
        }
    }
    
    private void OnEnable()
    {
        if (isBlinkOnEnable) StartBlink();
    }

    private void OnDisable()
    {
        isBlinking = false;
    }

}

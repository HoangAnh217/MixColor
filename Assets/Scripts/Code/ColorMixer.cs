using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorMixer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetSprite;
    private int limitNum = 0;
    private Vector3 originScale;

    private void Start()
    {
        originScale = targetSprite.transform.localScale;
    }

    public void MixColor(Color newColor)
    {   
        if (newColor== Color.white)
        {
            targetSprite.color = Color.white;
            limitNum = 0;
            return;
        }
        if (targetSprite.color == Color.white)
        {
            targetSprite.color = newColor;
            limitNum=1;
            return;
        }
        if (limitNum>=2)
            return;
        Color currentColor = targetSprite.color;
        Color mixedColor = new Color(
            (currentColor.r + newColor.r) / 2,
            (currentColor.g + newColor.g) / 2,
            (currentColor.b + newColor.b) / 2
        );
        targetSprite.color = mixedColor;
        limitNum++;
    }
    public void OnPointerEnter()
    {

        targetSprite.transform.DOScale(originScale * 1.1f, 0.24f);
    }
    public void OnPointerExit()
    {
        targetSprite.transform.DOScale(originScale , 0.24f);
    }
}

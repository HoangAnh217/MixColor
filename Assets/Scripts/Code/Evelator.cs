using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Evelator : MonoBehaviour
{   

    [SerializeField] private List<Transform> elevators;
    private List<Color> colors;
    private SpriteRenderer colorThis;

    [SerializeField] private Sprite[] spr = new Sprite[2];
    [Header("Elevetor Pra")] 
    [SerializeField] private List<float> maxHeight = new List<float>();
    private bool isPlayerOnPlatform = false; 
    private void Start()
    {
        isPlayerOnPlatform = false;
        colorThis = transform.Find("Model").GetComponentInChildren<SpriteRenderer>();
        colors = new List<Color>();
        for (int i = 0; i < elevators.Count; i++)
        {
            if (elevators[i].Find("Model").GetComponentInChildren<SpriteRenderer>().color==null)
                return;
            Color col = elevators[i].Find("Model").GetComponentInChildren<SpriteRenderer>().color;
            colors.Add(col);
        }
    }
    private void Update()
    {
       
        if (isPlayerOnPlatform)
        {
            for (int i = 0; i < elevators.Count; i++)
            {
                if (AreColorsSimilar(colors[i], colorThis.transform, 0.05f))
                {
                    if (maxHeight[i] > 0)
                    {
                        elevators[i].Translate(Vector2.up * 2.5f * Time.deltaTime);
                        maxHeight[i] -= 1f * Time.deltaTime;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        colorThis.sprite = spr[1];
        isPlayerOnPlatform = true;
    }
   /* private void OnTriggerStay2D(Collider2D collision)
    {
        colorThis.sprite = spr[1];

        Debug.Log("asdasd");

        for (int i = 0; i < elevators.Count; i++)
        {
            if (AreColorsSimilar(colors[i],colorThis.transform,0.05f))
            {
                if (maxHeight[i]>0)
                {
                    elevators[i].Translate(Vector2.up * 2.5f * Time.deltaTime);
                    maxHeight[i] -= 1f * Time.deltaTime;
                }
            }
        }
    }*/
    private void OnTriggerExit2D(Collider2D collision)
    {
        colorThis.sprite = spr[0]; 
        isPlayerOnPlatform = false;
    }
    private bool AreColorsSimilar(Color color1, Transform color2, float tolerance)
    {
        Color colorB = color2.GetComponent<SpriteRenderer>().color;
        return AreColorSimi(color1, colorB, tolerance);
    }

    private bool AreColorSimi( Color colorA, Color colorB,float tolerance)
    {
        return Mathf.Abs(colorA.r - colorB.r) < tolerance &&
                       Mathf.Abs(colorA.g - colorB.g) < tolerance &&
                       Mathf.Abs(colorA.b - colorB.b) < tolerance &&
                       Mathf.Abs(colorA.a - colorB.a) < tolerance;
    }
}

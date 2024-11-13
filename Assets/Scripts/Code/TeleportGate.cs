using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    [SerializeField] private Transform gateB;
    private Player player;

    [SerializeField] private float tolerance = 0.1f;
    private void Start()
    {
        player = Player.Instance;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (AreColorsSimilar(transform, gateB, tolerance))
        {
            if (collision.gameObject.name == "Box")
            {
                if (collision.gameObject.GetComponent<Box>().isCarrying)
                {
                    return;
                }
            }
            collision.transform.position = gateB.position;
            AudioManager.instance.PlayTeleportSound();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (AreColorsSimilar(transform, gateB, tolerance))
        {
            if (collision.gameObject.name == "Box")
            {
                if (collision.gameObject.GetComponent<Box>().isCarrying)
                {
                    return;
                }
            }
            collision.transform.position = gateB.position;
        }
    }
    private bool AreColorsSimilar(Transform color1, Transform color2, float tolerance)
    {
        Color colorGateA = color1.Find("Model").GetComponentInChildren<SpriteRenderer>().color;
        Color colorGateB = gateB.Find("Model").GetComponentInChildren<SpriteRenderer>().color;
        return Mathf.Abs(colorGateA.r - colorGateB.r) < tolerance &&
               Mathf.Abs(colorGateA.g - colorGateB.g) < tolerance &&
               Mathf.Abs(colorGateA.b - colorGateB.b) < tolerance &&
               Mathf.Abs(colorGateA.a - colorGateB.a) < tolerance;
    }
}

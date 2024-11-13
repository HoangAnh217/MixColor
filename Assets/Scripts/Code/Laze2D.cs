using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Color = UnityEngine.Color;
using Random = UnityEngine.Random;

public class Laze2D : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ParticleSystem remnants;
    public LayerMask layerMask;
    public Transform firePoint;
    public float length;
    private Vector2 endPoint;
    public int colorCount = 10; // Số lượng màu ngẫu nhiên muốn tạo
    [SerializeField] private List<Color> hexColorList = new List<Color>();
    [SerializeField] private Material material;

    [SerializeField] private Transform startP, endP;
    public float duration = 5f;
    private ColorMixer playercolorMixer;
    public int colorThis;
    private void Start()
    {
        int ran = Random.Range(0, colorCount);
        colorThis = ran;
        playercolorMixer = Player.Instance.GetComponentInChildren<ColorMixer>();
        SetLineColor(ran);
    }
    private void Update()
    {
        endPoint = firePoint.position - firePoint.up* length;
        CheckCollider();
        UpdateLineRenderer();
        
    }
    private void CheckCollider()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position,-firePoint.up, length);
        if (hit.collider == null)
        {
            remnants.gameObject.SetActive(false);
        }
        else
        {
            endPoint = hit.point;
            remnants.transform.position = endPoint;
            remnants.gameObject.SetActive(true);
            if (hit.collider.gameObject.name == "Player")
            {
                // Destroy(hit.collider.gameObject);
                if (AreColorsSimilar(Player.Instance.transform,0.1f,colorThis))
                {
                    return;
                }
                Player.Instance.OnDead();
            }
        }
    }

    private void UpdateLineRenderer()
    {
        var startPointLocal = lineRenderer.transform.InverseTransformPoint(firePoint.position);
        var endPointLocal = lineRenderer.transform.InverseTransformPoint(endPoint);

        lineRenderer.SetPosition(0, startPointLocal);
        lineRenderer.SetPosition(1, endPointLocal);
    }
    public void SetLineColor(int ra)
    {
        /*lineRenderer.startColor = hexColorList[ra];
         * 
        lineRenderer.endColor = hexColorList[ra];*/
        colorThis = ra;
        Gradient gradient = new Gradient();

        // Định nghĩa màu và alpha cho gradient
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0] = new GradientColorKey(hexColorList[ra], 0f); // Màu đỏ tại vị trí 0
        colorKeys[1] = new GradientColorKey(hexColorList[ra], 1f); // Màu đỏ tại vị trí 1

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1f, 1f); // Alpha = 1 tại vị trí 0
        alphaKeys[1] = new GradientAlphaKey(1f, 1f); // Alpha = 1 tại vị trí 1

        // Gán màu và alpha cho gradient
        gradient.colorKeys = colorKeys;
        gradient.alphaKeys = alphaKeys;

        // Gán gradient cho LineRenderer
        lineRenderer.colorGradient = gradient;

    }
    private bool AreColorsSimilar(Transform color1, float tolerance,int i)
    {
        Color colorGateA = color1.Find("Model").GetComponentInChildren<SpriteRenderer>().color;
        Color colorGateB = hexColorList[i];

        Debug.Log(colorGateB +" "+ colorGateA);

        return Mathf.Abs(colorGateA.r - colorGateB.r) < tolerance &&
               Mathf.Abs(colorGateA.g - colorGateB.g) < tolerance &&
               Mathf.Abs(colorGateA.b - colorGateB.b) < tolerance ;
    }
}

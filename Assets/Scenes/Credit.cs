using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    public RectTransform creditText; // G�n RectTransform c?a Text v�o ��y
    public float scrollDuration = 10f; // Th?i gian cu?n t? d�?i l�n tr�n
    public float startY = -500f; // V? tr� Y ban �?u c?a Text
    public float endY = 500f; // V? tr� Y k?t th�c c?a Text

    private void Start()
    {
        StartCreditRoll();
    }

    private void StartCreditRoll()
    {
        // �?t v? tr� ban �?u cho Text
        creditText.anchoredPosition = new Vector2(creditText.anchoredPosition.x, startY);

        // T?o hi?u ?ng cu?n t? d�?i l�n tr�n
        creditText.DOAnchorPosY(endY, scrollDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            // T�y ch?n: T?t credit khi ho�n th�nh
            //gameObject.SetActive(false);
        });
    }
}

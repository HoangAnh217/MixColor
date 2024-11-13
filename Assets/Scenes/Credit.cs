using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    public RectTransform creditText; // Gán RectTransform c?a Text vào ðây
    public float scrollDuration = 10f; // Th?i gian cu?n t? dý?i lên trên
    public float startY = -500f; // V? trí Y ban ð?u c?a Text
    public float endY = 500f; // V? trí Y k?t thúc c?a Text

    private void Start()
    {
        StartCreditRoll();
    }

    private void StartCreditRoll()
    {
        // Ð?t v? trí ban ð?u cho Text
        creditText.anchoredPosition = new Vector2(creditText.anchoredPosition.x, startY);

        // T?o hi?u ?ng cu?n t? dý?i lên trên
        creditText.DOAnchorPosY(endY, scrollDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            // Tùy ch?n: T?t credit khi hoàn thành
            //gameObject.SetActive(false);
        });
    }
}

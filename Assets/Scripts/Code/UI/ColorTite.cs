using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ColorTite : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    private Image colorImage;
    private Color draggedColor;
    private GameObject draggedObject;

    private Transform border;
    void Start()
    {   
        colorImage = GetComponent<Image>();
        draggedColor = colorImage.transform.GetChild(0).GetComponent<Image>().color;
        border = transform.Find("Image");
        border.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        draggedObject = Instantiate(gameObject.transform.GetChild(0).gameObject, transform.parent);
        draggedObject.GetComponent<Image>().enabled = true;
        draggedObject.GetComponent<Image>().raycastTarget = false;
        draggedObject.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedObject != null)
        {
            draggedObject.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        if (hit.collider != null)
        {
            ColorMixer target = hit.collider.GetComponentInChildren<ColorMixer>();
            if (target != null)
            {
                target.MixColor(draggedColor); 
            }
        }
        Destroy(draggedObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        border.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        border.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoticeBoard : MonoBehaviour
{
    [SerializeField] private Transform noticeBoard;
    [SerializeField] private Transform pressF_TextUI;
    private string fullText;
    private TextMeshPro textMeshPro;
    private void Start()
    {
        textMeshPro = pressF_TextUI.gameObject.GetComponent<TextMeshPro>();
        fullText = textMeshPro.text;
        textMeshPro.text = "";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        pressF_TextUI.gameObject.SetActive(true);
        StartCoroutine(TypeText());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        pressF_TextUI.gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            noticeBoard.GetComponentInParent<GuideUI>().Open();
        }
    }
    private IEnumerator TypeText()
    {
        textMeshPro.text = ""; 
        foreach (char letter in fullText.ToCharArray())
        {
            textMeshPro.text += letter; 
            yield return new WaitForSeconds(0.08f); 
        }
    }
}

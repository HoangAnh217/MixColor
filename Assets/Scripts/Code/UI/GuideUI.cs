using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GuideUI : MonoBehaviour
{
    private bool isOpened;
    [SerializeField] private Transform guide;
    [SerializeField] private VideoPlayer videoPlayer;
    private void Start()
    {
        isOpened = false;
        guide.gameObject.SetActive(false);
    }
    public void Open()
    {
        if (!isOpened)
        {
            isOpened =true;
            guide.gameObject.SetActive(true);
            videoPlayer.Play();
        }
    }
    public void Close()
    {
        if (isOpened)
        {
            isOpened = false;
            videoPlayer.Stop();
            guide.gameObject.SetActive(false);
        }
    }
}

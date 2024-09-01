using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContentManager : MonoBehaviour, IPointerDownHandler
{
    public ScrollSnapRect scrollSnapRect;
    public GameObject image;

    private float currentTime;

    public GameObject prevBtn;
    public GameObject nextBtn;
    public GameObject backBtn;

    public static ContentManager instance = null;

    public ImageController clickedObject;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        foreach (var obj in GameManager.instance.sprites)
        {
            Instantiate(obj, scrollSnapRect._container);
        }

        scrollSnapRect.Init();

        currentTime = Time.time;
    }

    void Update()
    {
        if (Time.time - currentTime > GameManager.instance.settingTime)
        {
            GameManager.instance.fade.GetComponent<Image>().raycastTarget = true;
            GameManager.instance.fade.StartFadeOut(0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            currentTime = Time.time;
        }
    }

    public void UISetEnable()
    {
        if(scrollSnapRect.GetCurrentPage() != 0)
            prevBtn.SetActive(true);
        if(scrollSnapRect.GetCurrentPage() != scrollSnapRect.GetPageCountPage() -1)
            nextBtn.SetActive(true);
        backBtn.SetActive(true);
    }

    public void UISetUnEnable()
    {
        prevBtn.SetActive(false);
        nextBtn.SetActive(false);
        backBtn.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ImageController _clicked = eventData.pointerCurrentRaycast.gameObject.GetComponent<ImageController>();
        if (_clicked != null)
        {
            clickedObject = _clicked;
            clickedObject.Setting();
        }
        else if (clickedObject != null)
        {
            clickedObject.UnSetting();
            clickedObject = null;
        }
    }
}

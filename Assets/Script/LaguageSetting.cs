using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaguageSetting : MonoBehaviour
{
    public GameObject korTxt;
    public GameObject engTxt;

    public Image[] images;

    public bool isEnable;

    void Start()
    {
        isEnable = true;
        if (korTxt == null && transform.Find("KorTxt") != null)
        {
            korTxt = transform.Find("KorTxt").gameObject;
        }
        if (engTxt == null && transform.Find("EngTxt") != null)
        {
            engTxt = transform.Find("EngTxt").gameObject;
        }

        if (korTxt != null && engTxt != null)
        {
            if (GameManager.instance.laguageType == LaguageType.Korea)
            {
                korTxt.SetActive(true);
                engTxt.SetActive(false);
            }
            else
            {
                korTxt.SetActive(false);
                engTxt.SetActive(true);
            }
        }

        images = GetImages();
    }

    public Image[] GetImages()
    {
        Image[] _images = GetComponentsInChildren<Image>();
        if (_images.Length <= 1)
            return null;

        List<Image> _imageList = new List<Image>(_images);
        _imageList.RemoveAt(0);
        _images = _imageList.ToArray();

        return _images;
    }

    void FixedUpdate()
    {
        if (korTxt != null && engTxt != null && isEnable)
        {
            if (GameManager.instance.laguageType == LaguageType.Korea)
            {
                korTxt.SetActive(true);
                engTxt.SetActive(false);
            }
            else
            {
                korTxt.SetActive(false);
                engTxt.SetActive(true);
            }
        }
    }

    public void UnEnableSetting()
    {
        isEnable = false;

        if (korTxt != null && engTxt != null)
        {
            korTxt.SetActive(false);
            engTxt.SetActive(false);
        }

        if(images.Length > 0)
        {
            Image clieckImg = ContentManager.instance.clickedObject.GetComponent<Image>();
            foreach (var img in images)
            {
                if (img != clieckImg)
                {
                    img.gameObject.SetActive(false);
                }
            }
        }
    }

    public void EnableSetting()
    {
        isEnable = true;

        if (images.Length > 0)
        {
            foreach (var img in images)
            {
                img.gameObject.SetActive(true);
            }
        }
    }
}

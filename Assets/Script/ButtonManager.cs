using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public string folderPath = "";
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        if(button != null && folderPath.Trim().Length > 0)
        {
            button.onClick.AddListener(Button_Image);
        }
    }

    public void Button_Image()
    {
        GameManager.instance.setImagePaths(folderPath);
        GameManager.instance.fade.GetComponent<Image>().raycastTarget = true;
        GameManager.instance.fade.StartFadeOut(1);
    }

    public void Button_3DMap()
    {
        GameManager.instance.fade.GetComponent<Image>().raycastTarget = true;
        GameManager.instance.fade.StartFadeOut(2);
    }

    public void MainScene()
    {
        GameManager.instance.fade.GetComponent<Image>().raycastTarget = true;
        GameManager.instance.fade.StartFadeOut(0);
    }

    public void OnKorEng()
    {
        if (GameManager.instance.laguageType == LaguageType.English)
            GameManager.instance.laguageType = LaguageType.Korea;
        else
            GameManager.instance.laguageType = LaguageType.English;
    }
}

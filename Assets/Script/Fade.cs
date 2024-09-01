using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private Image image;
    private float fadeTime = 2f;
    public AnimationCurve fadeCurve;

    private bool isFade = false;

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.instance.fade = this;
        image = GetComponent<Image>();
    }

    void Start()
    {
        StartFadeIn();
    }


    IEnumerator FadeInOut(float start, float end, int idx = 0)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if(start > end)
        {
            yield return new WaitForSeconds(0.5f);
        }

        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color;

            yield return null;
        }

        if(start < end)
        {
            SceneController.instance.SetScene(idx);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeInOut(1, 0));
    }

    public void StartFadeOut(int idx)
    {
        if (!isFade)
        {
            isFade = true;
            StartCoroutine(FadeInOut(0, 1, idx));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public InfinityScroll infinityScroll;

    public float fadeTime = 1f;

    float accumTime = 0f;

    private Coroutine fadeCor;

    private float currentTime;

    bool isFade = false;

    bool mainWind = false;


    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public void StartFadeIn()
    {
        isFade = true;
        if (fadeCor != null)
        {
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        isFade = true;
        if (fadeCor != null)
        {
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine(FadeOut());

    }

    IEnumerator FadeIn()
    {
        canvasGroup.blocksRaycasts = true;
        accumTime = 0f;
        while(accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = 1f;
        isFade = false;
        mainWind = false;

        infinityScroll.SetTransform();
    }

    IEnumerator FadeOut()
    {
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        isFade = false;
    }

    void Update()
    {
        if (mainWind)
        {
            if (Time.time - currentTime > GameManager.instance.settingTime)
            {
                if (!isFade)
                    StartFadeIn();
            }

            if (Input.GetMouseButtonDown(0))
            {
                currentTime = Time.time;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isFade)
                    StartFadeOut();
                currentTime = Time.time;
                mainWind = true;
            }
        }
    }
}

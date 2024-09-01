using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageController : MonoBehaviour, IDragHandler, IScrollHandler
{
    private RectTransform rectTransform;

    private Vector3 minitialPosition;
    private Vector3 minitialScale;

    private RectTransform minitialTransform;

    private float zoomSpeed = 0.1f;
    private float maxZoom = 10.0f;
    private bool isSelect = false;

    private float initialDistance;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        minitialPosition = transform.localPosition;
        minitialScale = transform.localScale;

        minitialTransform = rectTransform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isSelect)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (isSelect)
        {
            var delta = Vector3.one * (eventData.scrollDelta.y * zoomSpeed);
            var desiredScale = transform.localScale + delta;

            desiredScale = ClampDesiredScale(desiredScale);

            transform.localScale = desiredScale;
        }
    }

    private Vector3 ClampDesiredScale(Vector3 desiredScale)
    {
        desiredScale = Vector3.Max(minitialScale, desiredScale);
        desiredScale = Vector3.Min(minitialScale * maxZoom, desiredScale);

        return desiredScale;
    }

    private void Update()
    {
        if(isSelect)
        {
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                
                if(touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled
                    || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                {
                    return;
                }

                if(touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    minitialPosition = transform.localScale;
                }
                else
                {
                    var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    if (Mathf.Approximately(initialDistance, 0))
                        return;

                    var factor = currentDistance / initialDistance;

                    transform.localScale = minitialScale * factor;
                }
            }
        }
    }

    public void Setting()
    {
        isSelect = true;
        ContentManager.instance.UISetUnEnable();
        if(GetComponentInParent<LaguageSetting>() != null)
            GetComponentInParent<LaguageSetting>().UnEnableSetting();
    }

    public void UnSetting()
    {
        isSelect = false;
        transform.localScale = minitialScale;
        transform.localPosition = minitialPosition;

        rectTransform.anchoredPosition = minitialTransform.anchoredPosition;

        ContentManager.instance.UISetEnable();
        if (GetComponentInParent<LaguageSetting>() != null)
            GetComponentInParent<LaguageSetting>().EnableSetting();
    }
}

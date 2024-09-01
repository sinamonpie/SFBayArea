using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class InfinityScroll : MonoBehaviour
{
    private ScrollSnapRect scrollSnap;
    public ScrollRect scrollRect;

    public RectTransform scrollViewTransform;
    public RectTransform maskTransform;
    public RectTransform viewPortTransform;
    public RectTransform contentPanelTransform;
    public HorizontalLayoutGroup HLG;

    public RectTransform[] ItemList;
    public List<RectTransform> contentList;

    Vector2 OldVeclocity;
    bool isUpdated;

    private RectTransform preSetContentTransform;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        scrollViewTransform = GetComponent<RectTransform>();

        if (maskTransform == null)
        {
            var mask = GetComponentInChildren<Mask>(true);
            if (mask)
            {
                maskTransform = mask.rectTransform;
            }
            if (maskTransform == null)
            {
                var mask2D = GetComponentInChildren<RectMask2D>(true);
                if (mask2D)
                {
                    maskTransform = mask2D.rectTransform;
                }
            }
        }

        isUpdated = false;
        OldVeclocity = Vector2.zero;

        int ItemsToAdd = Mathf.CeilToInt(viewPortTransform.rect.width / (ItemList[0].rect.width + HLG.spacing));

        for (int i = 0; i < ItemsToAdd; i++)
        {
            RectTransform RT = Instantiate(ItemList[i % ItemList.Length], contentPanelTransform);
            RT.SetAsLastSibling();
        }

        for (int i = 0; i < ItemsToAdd; i++)
        {
            int num = ItemList.Length - i - 1;

            while (num < 0)
            {
                num += ItemList.Length;
            }

            RectTransform RT = Instantiate(ItemList[num], contentPanelTransform);
            RT.SetAsFirstSibling();
        }

        contentPanelTransform.localPosition = new Vector3((0 - (ItemList[0].rect.width + HLG.spacing) * ItemsToAdd) - ItemList[0].rect.width, contentPanelTransform.localPosition.y, contentPanelTransform.localPosition.z);

        preSetContentTransform = contentPanelTransform;
    }

    public void SetTransform()
    {
        contentPanelTransform.localPosition = preSetContentTransform.localPosition;
    }

    private void Update()
    {
        if(isUpdated)
        {
            isUpdated = false;
            scrollRect.velocity = OldVeclocity;
        }

        if(contentPanelTransform.localPosition.x > 0)
        {
            Canvas.ForceUpdateCanvases();
            OldVeclocity = scrollRect.velocity;
            contentPanelTransform.localPosition -= new Vector3(ItemList.Length * (ItemList[0].rect.width + HLG.spacing), 0, 0);
            isUpdated = true;
        }

        if (contentPanelTransform.localPosition.x < 0 - (ItemList.Length * (ItemList[0].rect.width + HLG.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            OldVeclocity = scrollRect.velocity;
            contentPanelTransform.localPosition += new Vector3(ItemList.Length * (ItemList[0].rect.width + HLG.spacing), 0, 0);
            isUpdated = true;
        }
    }
}

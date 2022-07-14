using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Goods : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler, IPointerClickHandler
{
    public Canvas canvasTips;

    public GameObject GoodsTipsPrefab;

    private RectTransform rectTransform;

    private Vector3 offset;

    private bool isDraging;

    private GameObject goodsTipsObj;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out worldPos);
        rectTransform.position = worldPos + offset;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 worldPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out worldPos);
        offset = rectTransform.position - worldPos;
        isDraging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDraging)
        {
            //弹提示
            ShowGoodsTips();
        }
    }

    private void ShowGoodsTips()
    {
        if(goodsTipsObj == null)
        {
            goodsTipsObj = Instantiate(GoodsTipsPrefab, canvasTips.transform);
        }
        goodsTipsObj.SetActive(true);
        goodsTipsObj.transform.position = transform.position;
        //计算边界并修正位置（简易实现原理）
        RectTransform tipsRect = goodsTipsObj.GetComponent<RectTransform>();
        RectTransform canvasRecrt = canvasTips.GetComponent<RectTransform>();
        float diff = 0;
        //左
        diff = -(canvasRecrt.rect.width / 2) - (tipsRect.anchoredPosition.x - tipsRect.rect.width / 2);
        if (diff > 0)
        {
            tipsRect.anchoredPosition += Vector2.right * diff;
        }
        //右
        diff = -(canvasRecrt.rect.width / 2) + (tipsRect.anchoredPosition.x + tipsRect.rect.width / 2);
        if (diff > 0)
        {
            tipsRect.anchoredPosition -= Vector2.right * diff;
        }
        //下
        diff = -(canvasRecrt.rect.height / 2) - (tipsRect.anchoredPosition.y - tipsRect.rect.height / 2);
        if (diff > 0)
        {
            tipsRect.anchoredPosition += Vector2.up * diff;
        }
        //上
        diff = -(canvasRecrt.rect.height / 2) + (tipsRect.anchoredPosition.y + tipsRect.rect.height / 2);
        if (diff > 0)
        {
            tipsRect.anchoredPosition -= Vector2.up * diff;
        }
    }

    private void HideGoodsTips()
    {
        if (goodsTipsObj == null) return;
        goodsTipsObj.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            eventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(eventData, raycastResults);
            bool hide = true;
            foreach (var result in raycastResults)
            {
                if (result.gameObject.CompareTag("Tips"))
                {
                    hide = false;
                    break;
                }
            }
            if (hide)
            {
                HideGoodsTips();
            }
        }
    }
}

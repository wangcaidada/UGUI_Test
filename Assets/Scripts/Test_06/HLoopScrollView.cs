using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HLoopScrollView : MonoBehaviour
{
    //Ԫ��Ԥ����
    public GameObject ItemPrefab;
    //Ԫ�طֲ�
    public ItemPos ItemVerticalPos;
    //������
    public float Spacing;
    //ģ����������
    public List<string> ItemData;

    [Header("ģ�������������������0��ģ��")]
    public int TestItemNum;

    //���������
    private Dictionary<int,ListItem> _itemCache;
    //���������
    private int _itemCacheNum;

    private ScrollRect _scrollRect;
    private RectTransform _scrollTran;

    private float _itemWidth;
    private float _itemHeight;

    private float _viewPortWidth;
    private float _viewPortHeight;

    //ͷ����
    private int _startIndex;
    //β����
    private int _endIndex;

    //��һ�����������
    private int _startItemIndex;

    private void Awake()
    {
        //����ģ������
        if (TestItemNum > 0)
        {
            ItemData.Clear();
            for (int i = 0; i < TestItemNum; i++)
            {
                ItemData.Add(i.ToString());
            }
        }

        _scrollRect = GetComponent<ScrollRect>();
        _scrollTran = GetComponent<RectTransform>();

        _itemCache = new Dictionary<int, ListItem>();
        _itemWidth = ItemPrefab.GetComponent<RectTransform>().sizeDelta.x;
        _itemHeight = ItemPrefab.GetComponent<RectTransform>().sizeDelta.y;
        _viewPortWidth = _scrollRect.viewport.sizeDelta.x;
        _viewPortHeight = _scrollRect.viewport.sizeDelta.y;

        //������С��������
        _itemCacheNum = (int)Math.Ceiling(_viewPortWidth / (_itemWidth + Spacing)) + 2;
        _itemCacheNum = Math.Min(ItemData.Count, _itemCacheNum);

        _scrollRect.onValueChanged.AddListener(OnMove);
    }

    private void Start()
    {
        InitItem();
    }

    private void InitItem()
    {
        if(_itemCacheNum > 0)
        {
            //��ʼ��Content��С
            _scrollRect.content.sizeDelta = new Vector2((_itemWidth + Spacing) * ItemData.Count, _viewPortHeight);
            //��ʼ����������
            for (int i = 0; i < _itemCacheNum; i++)
            {
                GameObject itemObj = Instantiate(ItemPrefab, _scrollRect.content);
                ListItem listItem = itemObj.GetComponent<ListItem>();
                listItem.name = listItem.GetType() + "_" + i;
                RectTransform itemRectTran = itemObj.GetComponent<RectTransform>();
                itemRectTran.pivot = new Vector2(0, 1);
                //�����ʼλ��
                itemRectTran.anchoredPosition = GetItemPosFromIndex(i);
                _itemCache.Add(i,listItem);
                //��ʼ����
                listItem.SetData(ItemData[i]);
            }
            _startIndex = 0;
            _endIndex = _itemCacheNum - 1;
        }
    }

    public Vector2 GetItemPosFromIndex(int index)
    {
        Vector2 pos = Vector2.zero;
        switch (ItemVerticalPos)
        {
            case ItemPos.Top:
                pos = new Vector2(index * (_itemWidth + Spacing), 0);
                break;
            case ItemPos.Mid:
                float surLen = _scrollRect.content.sizeDelta.y - _itemHeight;
                if (surLen > 0)
                {
                    pos = new Vector2(index * (_itemWidth + Spacing), -(_scrollRect.content.sizeDelta.y-_itemHeight)/2);
                }
                else
                {
                    pos = new Vector2(index * (_itemWidth + Spacing), 0);
                }
                break;
            case ItemPos.Bottom:
                pos = new Vector2(index * (_itemWidth + Spacing), -_scrollRect.content.sizeDelta.y + _itemHeight);
                break;
            default:
                pos = new Vector2(index * (_itemWidth + Spacing), 0);
                break;
        }
        return pos;
    }

    //����
    private void OnMove(Vector2 arg0)
    {
        //���󻬶�
        if (_scrollRect.velocity.x < 0)
        {
            //���ͷ���峬������Ӽ���ľ���,����β������������򽻻�λ������������
            Vector2 startPos = GetItemPosFromIndex(_startIndex);
            if(_scrollRect.content.anchoredPosition.x < -(startPos.x + _itemWidth + Spacing))
            {
                //�����Ҫ���β��
                if (_endIndex + 1 < ItemData.Count)
                {
                    ListItem listItem = _itemCache[_startIndex];
                    _itemCache.Remove(_startIndex);
                    _endIndex++;
                    listItem.SetData(ItemData[_endIndex]);
                    listItem.rectTransform.anchoredPosition = GetItemPosFromIndex(_endIndex);
                    listItem.name = listItem.GetType() + "_" + _endIndex;
                    _itemCache.Add(_endIndex, listItem);
                    _startIndex++;
                }
            }
        }
        //���һ���
        if(_scrollRect.velocity.x > 0)
        {
            //���β���峬������Ӽ���ľ���,����β������������򽻻�λ������������
            Vector2 endPos = GetItemPosFromIndex(_endIndex);
            if (_scrollRect.content.anchoredPosition.x-_viewPortWidth > -endPos.x-Spacing)
            {
                //�����Ҫ���β��
                if (_startIndex - 1 > -1)
                {
                    ListItem listItem = _itemCache[_endIndex];
                    _itemCache.Remove(_endIndex);
                    _startIndex--;
                    listItem.SetData(ItemData[_startIndex]);
                    listItem.rectTransform.anchoredPosition = GetItemPosFromIndex(_startIndex);
                    listItem.name = listItem.GetType() + "_" + _startIndex;
                    _itemCache.Add(_startIndex, listItem);
                    _endIndex--;
                }
            }
        }
    }

    public enum ItemPos
    {
        Top,
        Mid,
        Bottom
    }
}

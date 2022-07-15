using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    public RectTransform rectTransform;
    private Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetData(string data)
    {
        text.text = data;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text;

public class Typer : MonoBehaviour
{
    public TMP_Text text;
    public TMP_InputField inputFieldText;
    public TMP_InputField inputFieldTime;
    public Button button;

    private Coroutine coroutine;

    private void Start()
    {
        button.onClick.AddListener(OnClickBtn);
    }

    private void OnClickBtn()
    {
        string textStr = inputFieldText.text;
        string timeStr = inputFieldTime.text;
        float time = 0;
        if(timeStr != null && !timeStr.Equals(string.Empty))
        {
            time = Convert.ToSingle(timeStr);
        }
        if(textStr != null && !text.Equals(string.Empty))
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            StartCoroutine(StartShowText(textStr, time));
        }
    }



    private IEnumerator StartShowText(string str,float time)
    {
        text.text = string.Empty;
        char[] chars = str.ToCharArray();
        StringBuilder sb = new StringBuilder();
        int len = chars.Length;
        time = Math.Max(0, time);
        if(time <= 0)
        {
            text.text = str;
            yield break;
        }
        float stepTime = time / len;
        for (int i = 0; i < len; i++)
        {
            sb.Append(chars[i]);
            text.text = sb.ToString();
            yield return new WaitForSeconds(stepTime);
        }
    }
}

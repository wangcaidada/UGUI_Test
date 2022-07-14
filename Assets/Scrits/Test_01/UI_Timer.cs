using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    public TMP_Text TxtTime;
    public TMP_InputField InputAllTIme;
    public Button BtnStart;
    private Coroutine coroutine;

    private void Start()
    {
        BtnStart.onClick.AddListener(OnClickBtnStart);
    }

    private void OnClickBtnStart()
    {
        string inputStr = InputAllTIme.text;
        if (inputStr.Equals(string.Empty))
        {
            inputStr = "0";
        }
        int allTime = Convert.ToInt32(inputStr);
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(StartTimer(allTime));
    }

    private IEnumerator StartTimer(int allTime)
    {
        string timeStr = ConvertTimeToHMS(allTime);
        TxtTime.text = timeStr;
        while (allTime > 0)
        {
            timeStr = ConvertTimeToHMS(allTime);
            TxtTime.text = timeStr;
            yield return new WaitForSeconds(1);
            allTime--;
        }
    }

    private string ConvertTimeToHMS(int allTime)
    {
        allTime = Math.Max(0, allTime);
        int hour = allTime / (60 * 60);
        int min = (allTime / 60) % 60;
        int sec = allTime % 60;
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hour, min, sec);
    }
}

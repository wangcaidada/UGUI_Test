using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CanBackInput : MonoBehaviour
{
    public TMP_InputField inputField;

    private Stack<string> cacheStr;

    private bool isBacking;

    private string lastStr;

    private void Awake()
    {
        cacheStr = new Stack<string>();
    }

    private void Start()
    {
        inputField.onValueChanged.AddListener(OnInputChange);
    }

    private void OnInputChange(string arg0)
    {
        if (!isBacking)
        {
            cacheStr.Push(arg0);
        }
    }

    private void Update()
    {
        if (inputField.isFocused && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Z))
        {
            string oldStr = string.Empty;
            while (cacheStr.Count > 0)
            {
                oldStr = cacheStr.Pop();
                if (oldStr != inputField.text)
                {
                    break;
                }
            }
            isBacking = true;
            inputField.text = oldStr;
            inputField.MoveTextEnd(false);
            isBacking = false;
        }
    }
}

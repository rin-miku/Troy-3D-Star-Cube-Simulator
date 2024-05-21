using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Action<string> showOnUIAction;
    public static void PrintLog(string log, bool showOnUI = false)
    {
        if (showOnUI)
        {
            showOnUIAction.Invoke(log);
        }
        Debug.Log(log);
    }
}

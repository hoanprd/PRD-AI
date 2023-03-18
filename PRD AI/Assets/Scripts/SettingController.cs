using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingController : MonoBehaviour
{
    public TextMeshProUGUI output;

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            GlobalController.Lang = 0;
        }
        if (val == 1)
        {
            GlobalController.Lang = 1;
        }
        if (val == 2)
        {
            GlobalController.Lang = 2;
        }

        PlayerPrefs.SetInt("SVL", GlobalController.Lang);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject SettingPanel;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SLV"))
        {
            GlobalController.Lang = 0;
        }
        else
        {
            GlobalController.Lang = PlayerPrefs.GetInt("SLV");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChatButton()
    {
        SceneManager.LoadScene("MainPC");
    }

    public void OpenSettingButton()
    {
        SettingPanel.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void AboutUsButton()
    {
        Application.OpenURL("https://www.facebook.com/hoan.nguyenduy.7967");
    }    

    public void CloseSettingButton()
    {
        SettingPanel.SetActive(false);
    }
}

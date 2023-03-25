using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Start()
    {
        //Debug.Log(duongdan);
    }

    public void ChatMessButton()
    {
        SceneManager.LoadScene("ChatRoomTextPC");
    }

    public void ChatImageButton()
    {
        SceneManager.LoadScene("ChatRoomImagePC");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void AboutUsButton()
    {
        Application.OpenURL("https://www.facebook.com/hoan.nguyenduy.7967");
    }
}

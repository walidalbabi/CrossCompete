using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAppManager : Singleton<UIAppManager>
{
    [SerializeField]
    private GameObject Canvas;
    [SerializeField]
    private GameObject LoginPanel;
    [SerializeField]
    private GameObject RegisterPanel;
    [SerializeField]
    private GameObject ResetPanel;
    [SerializeField]
    private GameObject Home;

    public void ShowLogin()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
        ResetPanel.SetActive(false);
    }
    public void ShowRegister()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    public void ShowResetPasswordPassword()
    {
        LoginPanel.SetActive(false);
        ResetPanel.SetActive(true);
    }

    public void ShowHome()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(false);
        Home.SetActive(true);
    }

    public void DisableCanvas()
    {
        if (Canvas.activeInHierarchy)
            Canvas.SetActive(false);
        else
            Canvas.SetActive(true);
    }
}

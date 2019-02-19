using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine.SceneManagement;

public class ControllerMainMenu
{
    ViewMainMenu view;

    public ControllerMainMenu()
    {

        view = GameObject.FindObjectOfType<ViewMainMenu>().GetComponent<ViewMainMenu>();
        // DisplayUI();


    }
    public void ShowAchievements(string display)
    {
        view.ShowAchievements(display);
    }

    public void HideAchievements()
    {
        view.HideAchievements();
    }


    public void DestroyUI()
    {
        view.DestroyUI();
    }
    public void DisplayUI()
    {
        view.DisplayUI();
    }

}
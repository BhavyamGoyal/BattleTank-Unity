using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerStartUI
{
    // For Controlling the UI in the Start screen
    ViewStartUI view;
    public GameObject panel;
    public ControllerStartUI(GameObject startUI, RectTransform parent)
    {
        panel = startUI;
        GameObject obj;
        obj = GameObject.Instantiate(panel);
        obj.gameObject.transform.SetParent(parent);
        view = obj.GetComponent<ViewStartUI>();
        DisplayUI();
    }
    public void DestroyUI()
    {
        view.DestroyUI();
        Debug.Log("InStartController");
       // view=null;
    }
    public void SetMiniMap(RenderTexture texture){
        view.SetMiniMap(texture);
    }
    public void DisplayUI()
    {
        view.DisplayUI();
    }
    public void UpdateScore(int score)
    {
        string scoreView = "Score : " + score;
        view.UpdateScore(scoreView);
    }
    public void UpdateHealth(int health)
    {
        string healthView = "Health : " + health;
        view.UpdateHealth(healthView);
    }
}

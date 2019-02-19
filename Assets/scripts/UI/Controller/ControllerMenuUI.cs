using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMenuUI
{
    ViewMenuUI view;
    public ControllerMenuUI()
    {
        view = GameObject.FindObjectOfType<ViewMenuUI>().GetComponent<ViewMenuUI>();
        DisplayUI();
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

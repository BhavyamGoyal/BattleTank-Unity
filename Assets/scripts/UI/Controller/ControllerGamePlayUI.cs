using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
    public class ControllerGamePlayUI
    {
        ViewGamePlayUI view;
        public ControllerGamePlayUI()
        {
            view = GameObject.FindObjectOfType<ViewGamePlayUI>();
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

}
using UI;
using System.Collections;
using System.Collections.Generic;
using StateMachines;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ControllerReplayUI
    {
        ViewReplayUI view;
        public ControllerReplayUI()
        {
            view = GameObject.FindObjectOfType<ViewReplayUI>().GetComponent<ViewReplayUI>();
            
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
using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputComponent 
{
    IBasePlayerController controller;

    public InputComponent(IBasePlayerController controller) {
        this.controller = controller;
        //InputManager.Instance.RegisterInputComponent(this,controller.GetControls());
    }
    public void InputUpdate(InputData inputData)
    {
        controller.Update();
        controller.Move(inputData.forward,inputData.direction);
        if (inputData.shoot)
        {
            controller.Shoot();
        }
        if (inputData.boost)
        {
            controller.StartBoost();
        }
        else
        {
            controller.StopBoost();
        }
       //Debug.Log("Updating InputComponent");
    }
    
}

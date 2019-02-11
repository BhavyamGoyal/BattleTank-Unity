﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : Singleton<InputManager>
{
    public Dictionary<Controls, Queue<InputData>> playerInput = new Dictionary<Controls, Queue<InputData>>();
    public Dictionary<Controls, List<InputComponent>> inputComponents = new Dictionary<Controls, List<InputComponent>>();


    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        GameApplication.Instance.OnPlayerSpawn += AddPlayerListener;
    }
    private void OnLevelWasLoaded(int level)
    {

    }
    public void EnqueueData(InputData inputdata, Controls controls)
    {
        if (Instance.playerInput.ContainsKey(controls))
        {
            Instance.playerInput[controls].Enqueue(inputdata);
        }
    }
    public virtual void Update()
    {
        foreach (Controls controls in InputManager.Instance.inputComponents.Keys)
        {
            foreach (InputComponent inputComponent in inputComponents[controls])
            {
                if (Instance.playerInput[controls].Count != 0)
                {
                    //Debug.Log(InputManager.Instance.playerInput[Controls.IJKL].forward);
                    inputComponent.InputUpdate(Instance.playerInput[controls].Dequeue());
                }
            }
        }

    }
    public void RegisterInputComponent(InputComponent inputComponent, Controls controls)
    {
        if (!InputManager.Instance.playerInput.ContainsKey(controls))
        {
            InputManager.Instance.playerInput.Add(controls, new Queue<InputData>());
            List<InputComponent> newList = new List<InputComponent>();
            newList.Add(inputComponent);
            InputManager.Instance.inputComponents.Add(controls, newList);
        }
        else
        {
            InputManager.Instance.inputComponents[controls].Add(inputComponent);
        }
        //Debug.Log("One InputComponent added, Total="+inputComponents.Count +"  "+controls);
    }
    public void AddPlayerListener(ControllerPlayer controller)
    {

        RegisterInputComponent(controller.GetInputComponent(), controller.GetControls());
        controller.OnPlayerDeath += RemoveInputComponent;

    }
    public void RemoveInputComponent(ControllerPlayer controller, InputComponent inputComponent, Controls controls)
    {
        InputManager.Instance.inputComponents[controls].Remove(inputComponent);

        // Debug.Log("One InputComponent Removed WASD, Total=" + inputComponents[controls].Count);
        if (InputManager.Instance.inputComponents.ContainsKey(controls) && InputManager.Instance.inputComponents[controls].Count == 0)
        {
            InputManager.Instance.inputComponents.Remove(controls);
            InputManager.Instance.playerInput.Remove(controls);
            // ServiceUI.Instance.GameOver();
        }


    }
}

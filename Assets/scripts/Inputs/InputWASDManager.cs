using UnityEngine;
using UnityEditor;
using Replay_Service;
using System.Collections.Generic;
using Interfaces.ServiecesInterface;

public class InputWASDManager : MonoBehaviour
{
    InputData inputData;
    Controls controls = Controls.WASD;
    private void Start()
    {
        ServiceLocator.Instance.get<IInputManager>().AddPlayerInputData(controls);

    }
    public void Update()
    {

        inputData = new InputData();
        inputData.forward = Input.GetAxis("Horizontal1");
        inputData.direction = Input.GetAxis("Vertical1");
        //Debug.Log("getting Input using IJKL");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            inputData.boost = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            inputData.boost = false;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            inputData.shoot = true;
        }
        else
        {
            inputData.shoot = false;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.DeleteAll();
        }
        
        inputData.frame = ServiceLocator.Instance.get<IFrameService>().GetFrame();
        ServiceLocator.Instance.get<IInputManager>().EnqueueData(inputData, controls);
        ServiceReplay.Instance.RecordInput(inputData, controls);
    }
}
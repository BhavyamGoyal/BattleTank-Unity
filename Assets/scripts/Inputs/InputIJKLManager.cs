using System.Collections;
using System.Collections.Generic;
using Interfaces.ServiecesInterface;
using Replay_Service;
using UnityEngine;

public class InputIJKLManager : MonoBehaviour
{
    Controls controls = Controls.IJKL;
    InputData inputData;
    private void Start()
    {
        ServiceLocator.Instance.get<IInputManager>().AddPlayerInputData(controls);

    }
    public void Update()
    {

        inputData = new InputData();
        inputData.forward = Input.GetAxis("Horizontal");
        inputData.direction = Input.GetAxis("Vertical");
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
        inputData.frame =ServiceLocator.Instance.get<IFrameService>().GetFrame();
        ServiceLocator.Instance.get<IInputManager>().EnqueueData(inputData, controls);
        ServiceReplay.Instance.RecordInput(inputData, controls);
    }
}

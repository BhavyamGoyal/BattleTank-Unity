using System.Collections;
using System.Collections.Generic;
using Interfaces.ServiecesInterface;
using Player;
using Replay_Service;
using StateMachines;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : IInputManager
{
    //public int initialFame, pauseFrame, resumeFrame;
    public Dictionary<Controls, Queue<InputData>> playerInput = new Dictionary<Controls, Queue<InputData>>();
    public GameObject InputControllersPrefab, referenceInput;
    public Dictionary<Controls, List<InputComponent>> inputComponents = new Dictionary<Controls, List<InputComponent>>();
    bool userControls = true, replay = false, pause = false;
    // Start is called before the first frame update
    // Update is called once per frame
    public InputManager()
    {
        InputControllersPrefab = Resources.Load<GameObject>("Inputs");
        SceneManager.sceneLoaded += OnLevelLoaded;
        ServiceLocator.Instance.get<IStateManager>().OnStateChanged += GameStateChanged;
        GameApplication.Instance.OnPlayerSpawn += AddPlayerListener;
    }
    public void AddPlayerInputData(Controls controls)
    {
        playerInput.Add(controls, new Queue<InputData>());

    }
    public void SetQueue(Dictionary<Controls, Queue<InputData>> Pinput)
    {
        playerInput = Pinput;
    }
    public void EnqueueData(InputData inputdata, Controls controls)
    {
        if (playerInput.ContainsKey(controls))
        {
            //ServiceReplay.Instance.RecordInput(inputdata, controls);
            playerInput[controls].Enqueue(inputdata);
        }
    }
    public void Update()
    {
        if (!pause)
        {//Debug.Log(replay);
            foreach (Controls controls in inputComponents.Keys)
            {

                foreach (InputComponent inputComponent in inputComponents[controls])
                {
                    if (playerInput[controls].Count > 0)
                    {
                        //
                        // if (replay)
                        // {
                        //         Debug.Log("current: " + (Time.frameCount - initialFame) + "input: " + Instance.playerInput[controls].Peek().frame);
                        // }
                        if ((ServiceLocator.Instance.get<IFrameService>().GetFrame()) == (playerInput[controls].Peek().frame))
                        {

                            //Debug.Log(InputManager.Instance.playerInput[Controls.IJKL].forward);
                            //Debug.Log("try moving");
                            inputComponent.InputUpdate(playerInput[controls].Dequeue());
                        }
                        else
                        {
                            while (playerInput[controls].Count > 0 && (ServiceLocator.Instance.get<IFrameService>().GetFrame()) >= playerInput[controls].Peek().frame)
                            {
                                //Debug.Log("removing");
                                inputComponent.InputUpdate(playerInput[controls].Dequeue());
                            }
                        }

                    }
                }
            }
        }
    }
    ~InputManager()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }
    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            playerInput = new Dictionary<Controls, Queue<InputData>>();

            if (userControls)
            {
                referenceInput = GameObject.Instantiate(InputControllersPrefab);
            }
        }
    }
    public void GameStateChanged(GameState currentState)
    {
        if (currentState is GamePlayState)
        {
            pause = false;
            // Debug.Log("gameplaystate");
            userControls = true;
            replay = false;
            if (referenceInput != null)
            {
                referenceInput.SetActive(true);
            }
        }
        else
        {
            if (referenceInput != null)
            {
                referenceInput.SetActive(false);
            }
            userControls = false;
            if (currentState is GameReplayState)
            {
                pause = false;
                if (ServiceLocator.Instance.get<IStateManager>().GetPreviousState() is GamePauseState)
                {
                    pause = true;
                }
                replay = true;
                // initialFame = Time.frameCount;
            }
            if (currentState is GamePauseState)
            {
                pause = true;
                if (ServiceLocator.Instance.get<IStateManager>().GetPreviousState() is GameReplayState)
                {
                    replay = false;
                }
            }
            else
            {
                pause = false;
            }
        }
    }
    public void RegisterInputComponent(InputComponent inputComponent, Controls controls)
    {
        if (!inputComponents.ContainsKey(controls))
        {
            List<InputComponent> newList = new List<InputComponent>();
            newList.Add(inputComponent);
            inputComponents.Add(controls, newList);
            //Debug.Log("One InputComponent added, Total=" + inputComponents.Count + "  " + controls);
        }
        else
        {
            inputComponents[controls].Add(inputComponent);
        }
    }
    public void AddPlayerListener(ControllerPlayer controller)
    {
        RegisterInputComponent(controller.GetInputComponent(), controller.GetControls());
        controller.OnPlayerDeath += RemoveInputComponent;
    }
    public void ResetInput()
    {
        playerInput = new Dictionary<Controls, Queue<InputData>>();
        inputComponents = new Dictionary<Controls, List<InputComponent>>();
    }
    public void RemoveInputComponent(ControllerPlayer controller, InputComponent inputComponent, Controls controls)
    {
        //Debug.Log("One InputComponent Removed" + controls);
        inputComponents[controls].Remove(inputComponent);
        if (inputComponents.ContainsKey(controls) && inputComponents[controls].Count == 0)
        {
            inputComponents.Remove(controls);
            //InputManager.Instance.playerInput.Remove(controls);
            // ServiceUI.Instance.GameOver();
        }
    }
}

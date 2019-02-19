using System.Collections;
using System.Collections.Generic;
using Interfaces.ServiecesInterface;
using StateMachines;
using UnityEngine;

public class FrameService : IFrameService
{
    // Start is called before the first frame update
    int frame = -0;
    bool isPause = false;
    public FrameService()
    {
        ServiceLocator.Instance.get<IStateManager>().OnStateChanged += OnStateChanged;
    }
    public void OnStateChanged(GameState state)
    {
        if (state is GamePauseState)
        {
            isPause = true;
        }
        else
        {
            if ((state is GameReplayState) && !(ServiceLocator.Instance.get<IStateManager>().GetPreviousState() is GamePauseState))
            {
                frame = 0;
                Debug.Log("HEy there mate I reset the frame");
            }
            isPause = false;
        }
    }
    private void OnDisable()
    {
        ServiceLocator.Instance.get<IStateManager>().OnStateChanged -= OnStateChanged;
    }
    // Update is called once per frame
    public void Update()
    {
        if (!isPause)
        {
            ++frame;
        }
    }
    public int GetFrame()
    {
        return frame;
    }
}

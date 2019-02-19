using System.Collections;
using System.Collections.Generic;
using Interfaces.ServiecesInterface;
using StateMachines;
using UnityEngine;
using UnityEngine.UI;

public class ViewMenuUI : MonoBehaviour
{
    public Button play;

    public void Start()
    {
        //play = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<Button>();   
    }
    public void updateUI()
    {
       
        if ((ServiceLocator.Instance.get<IStateManager>().GetPreviousState() is GameReplayState))
        {
             ServiceLocator.Instance.get<IStateManager>().ChangeState(new GameReplayState(), false);
        }else{
            ServiceLocator.Instance.get<IStateManager>().ChangeState(new GamePlayState(), false);
        }
        //Debug.Log(StateManager.Instance.previousState+" "+StateManager.Instance.currentState);
       
    }
    public void DestroyUI()
    {
        play.gameObject.SetActive(false);
    }
    public void DisplayUI()
    {
        play.gameObject.SetActive(true);
    }
}

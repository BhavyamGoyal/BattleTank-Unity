using System;
using System.Collections;
using System.Collections.Generic;
namespace StateMachines
{
    public class PlayerStateMachine
    {
        ControllerPlayer player;
        public Dictionary<PlayerStates, bool> currentStates = new Dictionary<PlayerStates, bool>();
        Dictionary<PlayerStates, List<PlayerStates>> StateGraph = new Dictionary<PlayerStates, List<PlayerStates>>();
        public PlayerStateMachine(ControllerPlayer player)
        {
            this.player = player;
            currentStates.Add(PlayerStates.Move, false);
            currentStates.Add(PlayerStates.Regen, false);
            currentStates.Add(PlayerStates.Shoot, false);
            currentStates.Add(PlayerStates.paused, false);
            StateGraph.Add(PlayerStates.Move, new List<PlayerStates> { PlayerStates.paused, PlayerStates.Regen });
            StateGraph.Add(PlayerStates.Shoot, new List<PlayerStates> { PlayerStates.paused });
            StateGraph.Add(PlayerStates.Regen, new List<PlayerStates> { PlayerStates.paused, PlayerStates.Move });
            StateGraph.Add(PlayerStates.paused, new List<PlayerStates> { PlayerStates.Regen, PlayerStates.Move, PlayerStates.Shoot });

        }
        public void EnterPauseState()
        {
            
            currentStates[PlayerStates.Move] = false;
            currentStates[PlayerStates.Regen] = false;
            currentStates[PlayerStates.Shoot] = false;
            currentStates[PlayerStates.paused] = true;
        }
        public void Resume() { 
            currentStates[PlayerStates.paused] = false;
        }
        public void EnterRegenState()
        {
            currentStates[PlayerStates.Regen] = true;
        }
        public void EnterMoveState()
        {
             currentStates[PlayerStates.Move] = true;
             currentStates[PlayerStates.Regen] = false;
        }
         public void EnterIdleState()
        {
             currentStates[PlayerStates.Move] = false;
            // currentStates[PlayerStates.Regen] = false;
        }
        public bool isPaused(){
            return currentStates[PlayerStates.paused];
        }
        public bool isMoving(){
            return currentStates[PlayerStates.Move];
        }

    }

}
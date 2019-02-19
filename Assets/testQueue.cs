using System.Collections;
using System.Collections;
using System.Collections.Generic;
using Replay_Service;
using StateMachines;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tank_MVC.Assets
{
    public class testQueue:MonoBehaviour
    {int x=0;
        Queue q=new Queue();
        private void Update() {
            if(Input.GetKey(KeyCode.P)){
               Debug.Log("Enqueueing X");
               q.Enqueue(x++);
            }
            if(Input.GetKey(KeyCode.X)){
                Debug.Log(q.Dequeue());
            }
        }
    }
}
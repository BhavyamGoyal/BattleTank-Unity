using System.Collections;
using System.Collections.Generic;
using Interfaces.ServiecesInterface;
using StateMachines;
using UnityEngine;
using UnityEngine.UI;

namespace LoadingBar
{


    public class LoadingBar : MonoBehaviour
    {
        // Start is called before the first frame update
        Slider slider;
        bool changeScene=true;
        void Start()
        {
            
           // StateManager.Instance.OnStateChanged+=SetUI;
            slider = gameObject.GetComponent<Slider>();
        }
        float loaded = 0;
        // Update is called once per frame
        void Update()
        {
            if (loaded < .6)
            {
                changeScene=true;
                loaded = loaded + 0.02f;
                slider.value = loaded;

            }
            else if (loaded < .85)
            {
                loaded += 0.02f;
                slider.value = loaded;
            }
            else if (loaded < 1)
            {
                loaded += 0.02f;
            }
            else
            {
                slider.value = loaded;
                if(changeScene){
                ServiceLocator.Instance.get<IStateManager>().ChangeState();
                changeScene=false;
                }
            }
        }
        private void SetUI(){
            slider.gameObject.SetActive(true);
        }
        
        private void RemoveUI(){
            slider.gameObject.SetActive(false);
        }
    }
}
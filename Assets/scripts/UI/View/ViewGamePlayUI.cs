using System.Collections;
using System.Collections.Generic;
using Achievements;
using Interfaces.ServiecesInterface;
using StateMachines;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{


    public class ViewGamePlayUI : MonoBehaviour
    {
        // Start is called before the first frame update
        //[SerializeField] public Text achievement;
        [SerializeField] public GameObject MainPanal;
        [SerializeField] public Button pause;
        void Start()
        {


        }

        public void GamePaused()
        {
            ServiceLocator.Instance.get<IStateManager>().ChangeState(new GamePauseState(), false);
        }
        public void DestroyUI()
        {
            pause.gameObject.SetActive(false);
            MainPanal.gameObject.SetActive(false);
            // achievement.gameObject.SetActive(false);
            pause.onClick.RemoveListener(GamePaused);
        }
        public void DisplayUI()
        {
            pause.gameObject.SetActive(true);
            MainPanal.gameObject.SetActive(true);
            pause.onClick.AddListener(GamePaused);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

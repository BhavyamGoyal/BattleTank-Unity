using System.Collections;
using System.Collections.Generic;
using Achievements;
using ScriptableObjects;
using StateMachines;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Interfaces.ServiecesInterface;

public class ViewMainMenu : MonoBehaviour
{
    Button play;
    Text highScore;
    
    [SerializeField] TextMeshProUGUI achievements;
       public void Start()
    {
        //scrollContent = GameObject.FindGameObjectWithTag("ScrollContent").GetComponent<RectTransform>();
        play = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<Button>();
        highScore = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        DisplayUI();
        highScore.text = "HighScore : " + PlayerPrefs.GetInt("HighScore", 0);
    }

public void ShowAchievements(string display){
    achievements.text=display;
    achievements.gameObject.SetActive(true);
}

public void HideAchievements(){
    achievements.gameObject.SetActive(false);
}
    
    public void DestroyUI()
    {
        play.gameObject.SetActive(false);

    }
    public void DisplayUI()
    {
        play.gameObject.SetActive(true);
    }
    public void LoadGameScene()
    {
        ServiceLocator.Instance.get<IStateManager>().ChangeState(new GamePlayState(), true);
    }
}

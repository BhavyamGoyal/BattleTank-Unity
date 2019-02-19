using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using StateMachines;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UI;
using Achievements;
using Player;
using Interfaces.ServiecesInterface;

public class ServiceUI : Singleton<ServiceUI>
{    //For Changing Between Different UI Screens and any actions to be performed by the UI like Button Press
    Queue<string> achievementsQueue = new Queue<string>();
    GameObject startUI;
    public ControllerGamePlayUI GameUI;
    ControllerMainMenu lobbyUI;
    public RectTransform startUIParent;
    public Dictionary<PlayerNumber, ControllerStartUI> playerUI = new Dictionary<PlayerNumber, ControllerStartUI>();

    void Start()
    {
        startUI = Resources.Load<GameObject>("Panel");
       
        GameApplication.Instance.OnPlayerSpawn += AddPlayerListener;
        //Set The Menu UI ie: play Button
        
    }
    public void SetListeners(){
         ServiceLocator.Instance.get<IServiceAchievements>().OnAchievementUnlocked += AchievementUnlocked;
        ServiceLocator.Instance.get<IStateManager>().OnStateChanged += GameStateChanged;
    }
    public void DestroyStartUI()
    {
        GameUI.DestroyUI();
    }
    public void AchievementUnlocked(string display, int achievementId)
    {
        achievementsQueue.Enqueue(display);
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            StartCoroutine(DisplayAchievement());
        }
    }
    public void DisplayStartUI()
    {
        if (GameUI == null)
        {
            GameUI = new ControllerGamePlayUI();
        }
        else
        {
            GameUI.DisplayUI();
        }
    }
    public void SetStartUI(PlayerNumber playerNumber)
    {
        lobbyUI = null;
        //ControllerStartUI start=new ControllerStartUI(startUI,startUIParent);
        if (!playerUI.ContainsKey(playerNumber))
        {
            playerUI.Add(playerNumber, new ControllerStartUI(startUI, startUIParent));
        }
        else
        {
            playerUI[playerNumber].DisplayUI();
        }
    }
    public void SetMiniMap(PlayerNumber playerNumber, RenderTexture texture)
    {
        playerUI[playerNumber].SetMiniMap(texture);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }
    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            playerUI = new Dictionary<PlayerNumber, ControllerStartUI>();
            startUIParent = GameObject.FindObjectOfType<HorizontalLayoutGroup>().gameObject.GetComponent<RectTransform>();
        }
        else if (scene.name == "MainMenu")
        {
            lobbyUI = new ControllerMainMenu();
            if (achievementsQueue.Count > 0)
            {
                StartCoroutine(DisplayAchievement());
            }
        }
        else
        {
            GameUI = null;
        }
    }

    public void GameStateChanged(GameState currentState)
    {
    }
    public void updateUI(PlayerData playerData)
    {
        if (playerUI[playerData.player.GetPlayerNumber()] != null)
        {
            playerUI[playerData.player.GetPlayerNumber()].UpdateHealth(playerData.health);
            playerUI[playerData.player.GetPlayerNumber()].UpdateScore(playerData.score);
            if (playerData.score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", playerData.score);
            }
        }
    }
    public void Replay()
    {
        ServiceLocator.Instance.get<IStateManager>().ChangeState(new GamePlayState(), true);
    }
    public void GameOver()
    {
        ServiceLocator.Instance.get<IStateManager>().ChangeState(new GameOverState(), true);
    }
    public void LoadMenu()
    {
        ServiceLocator.Instance.get<IStateManager>().ChangeState(new LobbyState(), false);
    }
    public void AddPlayerListener(ControllerPlayer player)
    {
        player.OnUIUpdate += updateUI;
        player.OnPlayerDeath += OnPlayerDead;
        SetStartUI(player.GetPlayerNumber());
    }
    public void OnPlayerDead(ControllerPlayer player, InputComponent component, Controls controls)
    {
        playerUI[player.GetPlayerNumber()].DestroyUI();
        // playerUI.Remove(player.GetPlayerNumber());
    }
    IEnumerator DisplayAchievement()
    {

        lobbyUI.ShowAchievements(achievementsQueue.Dequeue());
        yield return new WaitForSeconds(3f);
        lobbyUI.HideAchievements();
        if (achievementsQueue.Count > 0)
        {
            StartCoroutine(DisplayAchievement());
        }
    }
}

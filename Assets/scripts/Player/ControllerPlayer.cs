using Interfaces;
using Interfaces.ServiecesInterface;
using ObjectPooling;
using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : IBasePlayerController
{

    ViewPlayer view;
    float pauseTartedTime = 0;
    ModelPlayer model;

    public PlayerData playerData;
    InputComponent inputComponent;
    PlayerStateMachine stateMachine;
    public event Action<PlayerData> OnUIUpdate;
    public event Action<PlayerData> OnScoreUpdate;
    public event Action<PlayerData> OnBulletShot;
    public event Action<PlayerData> OnEnemyKilled;
    public event Action<ControllerPlayer, InputComponent, Controls> OnPlayerDeath;
    public ControllerPlayer(GameObject player, Vector3 spawnPoint, Controls controls, PlayerNumber playerNumber, bool gameStarted)
    {

        stateMachine = new PlayerStateMachine(this);
        this.view = GameObject.Instantiate(player, spawnPoint, Quaternion.identity, null).GetComponent<ViewPlayer>();
        this.model = new ModelPlayer(controls);
        view.SetController(this);
        model.playerNumber = playerNumber;
        model.gameStarted = gameStarted;
        inputComponent = new InputComponent(this);
        playerData.player = this;
        ServiceLocator.Instance.get<IStateManager>().OnStateChanged += GamePauseState;
        //GameObject.FindObjectOfType<MiniMap>().SetMinimapTarget(view.gameObject);
    }
    public GameObject GetPlayerObject()
    {
        return view.gameObject;
    }
    public void SetCamera(Rect camRect)
    {

        //Debug.Log(camRect);
        view.gameObject.GetComponentInChildren<Camera>().rect = camRect;
    }
    public Vector3 GetPosition()
    {
        return view.gameObject.transform.position;
    }
    public void GamePauseState(GameState currentState)
    {
        if (currentState is GamePauseState)
        {
            stateMachine.EnterPauseState();
            pauseTartedTime = Time.timeSinceLevelLoad;
        }
        else
        {
            stateMachine.Resume();
            pauseTartedTime = Time.timeSinceLevelLoad - pauseTartedTime;
        }
    }
    public bool GetGameStarted()
    {
        return model.gameStarted;
    }
    public PlayerNumber GetPlayerNumber()
    {
        return model.playerNumber;
    }
    public Controls GetControls()
    {
        return model.controls;
    }
    public bool IsFreez()
    {
        return model.freez;
    }
    public void Update()
    {
        if (!stateMachine.isPaused() && !stateMachine.isMoving())
        {
            stateMachine.EnterRegenState();
            if (model.health < 100)
            {
                model.TakeDamage(-0.1f);
                // Debug.Log(model.health);
            }
        }
    }

    public void Move(float horizontal, float vertical)
    {
        //Debug.Log("controller");
        if (horizontal != 0 || vertical != 0)
        {
            stateMachine.EnterMoveState();
            view.MovePlayer(horizontal * model.rotationSpeed * Time.deltaTime, vertical * model.speed * Time.deltaTime * model.boost);
        }
        else
        {
            stateMachine.EnterIdleState();
        }
        Update();

    }
    public void StartBoost()
    {
        model.boost = 2;
    }
    public void StopBoost()
    {
        model.boost = 1;
    }
    public void TankHit(int damage)
    {
        model.TakeDamage(damage);
        playerData.score = model.score;
        playerData.player = this;
        playerData.health = (int)model.health;
        playerData.achievementTypes = AchievementTypes.Score;
        OnUIUpdate.Invoke(playerData);
        if (!model.IsAlive())
        {
            DestroyObject();
        }
        //ServiceUI.Instance.updateUI(model.health, model.score);
    }
    public void EnemyKilled(int score)
    {
        UpdateScore(score);
        playerData.achievementTypes = AchievementTypes.EnemiesKilled;
        OnEnemyKilled?.Invoke(playerData);
    }
    private void UpdateScore(int score)
    {
        model.score = model.score + score;
        playerData.score = model.score;
        playerData.health = (int)model.health;
        playerData.progress = score;
        playerData.achievementTypes = AchievementTypes.Score;
        OnUIUpdate?.Invoke(playerData);
        OnScoreUpdate?.Invoke(playerData);
        //ServiceUI.Instance.updateUI(model.health, model.score);
    }
    public void Shoot()
    {
        if (!stateMachine.isPaused())
        {
            if (model.lastShot + (model.fireInterval + pauseTartedTime) < Time.timeSinceLevelLoad)
            {
                pauseTartedTime = 0;
                model.lastShot = Time.timeSinceLevelLoad;
                playerData.achievementTypes = AchievementTypes.BulletsShot;
                playerData.progress = 1;
                IPoolableBullet controllerBullet = ServiceLocator.Instance.get<IServiceBullet>().MakeBullet(model.bulletType);
                OnBulletShot?.Invoke(playerData);
                controllerBullet.SetShooter(this);
                controllerBullet.Set(view.muzzle.transform);
            }
        }
    }
    public InputComponent GetInputComponent()
    {
        return inputComponent;
    }
    public void DestroyObject()
    {
        OnPlayerDeath.Invoke(this, inputComponent, GetControls());
        view.DestroyPlayer();
        view = null;
    }
}

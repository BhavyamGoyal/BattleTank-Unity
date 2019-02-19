using System;
using System.Collections;
using System.Collections.Generic;
using Achievements;
using Enemy;
using Interfaces;
using Interfaces.ServiecesInterface;
using Rewards;
using Sound;
using StateMachines;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiceLocator : Singleton<ServiceLocator>
{
    // Start is called before the first frame update
    IFrameService frameService;
    List<IServices> services = new List<IServices>();
    InputManager inputManager;
    public override void OnInitialize()
    {
        base.OnInitialize();


    }
    void Start()
    {
        Register<IStateManager>(new StateManager());
        inputManager = new InputManager();
        Register<IInputManager>(inputManager);
        Register<IServiceAchievements>(new ServiceAchievements());
        Register<IServiceRewards>(new ServiceRewards());
        ServiceUI.Instance.SetListeners();
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
            frameService = new FrameService();
            Register<IFrameService>(frameService);
            ServiceEnemy enemyservice = new ServiceEnemy();
            Register<IServiceEnemy>(enemyservice);
            enemyservice.StartSpawning();
            Register<IServiceBullet>(new ServiceBullet());
            Register<IServiceSound>(new ServiceSound());
        }
        else
        {
            frameService = null;
            foreach (IServices service in services)
            {
                if (service is IServiceEnemy || service is IServiceBullet || service is IFrameService ||service is IServiceSound)
                {
                    services.Remove(service);
                }
            }
        }
    }
    private void Register<T>(T service) where T : IServices
    {
        services.Add(service);
    }
    public T get<T>() where T : IServices
    {
        T serve = default(T);
        foreach (IServices service in services)
        {
            if (service is T)
            {
                serve = (T)service;
                break;
            }
        }
        return serve;
    }


    // Update is called once per frame
    void Update()
    {
        if (frameService != null)
        {
            frameService.Update();
        }
        inputManager.Update();
    }
}

using Interfaces;
using Interfaces.ServiecesInterface;
using ObjectPooling;
using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBullet : IPoolableBullet
{
    protected ModelBullet model;
    protected ViewBullet view;
    protected IBasePlayerController shooter;
    public event Action<ControllerBullet> OnBulletDestroy;
    public virtual void Shoot(Transform muzzle) {  }
    public ControllerBullet()
    {
        getViewAndModel();
        view.controller = this;
        //view.time = model.lifeTime;
        ServiceLocator.Instance.get<IStateManager>().OnStateChanged += GameStateChanged;
    }
    public void Reset(){
        view.gameObject.SetActive(false);
    }
    public void Set(Transform muzzle){
        view.gameObject.SetActive(true);
        Shoot(muzzle);
    }
    public void GameStateChanged(GameState currentState)
    {
        if (currentState is GamePauseState)
        {
            view.BulletPaused();
        }
        else
        {
            view.BulletResume();
        }
    }
    public int GetDamage()
    {
        return model.bodyDamage;
    }
    public virtual void getViewAndModel()
    {
        model = new ModelBullet();
        view = new ViewBullet();
    }
    public virtual void DestroyBullet()
    {
        OnBulletDestroy?.Invoke(this);
    }
    public void SetShooter(IBasePlayerController shooter)
    {
        this.shooter = shooter;
    }
    public virtual IBasePlayerController GetShooter()
    {
        return shooter;
    }

}

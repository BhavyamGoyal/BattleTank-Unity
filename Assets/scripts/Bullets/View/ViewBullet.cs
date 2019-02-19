using Interfaces;
using Interfaces.ServiecesInterface;
using StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBullet : MonoBehaviour
{
    public ControllerBullet controller;
    bool paused;
    Rigidbody rb;
    Vector3 velocity;
    public virtual void StartShoot(Transform muzzle, float power, float time,ControllerBullet controler)
    {
        rb = GetComponent<Rigidbody>();
        //StartCoroutine(DeathTimer(time));
        gameObject.transform.position = muzzle.position;
        gameObject.transform.rotation = muzzle.rotation;
        GetComponent<Rigidbody>().velocity = transform.forward * power;
        ServiceLocator.Instance.get<IStateManager>().OnStateChanged += GamePauseState;
        controller=controler;
    }
    public void GamePauseState(GameState state)
    {
        if (state is GamePauseState)
        {
            paused = true;
        }
        else
        {
            paused = false;
        }
    }
    public void BulletPaused()
    {
        if (rb != null)
        {
            velocity = rb.velocity;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
    public void BulletResume()
    {
        if (rb != null)
        {
            rb.velocity = velocity;
        }
    }

    public void DestroyBullet()
    {
        controller.DestroyBullet();
        
    }
    IEnumerator DeathTimer(float time)
    {
        float timer = 0f;
        while (timer < time)
        {
            if (!paused)
            {
                timer += Time.deltaTime;
            }
            yield return null;
        }
        //yield return new WaitForSeconds(time);
        //controller.Destroy();
        DestroyBullet();
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        ITakeDamageView damageView = collision.gameObject.GetComponent<ITakeDamageView>();
        if (damageView != null)
        {
            damageView.TakeDamage(controller.GetDamage(), controller.GetShooter());
            
        }
        DestroyBullet();
    }


}

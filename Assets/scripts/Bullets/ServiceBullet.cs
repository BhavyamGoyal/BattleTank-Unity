using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces.ServiecesInterface;
using ObjectPooling;
using UnityEngine;



public class ServiceBullet : IServiceBullet
{
    ObjectPool<ControllerBullet> bulletPool=new ObjectPool<ControllerBullet>();
    //public static List<ControllerBullet> bulletList = new List<ControllerBullet>();

    public ControllerBullet MakeBullet(BulletTypes bulletType)
    {
        ControllerBullet temp = null;
        switch (bulletType)
        {
            default:
                temp = bulletPool.GetFromPool<ControllerDefaultBullet>();
                break;

            case BulletTypes.fastBullet:
                temp = bulletPool.GetFromPool<ControllerFastBullet>();
                break;

            case BulletTypes.explossiveBullet:
                temp = bulletPool.GetFromPool<ControllerExplossiveBullet>();
                break;
        }
        temp.OnBulletDestroy += RemoveBullet;
        return temp;
    }


    public void RemoveBullet(ControllerBullet bullet)
    {
         bullet.OnBulletDestroy -= RemoveBullet;
        //Debug.Log("Destroying bullet");
        // bulletList.Remove(temp);
        //bulletList.Remove(bullet);
        bulletPool.ReturnToPool(bullet);

    }

}

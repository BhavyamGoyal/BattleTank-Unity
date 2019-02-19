using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFastBullet : ControllerBullet
{


    public override void getViewAndModel()
    {
        GameObject shell = Resources.Load<GameObject>("FastShell");
        view = GameObject.Instantiate<GameObject>(shell).GetComponent<ViewFastBullet>();
        model = new ModelFastBullet();
    }
    public override void Shoot(Transform muzzle)
    {
        view.StartShoot(muzzle, model.power, model.lifeTime, this);
    }
    public override void DestroyBullet()
    {
        base.DestroyBullet();
        GameObject.Destroy(view.gameObject);
        model = null;

    }

}

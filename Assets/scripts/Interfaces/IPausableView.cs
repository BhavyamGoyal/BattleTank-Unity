using UnityEngine;

namespace Tank_MVC.Assets.scripts.Interfaces
{
    public interface IPausableView
    {
         Vector3 GetVelocity();
         void SerVelocity();
    }
}
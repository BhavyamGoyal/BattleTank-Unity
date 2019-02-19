using Interfaces;
using UnityEngine;

namespace ObjectPooling
{
    public interface IPoolableBullet : IPoolable
    {
        void Set(Transform muzzle);
        void SetShooter(IBasePlayerController shooter);
    }
}
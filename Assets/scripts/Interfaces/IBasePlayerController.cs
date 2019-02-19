using UnityEngine;
using UnityEditor;
namespace Interfaces
{
    public interface IBasePlayerController
    {
       
        Controls GetControls();
        bool IsFreez();
        void Move(float horizontal, float vertical);
          void Update();
        void DestroyObject();
        void Shoot();
        void StartBoost();
        void StopBoost();
    }
}
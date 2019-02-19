using System.Collections.Generic;

namespace Interfaces.ServiecesInterface
{
    public interface IInputManager : IServices
    {
        void AddPlayerInputData(Controls controls);
        void EnqueueData(InputData inputdata, Controls controls);
        void SetQueue(Dictionary<Controls, Queue<InputData>> Pinput);
        void ResetInput();
        void Update();
    }
}
using System;
using Rewards;

namespace Interfaces.ServiecesInterface
{
    public interface IServiceRewards : IServices
    {
        void UnlockReward(String name, int Id);
        event Action<ControllerReward> OnSelection;
        void Selected(ControllerReward reward);
    }
}
using System;

namespace Interfaces.ServiecesInterface
{
    public interface IServiceAchievements : IServices
    {
        event Action<string, int> OnAchievementUnlocked;
    }
}
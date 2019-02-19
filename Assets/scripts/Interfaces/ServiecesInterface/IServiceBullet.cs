namespace Interfaces.ServiecesInterface
{
    public interface IServiceBullet : IServices
    {
        void RemoveBullet(ControllerBullet bullet);
        ControllerBullet MakeBullet(BulletTypes bulletType);
    }
}
using UnityEngine;

namespace DemoGame
{
    public class BulletManager
    {
        public Bullet Bullet;

        private Pool<Bullet> BulletPool;

        public void Init()
        {
            Bullet = GameManager.ResourceManager.Load<Bullet>("Bullet");
            BulletPool = new Pool<Bullet>(Bullet, null, 100);
        }

        public void Fire(Vector3 Pos, Vector3 ForWard)
        {
            Bullet.BulletDetail.Generate(Pos, ForWard);
        }

        public void Destroy(Bullet bullet)
        {
            BulletPool.DestObject(bullet);
        }

        public Bullet GetBullet()
        {
            return BulletPool.GetObject();
        }

    }
}
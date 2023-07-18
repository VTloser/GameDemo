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

        public BulletDetail _CurrentBulletDetail = new DemoBulletDetail();

        public void Fire(Vector3 Pos, Vector3 ForWard)
        {
            Bullet._BulletDetail = _CurrentBulletDetail;
            Bullet._BulletDetail.Generate(Pos, ForWard, _CurrentBulletDetail);
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
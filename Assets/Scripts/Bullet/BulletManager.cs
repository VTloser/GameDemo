using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace DemoGame
{
    public class BulletManager
    {
        public Bullet _Bullet;
        private Pool<Bullet> BulletPool;
        private float _LastGenerate;

        public BulletDetail CurrentBulletDetail;

        public void Init() 
        {
            _Bullet = GameManager.ResourceManager.Load<Bullet>("Bullet");
            BulletPool = new Pool<Bullet>(_Bullet, null, 100);

            CurrentBulletDetail = new DemoBulletDetail();
            CurrentBulletDetail.ExtraDetail = new List<BulletDetail> { new SideBulletDetail() };
        }

        public void Fire(Vector3 Pos, Vector3 ForWard)
        {
            _Bullet._BulletDetail = CurrentBulletDetail;
            if (Time.time - _LastGenerate > _Bullet._BulletDetail.bulletAttr.Interval * 2)
            {
                _Bullet._BulletDetail.Generate(Pos, ForWard, CurrentBulletDetail);
                _LastGenerate = Time.time;
            }
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
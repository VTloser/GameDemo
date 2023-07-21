using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace DemoGame
{
    public class BulletManager
    {
        public BulletAgaent _Bullet;
        private Pool<BulletAgaent> BulletPool;

        public BulletDetail CurrentBulletDetail;

        public void Init() 
        {
            _Bullet = GameManager.Instance.ResourceManager.Load<BulletAgaent>("Bullet");
            BulletPool = new Pool<BulletAgaent>(_Bullet, null, 100);

            CurrentBulletDetail = new SuperBulletDetail();
            //CurrentBulletDetail.ExtraDetail = new List<BulletDetail> { new SideBulletDetail() };
        }

        public void Fire(Vector3 Pos, Vector3 ForWard)
        {
            //_Bullet._BulletDetail = CurrentBulletDetail;
            //if (Time.time - _LastGenerate > _Bullet._BulletDetail.bulletAttr.Interval * 2)
            //{
            //    _Bullet._BulletDetail.Generate(Pos, ForWard, CurrentBulletDetail);
            //    _LastGenerate = Time.time;
            //}
        }

        public void Destroy(BulletAgaent bullet)
        {
            BulletPool.DestObject(bullet);
        }

        public BulletAgaent GetBullet()
        {
            return BulletPool.GetObject();
        }
    }
}
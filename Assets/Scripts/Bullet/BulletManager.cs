using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace DemoGame
{
    public class BulletManager
    {

        #region 对象池部分

        public BulletAgaent _Bullet;
        private Pool<BulletAgaent> BulletPool;

        #endregion

        private List<BulletDetail> CurrentBulletDetail;
        public Dictionary<BulletDetail, BulletGenerate> Generates;

        public void AddBulletType(BulletDetail bulletDetail, BulletGenerate bulletGenerate)
        {
            CurrentBulletDetail.Add(bulletDetail);
            Generates.Add(bulletDetail, bulletGenerate);
        }

        public void RemoveBulletType(BulletDetail bulletDetail)
        {
            CurrentBulletDetail.Remove(bulletDetail);
        }

        public void Init() 
        {
            _Bullet = GameManager.Instance.ResourceManager.Load<BulletAgaent>("Bullet");
            BulletPool = new Pool<BulletAgaent>(_Bullet, null, 64);

            CurrentBulletDetail = new List<BulletDetail>();
            Generates = new Dictionary<BulletDetail, BulletGenerate>();
        }

        public void Fire(Vector3 ForWard)
        {
            foreach (BulletDetail b in CurrentBulletDetail)
            {
                Generates[b].Generate(ForWard);
            }
        }

        public void Destroy(BulletAgaent bullet)
        {
            BulletPool.DestObject(bullet);
        }

        public BulletAgaent GetBullet(BulletDetail bulletDetail)
        {
            BulletAgaent bullet = BulletPool.GetObject();
            bullet.Init(bulletDetail);
            return bullet;
        }
    }
}
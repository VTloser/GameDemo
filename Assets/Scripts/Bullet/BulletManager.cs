/*
 * FileName:      BulletManager.cs
 * Author:        摩诘创新
 * Date:          2023/07/19 16:58:25
 * Describe:      子弹管理系统
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections.Generic;
using DemoGame.Bullet;
using DemoGame.Pool;
using UnityEngine;


namespace DemoGame
{
    public class BulletManager
    {

        #region 对象池部分

        /// <summary>  子弹代理  </summary>
        public BulletAgent _Bullet;
        
        /// <summary>  子弹对象池 </summary>
        public Pool<BulletAgent> BulletPool;

        #endregion

        /// <summary> 子弹种类列表 </summary>
        private List<BulletDetail> CurrentBulletDetail;
        
        /// <summary> 子弹生成器列表 </summary>
        public Dictionary<BulletDetail, BulletGenerate> Generates;

        
        /// <summary>
        /// 添加子弹种类
        /// </summary>
        /// <param name="bulletDetail"></param>
        /// <param name="bulletGenerate"></param>
        public void AddBulletType(BulletDetail bulletDetail, BulletGenerate bulletGenerate)
        {
            CurrentBulletDetail.Add(bulletDetail);
            Generates.Add(bulletDetail, bulletGenerate);
        }

        /// <summary>
        /// 移除子弹种类
        /// </summary>
        /// <param name="bulletDetail"></param>
        public void RemoveBulletType(BulletDetail bulletDetail)
        {
            CurrentBulletDetail.Remove(bulletDetail);
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        public void Init() 
        {
            _Bullet = GameManager.Instance.ResourceManager.Load<BulletAgent>("Bullet");
            BulletPool = new Pool<BulletAgent>(_Bullet, null, 128);

            CurrentBulletDetail = new List<BulletDetail>();
            Generates = new Dictionary<BulletDetail, BulletGenerate>();
        }

        /// <summary>
        /// 开火指令
        /// </summary>
        /// <param name="forWard"></param>
        public void Fire(Vector3 forWard)
        {
            foreach (BulletDetail b in CurrentBulletDetail)
            {
                Generates[b].Generate(forWard);
            }
        }

        /// <summary>
        /// 子弹销毁
        /// </summary>
        /// <param name="bullet"></param>
        public void Destroy(BulletAgent bullet)
        {
            BulletPool.DestObject(bullet);
        }

        /// <summary>
        /// 获取某个子弹细节的子弹代理对象 
        /// </summary>
        /// <param name="bulletDetail"></param>
        /// <returns></returns>
        public BulletAgent GetBullet(BulletDetail bulletDetail)
        {
            BulletAgent bullet = BulletPool.GetObject();
            bullet.Init(bulletDetail);
            return bullet;
        }
    }
}
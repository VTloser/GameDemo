/*
 * FileName:      BulletFactory.cs
 * Author:        魏宇辰
 * Date:          2023/07/18 15:41:34
 * Describe:      子弹工厂
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections.Generic;

namespace DemoGame
{

    /// <summary> 
    /// 子弹基础属性
    /// </summary>
    /// 通过享元模式共享基础属性
    public class BulletAttr
    {
        /// <summary>     生命周期   </summary>
        public float LifeTime;
        /// <summary>     伤害       </summary>
        public float Damage;
        /// <summary>     移动速度   </summary>
        public float MoveSpeed;
        /// <summary>    攻击间隔    </summary>
        public float Interval;
        /// <summary>    子弹穿透    </summary>
        public float Penetrate;
        /// <summary>    暴击率      </summary>
        public float CritRate;
        /// <summary>    暴击加成    </summary>
        public float CritDamage;

        public BulletAttr(float lifeTime, float damage, float moveSpeed, float interval, float penetrate, float critRate, float critDamage)
        {
            LifeTime = lifeTime;
            Damage = damage;
            MoveSpeed = moveSpeed;
            Interval = interval;
            Penetrate = penetrate;
            CritRate = critRate;
            CritDamage = critDamage;
        }
    }

    public enum BulletType
    {
        //默认
        None = 0,
        //高伤害
        Height,
        //打的远
        Long,
        //子弹速度快
        Fast,
        //子弹射速快
        FastShoot,
    }


    public class BulletFactory
    {
        private Dictionary<BulletType, BulletAttr> bulletAttrDB = null;
        public BulletFactory()
        {
            bulletAttrDB = new Dictionary<BulletType, BulletAttr>();
            bulletAttrDB.Add(BulletType.None,      new BulletAttr(4, 1, 2, 0.1f,  1, 10, 2));
            bulletAttrDB.Add(BulletType.Height,    new BulletAttr(2, 1, 2, 0.5f,  0, 10, 2));
            bulletAttrDB.Add(BulletType.Long,      new BulletAttr(9, 1, 2, 0.5f,  0, 10, 2));
            bulletAttrDB.Add(BulletType.Fast,      new BulletAttr(2, 1, 9, 0.5f,  0, 10, 2));
            bulletAttrDB.Add(BulletType.FastShoot, new BulletAttr(2, 1, 2, 0.01f, 0, 10, 2));
        }

        public BulletAttr GetBulletAttr(BulletType bulletType)
        { 
            return bulletAttrDB[bulletType];
        }
    }

}
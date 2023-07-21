/*
 * FileName:      BulletFactory.cs
 * Author:        魏宇辰
 * Date:          2023/07/18 15:41:34
 * Describe:      子弹工厂
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections.Generic;
using UnityEngine;

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
        /// <summary>    子弹精灵    </summary>
        public Sprite Sprite;
        /// <summary>    攻击间隔    </summary>
        public float Interval;
        /// <summary>    子弹穿透    </summary>
        public float Penetrate;
        /// <summary>    暴击率      </summary>
        public float CritRate;
        /// <summary>    暴击加成    </summary>
        public float CritDamage;

        public BulletAttr(float lifeTime, float damage, float moveSpeed, Sprite sprite, float interval, float penetrate, float critRate, float critDamage)
        {
            LifeTime = lifeTime;
            Damage = damage;
            MoveSpeed = moveSpeed;
            Sprite = sprite;
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
        //火球
        FireBall,
    }

    public class BulletFactory
    {
        private Dictionary<BulletType, BulletAttr> bulletAttrDB = null;
        public BulletFactory()
        {
            bulletAttrDB = new Dictionary<BulletType, BulletAttr>();
            //后期读取配置表实现
            bulletAttrDB.Add(BulletType.None,     new BulletAttr(4, 1, 2, GameManager.Instance.ResourceManager.Load<Sprite>("Bullet/FireBall"), 0.1f, 1, 10, 2));
            bulletAttrDB.Add(BulletType.FireBall, new BulletAttr(2, 1, 2, GameManager.Instance.ResourceManager.Load<Sprite>("Bullet/FireBall"), 0.5f, 0, 10, 2));
        }

        public BulletAttr GetBulletAttr(BulletType bulletType)
        { 
            return bulletAttrDB[bulletType];
        }
    }

}
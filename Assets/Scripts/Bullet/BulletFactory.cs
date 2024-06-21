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

        /// <summary>    伤害半径    </summary>
        public float Radius;

        /// <summary>    子弹穿透    </summary>
        public float Penetrate;

        /// <summary>    暴击率      </summary>
        public float CritRate;

        /// <summary>    暴击加成    </summary>
        public float CritDamage;

        /// <summary>    移动方式    </summary>
        public BulletMove MoveType;

        public BulletAttr(float lifeTime, float damage, float moveSpeed, Sprite sprite, float interval, float radius,
            float penetrate, float critRate, float critDamage, BulletMove moveType)
        {
            LifeTime = lifeTime;
            Damage = damage;
            MoveSpeed = moveSpeed;
            Sprite = sprite;
            Interval = interval;
            Radius = radius;
            Penetrate = penetrate;
            CritRate = critRate;
            CritDamage = critDamage;
            MoveType = moveType;
        }
    }

    /// <summary>
    /// 子弹种类
    /// </summary>
    public enum BulletType
    {
        //性能检测用
        None = 0,

        //火球
        FireBall,
    }

    /// <summary>
    /// 子弹属性工厂
    /// </summary>
    public class BulletFactory
    {
        public Dictionary<BulletType, BulletAttr> bulletAttrDB = null;

        private BulletAttr NoneAttr = new BulletAttr(4, 1, 10,
            GameManager.Instance.ResourceManager.Load<Sprite>("Bullet/FireBall"), 0.01f,
            0.4f, 0, 0, 0.5f, new DirMove());

        private BulletAttr FireBallAttr = new BulletAttr(10, 1, 10,
            GameManager.Instance.ResourceManager.Load<Sprite>("Bullet/FireBall"), 0.5f,
            0.4f, 0, 0, 0.5f, new RandomMove());


        public BulletFactory()
        {
            bulletAttrDB = new Dictionary<BulletType, BulletAttr>();
            //TODO 后期读取配置表实现

            bulletAttrDB.Add(BulletType.None, NoneAttr);
            bulletAttrDB.Add(BulletType.FireBall, FireBallAttr);
        }
        
        /// <summary>
        /// 获取子弹属性
        /// </summary>
        /// <param name="bulletType"></param>
        /// <returns></returns>
        public BulletAttr GetBulletAttr(BulletType bulletType)
        {
            return bulletAttrDB[bulletType];
        }
    }
}
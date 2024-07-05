/*
 * FileName:      BulletFactory.cs
 * Author:        魏宇辰
 * Date:          2023/07/18 15:41:34
 * Describe:      子弹工厂
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DemoGame.Bullet
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

        /// <summary>    伤害半径    </summary>
        public float HitRadius;

        /// <summary>    跟踪半径  如果没有跟踪能力则认为0   </summary>
        public float TrackRadius;

        /// <summary>    子弹穿透    </summary>
        public float Penetrate;

        /// <summary>    暴击率      </summary>
        public float CritRate;

        /// <summary>    暴击加成    </summary>
        public float CritDamage;

        /// <summary>    移动方式    </summary>
        public BulletMove MoveType;

        /// <summary>    材质球    </summary>
        public Material Material;

        /// <summary>    子弹大小    </summary>
        public Vector2 ModelSize;

        public BulletAttr(float lifeTime, float damage, float moveSpeed, Material material, float interval,
            float hitRadius,
            float penetrate, float critRate, float critDamage, BulletMove moveType, Vector2 modelSize,
            float trackRadius)
        {
            LifeTime = lifeTime;
            Damage = damage;
            MoveSpeed = moveSpeed;
            Material = material;
            Interval = interval;
            HitRadius = hitRadius;
            Penetrate = penetrate;
            CritRate = critRate;
            CritDamage = critDamage;
            MoveType = moveType;
            ModelSize = modelSize;
            TrackRadius = trackRadius;
        }
    }

    /// <summary>
    /// 子弹种类
    /// </summary>
    public enum BulletType
    {
        //火球
        FireBall,
    }

    /// <summary>
    /// 子弹属性工厂
    /// </summary>
    public class BulletFactory
    {
        public readonly Dictionary<BulletType, BulletAttr> bulletAttrDB = null;

        public BulletFactory()
        {
            //TODO 后期读取配置表实现
            bulletAttrDB = new Dictionary<BulletType, BulletAttr>
            {
                {
                    BulletType.FireBall, new BulletAttr(5, 1, 10,
                        GameManager.Instance.ResourceManager.Load<Material>("Bullet/FireBall"), 0.5f, 0.4f, 0, 0, 0.5f,
                        new TrackingMove(), new Vector2(0.6f, 1.6f), 5)
                },
            };
        }

        /// <summary>
        /// 获取子弹属性
        /// </summary>
        /// <param name="bulletType"></param>
        /// <returns></returns>
        public BulletAttr GetAttr(BulletType bulletType)
        {
            return bulletAttrDB[bulletType];
        }

        public BulletDetail GetDetail(BulletType bulletType) => bulletType switch
        {
            BulletType.FireBall => new FireBallDetail(),
            _ => throw new ArgumentException("类型未处理{bulletType}"),
        };
    }
}
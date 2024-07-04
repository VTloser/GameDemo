/*
 * FileName:      EnemyFactory.cs
 * Author:        魏宇辰
 * Date:          2023/07/19 14:30:20
 * Describe:      怪物工厂
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DemoGame.Enemy
{

    /// <summary>
    /// 怪物属性
    /// </summary>
    public class EnemyAttr
    {
        /// <summary>    攻击距离       </summary>
        public float AttackRange;

        /// <summary>     伤害       </summary>
        public float Damage;

        /// <summary>    攻击间隔    </summary>
        public float Interval;
        
        /// <summary>     移动速度   </summary>
        public float MoveSpeed;
        
        /// <summary>    移动朝向目标    </summary>
        public Transform Tag;
        
        /// <summary>    怪物名称    </summary>
        public string ModeName;

        /// <summary>    碰撞大小    </summary>
        public float Radius;

        /// <summary>    显示大小    </summary>
        public Vector2 Size;

        /// <summary>    血量上限    </summary>
        public float MaxHp;

        /// <summary>    伤害减免率    </summary>
        public float DamageReduction;
        
        /// <summary>    材质球    </summary>
        public Material Material;

        public EnemyAttr(float attackRange, float damage, float moveSpeed, float interval, float maxHp, string modeName,
            float radius, float damageReduction, Material material, Vector2 size)
        {
            AttackRange = attackRange;
            Damage = damage;
            MoveSpeed = moveSpeed;
            Interval = interval;
            MaxHp = maxHp;
            ModeName = modeName;
            Radius = radius;
            Material = material;
            DamageReduction = damageReduction;
            Size = size;
        }
    }

    /// <summary>
    /// 怪物种类
    /// </summary>
    public enum EnemyType
    {
        /// <summary>  红色史莱姆  </summary>
        RedSlime = 1 << 1,

        /// <summary>  绿色史莱姆  </summary>
        GreenSlime = 1 << 2,
    }

    /// <summary>
    /// 怪物工厂
    /// </summary>
    public class EnemyFactory
    {
        public readonly Dictionary<EnemyType, EnemyAttr> EnemyAttrDB = null;

        public EnemyFactory()
        {
            EnemyAttrDB = new Dictionary<EnemyType, EnemyAttr>
            {
                {
                    EnemyType.RedSlime, new EnemyAttr(1, 20, 1f, 0, 1, "DemoEnemy", 1f, 0.2f,
                        GameManager.Instance.ResourceManager.Load<Material>("Material/DemoEnemy2"), new Vector2(2, 2))
                },
                {
                    EnemyType.GreenSlime, new EnemyAttr(20, 20, 1f, 0, 1, "DemoEnemy", 1f, 0.2f,
                        GameManager.Instance.ResourceManager.Load<Material>("Material/DemoEnemy1"), new Vector2(2, 2))
                },
            };
        }

        /// <summary>
        /// 切换主角信息
        /// </summary>
        /// <param name="tag"></param>
        public void ChangePlayer(Transform tag)
        {
            foreach (var item in EnemyAttrDB.Values)
            {
                item.Tag = tag;
            }            
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="enemyType"></param>
        /// <returns></returns>
        public EnemyAttr GetAttr(EnemyType enemyType)
        {
            return EnemyAttrDB[enemyType];
        }
        
        /// <summary>
        /// 获取某种怪物属性
        /// </summary>
        /// <param name="enemyType"></param>
        /// <returns></returns>
        public EnemyDetail GetEnemyDetail(EnemyType enemyType) => enemyType switch
        {
            EnemyType.RedSlime => new RedSlime(),
            EnemyType.GreenSlime => new GreenSlime(),
            _ => throw new ArgumentException(nameof(enemyType), $"未处理当前类型{enemyType}")
        };
        
        /// <summary>
        /// 设定某几种怪物类型，然后随机产生一种,
        /// 效率不高 不建议使用
        /// </summary>
        /// <param name="enemyType"></param>
        /// <returns></returns>
        public EnemyDetail GetRandomDetail(EnemyType enemyType)
        {
            List<EnemyDetail> res = new List<EnemyDetail>();
            foreach (EnemyType item in Enum.GetValues(typeof(EnemyType)))
            {
                if (enemyType.HasFlag(item))
                {
                    res.Add(GetEnemyDetail(item));
                }
            }
            return res[Random.Range(0, res.Count)];
        }
    }
}
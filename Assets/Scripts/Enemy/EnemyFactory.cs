/*
 * FileName:      EnemyFactory.cs
 * Author:        魏宇辰
 * Date:          2023/07/19 14:30:20
 * Describe:      怪物工厂
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System.Collections.Generic;
using UnityEngine;


namespace DemoGame
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

    /// <summary>
    /// 怪物工厂
    /// </summary>
    public class EnemyFactory
    {
        public Dictionary<EnemyType, EnemyAttr> EnemyAttrDB = null;

        public EnemyFactory()
        {
            EnemyAttrDB = new Dictionary<EnemyType, EnemyAttr>();
            EnemyAttrDB.Add(EnemyType.None,
                new EnemyAttr(2, 2, 1f, 0, 1, "DemoEnemy", 0.5f, 0.2f,
                    GameManager.Instance.ResourceManager.Load<Material>("Material/DemoEnemy"), new Vector2(2, 2)));
            EnemyAttrDB.Add(EnemyType.Height,
                new EnemyAttr(2, 2, 1f, 0, 1, "DemoEnemy", 0.5f, 0.2f,
                    GameManager.Instance.ResourceManager.Load<Material>("Material/DemoEnemy"), new Vector2(2, 2)));
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
        /// 获取怪物属性
        /// </summary>
        /// <param name="enemyType"></param>
        /// <returns></returns>
        public EnemyAttr GetEnemyAttr(EnemyType enemyType)
        {
            return EnemyAttrDB[enemyType];
        }
    }
}
/*
 * FileName:      EnemyFactory.cs
 * Author:        魏宇辰
 * Date:          2023/07/19 14:30:20
 * Describe:      怪物工厂
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using DemoGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoGame
{

    public class EnemyAttr
    {
        /// <summary>    攻击距离       </summary>
        public float AttackRange;
        /// <summary>     伤害       </summary>
        public float Damage;
        /// <summary>     移动速度   </summary>
        public float MoveSpeed;
        /// <summary>    攻击间隔    </summary>
        public float Interval;
        /// <summary>    移动朝向    </summary>
        public Transform Tag;
        /// <summary>    血量上限    </summary>
        public float MaxHp;
        /// <summary>    模型名称    </summary>
        public string ModeName;

        public EnemyAttr(float attackRange, float damage, float moveSpeed, float interval, float maxHp, string modeName)
        {
            AttackRange = attackRange;
            Damage = damage;
            MoveSpeed = moveSpeed;
            Interval = interval;
            MaxHp = maxHp;
            ModeName = modeName;
        }
    }
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


    public class EnemyFactory
    {
        private Dictionary<EnemyType, EnemyAttr> EnemyAttrDB = null;
        public EnemyFactory()
        {
            EnemyAttrDB = new Dictionary<EnemyType, EnemyAttr>();
            EnemyAttrDB.Add(EnemyType.None,      new EnemyAttr(2, 2, 0.1f, 2, 200, "DemoEnemy"));
            EnemyAttrDB.Add(EnemyType.Height,    new EnemyAttr(2, 2, 0.1f, 2, 200, "DemoEnemy"));
            EnemyAttrDB.Add(EnemyType.Long,      new EnemyAttr(2, 2, 0.1f, 2, 200, "DemoEnemy"));
            EnemyAttrDB.Add(EnemyType.Fast,      new EnemyAttr(2, 2, 0.1f, 2, 200, "DemoEnemy"));
            EnemyAttrDB.Add(EnemyType.FastShoot, new EnemyAttr(2, 2, 0.1f, 2, 200, "DemoEnemy"));
        }

        public void ChangePlayer(Transform tag)
        {
            foreach (var item in EnemyAttrDB.Values)
            {
                item.Tag = tag;
            }            
        }


        public EnemyAttr GetEnemyAttr(EnemyType enemyType)
        {
            return EnemyAttrDB[enemyType];
        }
    }

}
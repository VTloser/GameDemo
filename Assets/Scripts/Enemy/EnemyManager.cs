/*
 * FileName:      EnemyManager.cs
 * Author:        摩诘创新
 * Date:          2023/07/19 15:02:53
 * Describe:      怪物对象池
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using DemoGame.Enemy;
using DemoGame.Pool;
using UnityEngine;



namespace DemoGame
{
    /// <summary>
    /// 怪物管理系统
    /// </summary>
    public class EnemyManager
    {
        public EnemyAgaent _Enemy;
        public Pool<EnemyAgaent> EnemyPool;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            _Enemy = GameManager.Instance.ResourceManager.Load<EnemyAgaent>("Enemy");
            EnemyPool = new Pool<EnemyAgaent>(_Enemy, null, 64);
        }

        /// <summary>
        /// 销毁怪物
        /// </summary>
        /// <param name="enemy"></param>
        public void Destroy(EnemyAgaent enemy)
        {
            EnemyPool.DestObject(enemy);
        }

        /// <summary>
        /// 获取一个怪物
        /// </summary>
        /// <returns></returns>
        public EnemyAgaent GetEnemy(EnemyType enemyType)
        {
            var enemy = EnemyPool.GetObject();
            enemy.Init(enemyType);
            return enemy;
        }
    }
}

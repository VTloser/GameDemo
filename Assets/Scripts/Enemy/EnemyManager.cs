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
using UnityEngine;

namespace DemoGame
{
    public class EnemyManager
    {
        public EnemyAgaent _Enemy;
        private Pool<EnemyAgaent> EnemyPool;

        public void Init()
        {
            _Enemy = GameManager.Instance.ResourceManager.Load<EnemyAgaent>("Enemy");
            EnemyPool = new Pool<EnemyAgaent>(_Enemy, null, 64);
        }


        public void Destroy(EnemyAgaent enemy)
        {
            EnemyPool.DestObject(enemy);
        }

        public EnemyAgaent GetEnemy(EnemyDetail enemyDetail)
        {
            var enemy = EnemyPool.GetObject();
            enemy._EnemyDetail = enemyDetail;
            enemy._EnemyDetail.Init(enemy);
            return enemy;
        }
    }
}

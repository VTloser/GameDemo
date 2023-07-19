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
        public Enemy _Enemy;
        private Pool<Enemy> EnemyPool;

        public EnemyDetail CurrentEnemyDetail;

        public void Init()
        {
            _Enemy = GameManager.ResourceManager.Load<Enemy>("Enemy");
            EnemyPool = new Pool<Enemy>(_Enemy, null, 100);

            CurrentEnemyDetail = new DemoEnemyDetail();
        }

        public void Destroy(Enemy enemy)
        {
            EnemyPool.DestObject(enemy);
        }

        public Enemy GetEnemy()
        {
            return EnemyPool.GetObject();
        }
    }
}

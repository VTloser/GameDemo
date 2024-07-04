/*
 * FileName:      ComputerManager.cs
 * Author:        摩诘创新
 * Date:          2023/07/26 15:47:06
 * Describe:      ComputerShader 计算管理模块
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System.Collections.Generic;
using System.Runtime.InteropServices;
using DemoGame.Bullet;
using DemoGame.Enemy;
using DemoGame.Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace DemoGame.Manager.Computer
{
    /// <summary>
    ///  ComputerShader 计算管理模块
    /// </summary>
    public class ComputerManager : MonoBehaviour
    {

        [FormerlySerializedAs("BulletAndEnemyCS")] public ComputeShader bulletAndEnemyCs; //子弹敌人计算
        
        [FormerlySerializedAs("EnemyCS")] public ComputeShader enemyCs;


        private BulletAndEnemy _bulletAndEnemy = new();
        private EnemyCollider _enemy = new();
        
        
        private readonly List<BulletComputerData> _bulletComputerDates = new List<BulletComputerData>();
        private readonly List<EnemyComputerData> _enemyComputerDates = new List<EnemyComputerData>();
        
        
        /// <summary>  子弹对象池 </summary>
        private Pool<BulletAgent> _bulletPool;
        /// <summary>  子弹对象池 </summary>
        private Pool<EnemyAgaent> _enemyPool;
        
        
        private void Awake()
        {
            _bulletAndEnemy.Init(bulletAndEnemyCs);
            _enemy.Init(enemyCs);
            
            _bulletPool = GameManager.Instance.BulletManager.BulletPool;
            _enemyPool = GameManager.Instance.EnemyManager.EnemyPool;
        }

        private void Update()
        {
            _bulletComputerDates.Clear();
            _enemyComputerDates.Clear();
            

            for (int i = 0; i < _bulletPool.ActiveCount; i++)
            {
                _bulletComputerDates.Add(_bulletPool.ActivateItems[i].bulletDetail.GetData());
            }

            for (int i = 0; i < _enemyPool.ActiveCount; i++)
            {
                _enemyComputerDates.Add(_enemyPool.ActivateItems[i].enemyDetail.GetData());
            }

            // Debug.Log($"当前子弹数：{_bulletComputerDates.Count}");
            // Debug.Log($"当前怪物数：{_enemyComputerDates.Count}");
            
            _enemy.Tick(_enemyComputerDates, GameManager.Instance.Player?.GetData() ?? new PlayerData(Vector2.zero, 0));

            _bulletAndEnemy.Tick(_bulletComputerDates, _enemyComputerDates);
        }
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    public struct BulletComputerData
    {
        public Vector2 pos;        // 等价于float2
        public float hitRange;     // 伤害检测范围
        public int index;          // 伤害范围内的最近的敌人序号,如果为-1则认为没有敌人在附近
        public float followRadius;  // 寻敌半径, 如果为0 则认为没有追踪功能。
        public int followIndex;     // 追踪目标序号，如果为-1 则认为没有在追踪。
        public int num;             // 对象池序号
        public int Hit;             // ???

        public BulletComputerData(Vector2 pos, float hitRange, float followRadius, int num)
        {
            this.pos = pos;
            this.hitRange = hitRange;
            this.index = -1;
            this.followRadius = followRadius;
            this.followIndex = -1;
            this.num = num;
            Hit = 0;
        }
    }
}
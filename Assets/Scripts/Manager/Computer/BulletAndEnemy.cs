/*
 * FileName:      BulletEnemy.cs
 * Author:        Administrator
 * Date:          2024/06/25 14:42:54
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System.Collections.Generic;
using System.Runtime.InteropServices;
using DemoGame.Bullet;
using DemoGame.Enemy;
using UnityEngine;
using DemoGame.Pool;

namespace DemoGame.Manager.Computer
{
    public class BulletAndEnemy
    {
        /// <summary>  子弹怪物相关 ComputerShader </summary>
        private ComputeShader _bulletAndEnemyCs; 

        /// <summary>  暂定最大数量限制  ComputerShader </summary>
        const int MaxCount = 2048;

        /// <summary>  子弹怪物相关 ComputerShader Buffer </summary>
        public ComputeBuffer computeBulletBuffer;

        /// <summary>  怪物间碰撞 ComputerShader Buffer </summary>
        public ComputeBuffer computeEnemyBuffer;
        
        
        private int _bEkernelId;
        
        /// <summary>  接收 </summary>
        private BulletComputerData[] _receiveBullet;
        

        //属性缓存索引
        private static readonly int BulletBuffer = Shader.PropertyToID("BulletBuffer");
        private static readonly int EnemyBuffer = Shader.PropertyToID("EnemyBuffer");
        private static readonly int EnemyCount = Shader.PropertyToID("EnemyCount");


        /// <summary>  子弹对象池 </summary>
        private Pool<BulletAgent> _bulletPool;
        /// <summary>  子弹对象池 </summary>
        private Pool<EnemyAgaent> _enemyPool;


        public void Init(ComputeShader bulletCs)
        {
            _bulletAndEnemyCs = bulletCs;


            computeBulletBuffer = new ComputeBuffer(MaxCount, Marshal.SizeOf(typeof(BulletComputerData)));
            computeEnemyBuffer = new ComputeBuffer(MaxCount, Marshal.SizeOf(typeof(EnemyComputerData)));

            //利用FindKernel来找到我们声明的核函数的下标
            _bEkernelId = _bulletAndEnemyCs.FindKernel(name: "BulletAndEnemyCS");


            _bulletPool = GameManager.Instance.BulletManager.BulletPool;
            _enemyPool = GameManager.Instance.EnemyManager.EnemyPool;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bulletComputerDates"></param>
        /// <param name="enemyComputerDates"></param>
        public void Tick(List<BulletComputerData> bulletComputerDates,List<EnemyComputerData> enemyComputerDates)
        {
            
            computeBulletBuffer.SetData(bulletComputerDates);
            computeEnemyBuffer.SetData(enemyComputerDates);

            _bulletAndEnemyCs.SetBuffer(_bEkernelId, BulletBuffer, computeBulletBuffer);
            _bulletAndEnemyCs.SetBuffer(_bEkernelId, EnemyBuffer, computeEnemyBuffer);
            _bulletAndEnemyCs.SetInt(EnemyCount, enemyComputerDates.Count);


            //调度多少个线程组
            _bulletAndEnemyCs.Dispatch(_bEkernelId,
                Mathf.Clamp(Mathf.CeilToInt(bulletComputerDates.Count / 256f), 1, 8), 1, 1);
            
            _receiveBullet = new BulletComputerData[bulletComputerDates.Count];
            computeBulletBuffer.GetData(_receiveBullet);

            BulletEnemy();
        }

        private void BulletEnemy()
        {
            for (int i = 0; i < _receiveBullet.Length; i++)
            {
                // 跟踪
                if (_receiveBullet[i].followIndex != -1)
                {
                    _bulletPool.Items[_receiveBullet[i].num].bulletDetail.Move(
                        _enemyPool.Items[_receiveBullet[i].followIndex].transform);
                }
                // 直线行走
                else
                {
                    _bulletPool.Items[_receiveBullet[i].num].bulletDetail.Move();
                }
                
                
                //命中事件
                if (_receiveBullet[i].index != -1)
                {
                    _bulletPool.Items[_receiveBullet[i].num].bulletDetail
                        .JudgeHit(_enemyPool.Items[_receiveBullet[i].index]);
                }
            }
        }


        public void OnDestroy()
        {
            computeBulletBuffer.Release();
            computeBulletBuffer.Dispose();
        }
    }
}


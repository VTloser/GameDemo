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
using UnityEngine;

namespace DemoGame.Manager.Computer
{
    public class BulletEnemy
    {
        /// <summary>  子弹怪物相关 ComputerShader </summary>
        private ComputeShader BulletEnemyCS; //子弹敌人计算

        const int MaxCount = 2048;

        /// <summary>  子弹怪物相关 ComputerShader Buffer </summary>
        public ComputeBuffer computeBulletBuffer;

        private int _bEkernelId;

        private List<ComputerDate> _bulletComputerDates = new List<ComputerDate>();

        //[SerializeField]
        ComputerDate[] ReceiveBullet;
        
        private ComputerManager _computerManager;
        
        
        //属性缓存索引
        private static readonly int BulletBuffer = Shader.PropertyToID("BulletBuffer");
        private static readonly int EnemyBuffer = Shader.PropertyToID("EnemyBuffer");

        public void Init(ComputeShader computeShader, ComputerManager computerManager)
        {
            _computerManager = computerManager;
            
            //BulletEnemyCS = computeShader;
            
            computeBulletBuffer = new ComputeBuffer(MaxCount, Marshal.SizeOf(typeof(ComputerDate)));

            ReceiveBullet = new ComputerDate[MaxCount];

            //computeBulletBuffer.SetData(BulletComputerDates);

            //利用FindKernel来找到我们声明的核函数的下标
            _bEkernelId = BulletEnemyCS.FindKernel(name: "BulletEnemyCS");
            
        }

        public void Tick()
        {
            try
            {
                _bulletComputerDates.Clear();
                for (int i = 0; i < GameManager.Instance.BulletManager.BulletPool.Items.Length; i++)
                {
                    _bulletComputerDates.Add(GameManager.Instance.BulletManager.BulletPool.Items[i]._BulletDetail
                        .GetData());
                }
            }
            catch
            {
            }

            computeBulletBuffer.SetData(_bulletComputerDates);

            BulletEnemyCS.SetBuffer(_bEkernelId, BulletBuffer, computeBulletBuffer);
            //BulletEnemyCS.SetBuffer(_bEkernelId, EnemyBuffer, _computerManager.enemyCollider.computeEnemyBuffer);
            
            //调度多少个线程组
            BulletEnemyCS.Dispatch(_bEkernelId, 2048 / 1024, 1, 1);

            computeBulletBuffer.GetData(ReceiveBullet);
            
            for (int i = 0; i < ReceiveBullet.Length; i++)
            {
                //命中事件
                if (ReceiveBullet[i].Live == -1 &&
                    (GameManager.Instance.BulletManager.BulletPool.Items?[i].IsUse).Value)
                {
                    GameManager.Instance.BulletManager.BulletPool.Items[i]._BulletDetail
                        .JudgeHit(GameManager.Instance.EnemyManager.EnemyPool.Items[ReceiveBullet[i].index]);
                }

                // 跟踪
                if (ReceiveBullet[i].Isfloow == -1 &&
                    (GameManager.Instance.BulletManager.BulletPool.Items?[i].IsUse).Value)
                {
                    GameManager.Instance.BulletManager.BulletPool.Items[i]._BulletDetail.Move(GameManager.Instance
                        .EnemyManager.EnemyPool.Items[ReceiveBullet[i].floowindex].transform);
                }
                // 直线行走
                else if (i < GameManager.Instance.BulletManager.BulletPool.Items.Length &&
                         (GameManager.Instance.BulletManager.BulletPool.Items?[i].IsUse).Value)
                {
                    GameManager.Instance.BulletManager.BulletPool.Items?[i]?._BulletDetail?.Move();
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
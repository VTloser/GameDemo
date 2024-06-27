/*
 * FileName:      EnemyCollider.cs
 * Author:        魏宇辰
 * Date:          2024/06/25 14:35:30
 * Describe:      怪物间碰撞 ComputerShader
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DemoGame.Manager.Computer;
using UnityEngine;

namespace DemoGame
{
    public class EnemyCollider
    {
        /// <summary>  怪物间碰撞 ComputerShader </summary>
        public ComputeShader enemyColliderCs; 

        /// <summary>  怪物间碰撞 ComputerShader Buffer </summary>
        public ComputeBuffer computeEnemyBuffer;
        
        const int MaxCount = 2048;
        
        int _ekernelId;
        
        
        //[SerializeField]
        EnemyComputerData[] _receiveEnemy;
        private static readonly int EnemyBuffer = Shader.PropertyToID("EnemyBuffer");
        private static readonly int EnemyCount = Shader.PropertyToID("EnemyCount");

        public void Init(ComputeShader computeShader)
        {
            enemyColliderCs = computeShader;

            computeEnemyBuffer = new ComputeBuffer(MaxCount, Marshal.SizeOf(typeof(EnemyComputerData)));
            
            _ekernelId = enemyColliderCs.FindKernel(name: "EnemyColliderCS");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemyComputerDates"></param>
        public void Tick(List<EnemyComputerData> enemyComputerDates)
        {
            
            if (enemyComputerDates is not { Count: > 0 }) return;

            computeEnemyBuffer.SetData(enemyComputerDates);
            
            enemyColliderCs.SetInt(EnemyCount, enemyComputerDates.Count);
            enemyColliderCs.SetBuffer(_ekernelId, EnemyBuffer, computeEnemyBuffer);
            enemyColliderCs.Dispatch(_ekernelId, Mathf.CeilToInt(enemyComputerDates.Count / 256f), 1, 1);
            
            _receiveEnemy = new EnemyComputerData[enemyComputerDates.Count];
            computeEnemyBuffer.GetData(_receiveEnemy);

            for (int i = 0; i < _receiveEnemy.Length; i++)
            {
                GameManager.Instance.EnemyManager.EnemyPool.Items[_receiveEnemy[i].num].enemyDetail
                    .Move(_receiveEnemy[i].pos);
            }
        }

        public void OnDestroy()
        {
            computeEnemyBuffer.Release();
            computeEnemyBuffer.Dispose();
        }
        
    }
}

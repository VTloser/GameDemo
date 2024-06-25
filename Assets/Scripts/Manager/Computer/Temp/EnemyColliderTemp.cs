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
using UnityEngine;

namespace DemoGame
{
    public class EnemyColliderTemp
    {
        /// <summary>  怪物间碰撞 ComputerShader </summary>
        public ComputeShader EnemyColliderCS; 

        /// <summary>  怪物间碰撞 ComputerShader Buffer </summary>
        public ComputeBuffer computeEnemyBuffer;
        
        const int MaxCount = 2048;
        
        int EkernelId;
        
        List<EnemyComputerData> EnemyComputerDates = new List<EnemyComputerData>();
        
        //[SerializeField]
        EnemyComputerData[] ReceiveEnemy;

        public void Init(ComputeShader computeShader)
        {
            EnemyColliderCS = computeShader;

            computeEnemyBuffer = new ComputeBuffer(MaxCount, Marshal.SizeOf(typeof(EnemyComputerData)));
            
            //computeEnemyBuffer.SetData(EnemyComputerDates);

            EkernelId = EnemyColliderCS.FindKernel(name: "EnemyColliderCSTemp");
        }
        
        public void Tick()
        {
            try
            {
                EnemyComputerDates.Clear();
                for (int i = 0; i < GameManager.Instance.EnemyManager.EnemyPool.Items.Length; i++)
                {
                    EnemyComputerDates.Add(GameManager.Instance.EnemyManager.EnemyPool.Items[i]._EnemyDetail.GetData());
                }
            }
            catch
            {
                // ignored
            }
            
            ReceiveEnemy = new EnemyComputerData[EnemyComputerDates.Count];
            
            computeEnemyBuffer.SetData(EnemyComputerDates);
            EnemyColliderCS.SetBuffer(EkernelId, "EnemyBuffer", computeEnemyBuffer);
            EnemyColliderCS.Dispatch(EkernelId, 2048 / 1024, 1, 1);
            
            computeEnemyBuffer.GetData(ReceiveEnemy);
            
            
            for (int i = 0; i < ReceiveEnemy.Length; i++)
            {
                //Debug.Log(ReceiveEnemy.Length);
                GameManager.Instance.EnemyManager.EnemyPool.Items[i]._EnemyDetail?.Move(ReceiveEnemy[i].pos);
                
                // if (ReceiveEnemy[i].pos !=v  && (GameManager.Instance.EnemyManager.EnemyPool.Items?[i].IsUse).Value)
                // {
                //     GameManager.Instance.EnemyManager.EnemyPool.Items[i]._EnemyDetail?.Move(ReceiveEnemy[i].pos);
                // }
                // else if (i < GameManager.Instance.EnemyManager.EnemyPool.Items.Length &&
                //          (GameManager.Instance.EnemyManager.EnemyPool.Items?[i].IsUse).Value)
                // {
                //     GameManager.Instance.EnemyManager.EnemyPool.Items[i]._EnemyDetail?.Move(Vector3.zero);
                // }
            }
        }

        public void OnDestroy()
        {
            computeEnemyBuffer.Release();
            computeEnemyBuffer.Dispose();
        }
        
    }
}

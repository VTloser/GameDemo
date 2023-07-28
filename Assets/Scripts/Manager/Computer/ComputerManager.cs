/*
 * FileName:      ComputerManager.cs
 * Author:        摩诘创新
 * Date:          2023/07/26 15:47:06
 * Describe:      ComputerShader计算管理模块
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace DemoGame
{


    public class ComputerManager : MonoBehaviour
    {
        public static ComputerManager Instance;

        #region 子弹敌人碰撞管理模块

        public ComputeShader BulletEnemyCS;
        const int MaxCount = 2048;

        ComputeBuffer computeBulletBuffer;
        ComputeBuffer computeEnemyBuffer;
        int kernelId;

        private void Awake()
        {
            Instance = this;
        }

        List<ComputerDate> BulletComputerDates = new List<ComputerDate>();
        List<ComputerDate> EnemyComputerDates = new List<ComputerDate>();

        //[SerializeField]
        ComputerDate[] ReceiveBullet;
        //[SerializeField]
        ComputerDate[] ReceiveEnemy;

        private void Start()
        {
            computeBulletBuffer = new ComputeBuffer(MaxCount, 20); 
            computeEnemyBuffer = new ComputeBuffer(MaxCount, 20);

            ReceiveBullet = new ComputerDate[MaxCount];
            ReceiveEnemy = new ComputerDate[MaxCount];

            computeBulletBuffer.SetData(BulletComputerDates);
            computeEnemyBuffer.SetData(EnemyComputerDates);

            kernelId = BulletEnemyCS.FindKernel(name: "BulletEnemyCS");
        }
        private void Update()
        {
            try
            {
                BulletComputerDates.Clear();
                for (int i = 0; i < GameManager.Instance.BulletManager.BulletPool.Items.Length; i++)
                {
                    BulletComputerDates.Add(GameManager.Instance.BulletManager.BulletPool.Items[i]._BulletDetail.GetData());
                }
            }
            catch { }

            try
            {
                EnemyComputerDates.Clear();
                for (int i = 0; i < GameManager.Instance.EnemyManager.EnemyPool.Items.Length; i++)
                {
                    EnemyComputerDates.Add(GameManager.Instance.EnemyManager.EnemyPool.Items[i]._EnemyDetail.GetData());
                }
            }
            catch { }

            computeBulletBuffer.SetData(BulletComputerDates);
            computeEnemyBuffer.SetData(EnemyComputerDates);

            BulletEnemyCS.SetBuffer(kernelId, "BulletBuffer", computeBulletBuffer);
            BulletEnemyCS.SetBuffer(kernelId, "EnemyBuffer", computeEnemyBuffer);
            BulletEnemyCS.Dispatch(kernelId, 2048 / 1024, 1, 1);

            computeBulletBuffer.GetData(ReceiveBullet);
            //computeEnemyBuffer.GetData(ReceiveEnemy);

            for (int i = 0; i < ReceiveBullet.Length; i++)
            {
                if (ReceiveBullet[i].Live == -1 && (GameManager.Instance.BulletManager.BulletPool.Items?[i].IsUse).Value)
                {
                    GameManager.Instance.BulletManager.BulletPool.Items[i]._BulletDetail.JudgeHit(GameManager.Instance.EnemyManager.EnemyPool.Items[ReceiveBullet[i].index]);
                }
            }
        }

        void OnDestroy()
        {
            computeBulletBuffer.Release();
            computeBulletBuffer.Dispose();

            computeEnemyBuffer.Release();
            computeEnemyBuffer.Dispose();
        }
        #endregion

    }



    public struct ComputerDate
    {
        public Vector2 pos;  //等价于float2
        public float radius; //半径 如果半径小于等于0则认为接触到了
        public int Live;     // 0 是默认状态 -1是死亡状态  1是存活状态
        public int index;    //序号

        public ComputerDate(Vector2 _pos, float _radius, bool _Live) : this()
        {
            pos = _pos;
            radius = _radius;
            Live = _Live ? 1 : -1;
        }
    }
}


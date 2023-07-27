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

namespace DemoGame
{
    [Serializable]
    public struct ComputerDate
    {
        public Vector2 pos;  //等价于float2
        public float index;  //目标index
        public float radius; //半径 如果半径小于等于0则认为接触到了
        public float Boom; //是否接触

        public ComputerDate(Vector2 _pos, float _radius) : this()
        {
            pos = _pos;
            radius = _radius;
            index = -1;
        }
    }


    public class ComputerManager : MonoBehaviour
    {
        public static ComputerManager Instance;

        #region 子弹敌人碰撞管理模块

        public ComputeShader BulletEnemyCS;
        const int MaxCount = 1024;

        ComputeBuffer computeBulletBuffer;
        ComputeBuffer computeEnemyBuffer;
        int kernelId;

        private void Awake()
        {
            Instance = this;
        }

        public List<ComputerDate> BulletComputerDates = new List<ComputerDate>();
        public List<ComputerDate> EnemyComputerDates = new List<ComputerDate>();

        //ComputerDate[] BulletComputerDates;
        //ComputerDate[] EnemyComputerDates;

        [SerializeField]
        ComputerDate[] Receive;

        [SerializeField]
        ComputerDate[] ReceiveEnemy;
        private void Start()
        {
            computeBulletBuffer = new ComputeBuffer(MaxCount, 20);
            computeEnemyBuffer = new ComputeBuffer(MaxCount, 20);

            Receive = new ComputerDate[MaxCount];
            ReceiveEnemy = new ComputerDate[MaxCount];

            //BulletComputerDates = new ComputerDate[MaxCount];
            //EnemyComputerDates = new ComputerDate[MaxCount];

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


            //Debug.Log(GameManager.Instance.BulletManager.BulletPool.Items[0]._BulletDetail.GetData().pos);
            //var g = GameManager.Instance.BulletManager.BulletList.Select(_ => _._BulletDetail.GetData());
            //Debug.Log(g.Count());
            //var t = GameManager.Instance.BulletManager.BulletPool.Items.Select(_ => _._BulletDetail.GetData());
            //Debug.Log(t.Count());
            //BulletComputerDates = GameManager.Instance.BulletManager.BulletPool.Items.Select(_ => _._BulletDetail.GetData()).ToList();
            //EnemyComputerDates  = GameManager.Instance.EnemyManager.EnemyPool.Items.Select(_ => _._EnemyDetail.GetData()).ToList();

            computeBulletBuffer.SetData(BulletComputerDates);
            computeEnemyBuffer.SetData(EnemyComputerDates);

            BulletEnemyCS.SetBuffer(kernelId, "BulletBuffer", computeBulletBuffer);
            BulletEnemyCS.SetBuffer(kernelId, "EnemyBuffer", computeEnemyBuffer);
            BulletEnemyCS.Dispatch(kernelId, 1024, 1, 1);

            computeBulletBuffer.GetData(Receive);
            computeEnemyBuffer.GetData(ReceiveEnemy);

            //for (int i = 0; i < Receive.Length; i++)
            //{
            //    if (Receive[i].index != -1 && Receive[i].radius <= 0)
            //    {
            //        GameManager.Instance.BulletManager.BulletList[i]._BulletDetail.Die();
            //        GameManager.Instance.EnemyManager.EnemyList[(int)Receive[i].index]._EnemyDetail.Die();
            //    }
            //}
        }

        void OnDestroy()
        {
            computeBulletBuffer.Release();
            computeBulletBuffer.Dispose();

            //computeEnemyBuffer?.Release();
            //computeEnemyBuffer = null;
        }

        #endregion

    }
}

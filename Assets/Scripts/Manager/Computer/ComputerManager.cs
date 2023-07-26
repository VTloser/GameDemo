/*
 * FileName:      ComputerManager.cs
 * Author:        摩诘创新
 * Date:          2023/07/26 15:47:06
 * Describe:      ComputerShader计算管理模块
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DemoGame
{
    [Serializable]
    public struct ComputerDate
    {
        public Vector2 pos;  //等价于float2
        public float index;  //目标index
        public float minDis; //最小距离平方
        public float radius; //半径
        public float contact;//是否接触 >=1 接触

        public ComputerDate(Vector2 _pos, float _radius) : this()
        {
            index = -1;
            radius = _radius;
        }
    }


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

        [SerializeField]
        public List<ComputerDate> BulletComputerDates = new List<ComputerDate>();
        [SerializeField]
        public List<ComputerDate> EnemyComputerDates = new List<ComputerDate>();


        private void Start()
        {
            computeBulletBuffer = new ComputeBuffer(MaxCount, 4 * 6);
            computeEnemyBuffer = new ComputeBuffer(MaxCount, 4 * 6);

            kernelId = BulletEnemyCS.FindKernel(name: "BulletEnemyCS");
        }
        private void Update()
        {
            computeBulletBuffer.SetData(BulletComputerDates);
            computeEnemyBuffer.SetData(EnemyComputerDates);

            BulletEnemyCS.SetBuffer(kernelId, "BulletBuffer", computeBulletBuffer);
            BulletEnemyCS.SetBuffer(kernelId, "EnemyBuffer", computeEnemyBuffer);
            BulletEnemyCS.Dispatch(kernelId, 64, 16, 4);
        }

        [SerializeField]
        public List<ComputerDate> ddt = new List<ComputerDate>();
        void OnDestroy()
        {
            computeBulletBuffer?.Release();
            computeBulletBuffer = null;

            computeEnemyBuffer?.Release();
            computeEnemyBuffer = null;
        }

        #endregion



    }
}

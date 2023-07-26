/*
 * FileName:      Demo.cs
 * Author:        摩诘创新
 * Date:          2023/07/25 15:59:18
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DemoGame
{
    public class Demo : MonoBehaviour
    {
        public ComputeShader computeShader;

        const int MaxCount = 2048;

        ComputeBuffer computeBulletBuffer;
        ComputeBuffer computeEnemyBuffer;
        int kernelId;
        void Start()
        {
            computeBulletBuffer = new ComputeBuffer(1000, 20);
            //List<DemoDate> list1 = new List<DemoDate>();
            for (int i = 0; i < 1000; i++)
            {
               // list1.Add(new DemoDate(Random.insideUnitSphere, -1));
            }
            //computeBulletBuffer.SetData(list1);

            computeEnemyBuffer = new ComputeBuffer(1000, 20);
            //List<DemoDate> list2 = new List<DemoDate>();
            for (int i = 0; i < 1000; i++)
            {
                //list2.Add(new DemoDate(Random.insideUnitSphere, -1));
            }
            //computeEnemyBuffer.SetData(list2);


            kernelId = computeShader.FindKernel("CSMain");
        }

        [SerializeField]
        //DemoDate[] receive = new DemoDate[1000];
        void Update()
        {
            //List<DemoDate> list2 = new List<DemoDate>();
            for (int i = 0; i < 1000; i++)
            {
                //list2.Add(new DemoDate(Random.insideUnitSphere, -1));
            }
            //computeEnemyBuffer.SetData(list2);

            computeShader.SetBuffer(kernelId, "BulletBuffer", computeBulletBuffer);
            computeShader.SetBuffer(kernelId, "EnemyBuffer", computeEnemyBuffer);

            computeShader.Dispatch(kernelId, 10, 10, 10);

           // computeBulletBuffer.GetData(receive);

            //foreach (var item in receive)
            //{
            //    Debug.Log(item.index);
            //}
        }


        void OnDestroy()
        {
            computeBulletBuffer.Release();
            computeBulletBuffer = null;
        }
    }
}


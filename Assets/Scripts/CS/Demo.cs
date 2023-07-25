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
using UnityEngine;

namespace DemoGame
{
    public class Demo : MonoBehaviour
    {
        public ComputeShader computeShader;


        ComputeBuffer computeBulletBuffer;
        ComputeBuffer computeEnemyBuffer;
        int kernelId;
        void Start()
        {
            computeBulletBuffer = new ComputeBuffer(1000, 20);
            List<DemoDate> list1 = new List<DemoDate>();
            for (int i = 0; i < 1000; i++)
            {
                list1.Add(new DemoDate(Random.insideUnitSphere, -1));
            }
            computeBulletBuffer.SetData(list1);

            computeEnemyBuffer = new ComputeBuffer(1000, 20);
            List<DemoDate> list2 = new List<DemoDate>();
            for (int i = 0; i < 1000; i++)
            {
                list2.Add(new DemoDate(Random.insideUnitSphere, -1));
            }
            computeEnemyBuffer.SetData(list2);


            kernelId = computeShader.FindKernel("CSMain");
        }
        [SerializeField]
        DemoDate[] receive = new DemoDate[1000];
        void Update()
        {
            List<DemoDate> list2 = new List<DemoDate>();
            for (int i = 0; i < 1000; i++)
            {
                list2.Add(new DemoDate(Random.insideUnitSphere, -1));
            }
            computeEnemyBuffer.SetData(list2);

            computeShader.SetBuffer(kernelId, "BulletBuffer", computeBulletBuffer);
            computeShader.SetBuffer(kernelId, "EnemyBuffer", computeEnemyBuffer);

            computeShader.Dispatch(kernelId, 10, 10, 10);

            computeBulletBuffer.GetData(receive);

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

[System.Serializable]
public struct DemoDate
{
    public Vector3 pos;//等价于float3
    public float index;//等价于float4
    public float minDis;

    public DemoDate(Vector3 _pos, float _index) : this()
    {
        pos = _pos;
        index = _index;
    }
}
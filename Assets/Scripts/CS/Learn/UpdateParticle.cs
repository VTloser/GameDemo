/*
 * FileName:      UpdateParticle.cs
 * Author:        摩诘创新
 * Date:          2023/07/25 14:19:37
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DemoGame
{
    public class UpdateParticle : MonoBehaviour
    {
        public ComputeShader computeShader;
        public Material material;

        const int mParticleCount = 20000;
        int kernelId;


        //count代表buffer中元素的数量
        //stride指的是每个元素占用的空间（字节）
        //注意释放
        //ComputeBuffer buffer  = new ComputeBuffer(int count, int stride);

        ComputeBuffer computeBuffer;




        void Start()
        {
            //struct中一共7个float，size=28
            computeBuffer = new ComputeBuffer(mParticleCount,28);
            ParticleData[] particleDatas = new ParticleData[mParticleCount];
            computeBuffer.SetData(particleDatas);
            kernelId = computeShader.FindKernel("UpdateParticle");
        }

        void Update()
        {
            //ParticleBuffer  
            computeShader.SetBuffer(kernelId, "ParticleBuffer", computeBuffer);
            computeShader.SetFloat("Time", Time.time);
            computeShader.Dispatch(kernelId, mParticleCount / 1000, 1, 1);

            //ParticleBuffer  
            material.SetBuffer("_particleDataBuffer", computeBuffer);
        }

        void OnRenderObject()
        {
            material.SetPass(0);
            Graphics.DrawProceduralNow(MeshTopology.Points, mParticleCount);
        }

        void OnDestroy()
        {
            computeBuffer.Release();
            computeBuffer = null;
        }
    }
    public struct ParticleData
    {
        public Vector3 pos;//等价于float3
        public Color color;//等价于float4
    }
}


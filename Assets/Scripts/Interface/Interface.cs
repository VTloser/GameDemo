/*
 * FileName:      InteFace.cs
 * Author:        魏宇辰
 * Date:          2023/07/19 14:24:02
 * Describe:      接口库
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using DemoGame;
using UnityEngine;


public interface IMiniMap
{
    public MiniType _MiniType { get; }

    public Transform _Transform { get; }
}

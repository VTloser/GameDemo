/*
 * FileName:      Props.cs
 * Author:        魏宇辰
 * Date:          2023/07/24 09:40:37
 * Describe:      道具基础类
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DemoGame
{
    public abstract class PropsBase : MonoBehaviour, IMiniMap
    {
        #region 小地图部分
        private void OnEnable()
        {
            GameManager.Instance.MiniMapTail.Add(this);
        }

        private void OnDisable()
        {
            GameManager.Instance.MiniMapTail.Remove(this);
        }

        public MiniType _MiniType { get => MiniType.Props; }

        public Transform _Transform { get => this.transform; }
        #endregion

        /// <summary>     道具名字     </summary>
        public string Name;
        /// <summary>     道具ID     </summary>
        public float ID;
        /// <summary>     道具稀有度     </summary>
        public Rarity rarity;

        /// <summary>     获取道具     </summary>
        public abstract void Get();
        /// <summary>     丢弃道具     </summary>
        public abstract void Dis();
    }
}

public enum Rarity : byte
{
    None,
    N,
    R,
    SR,
    SSR,
    UR
}
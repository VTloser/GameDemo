/*
 * FileName:      UIMgr.cs
 * Author:        Administrator
 * Date:          2024/07/03 13:34:15
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System;
using UnityEngine;

namespace DemoGame.UI
{
    public class UIMgr : MonoBehaviour
    {
        public static UIMgr Instance;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>  背包根节点  </summary>
        public Transform Warehouse;

        /// <summary>  动态UI根节点  </summary>
        public Transform DynamicUI;

        /// <summary>  小地图根节点  </summary>
        public Transform MiniMap;

        /// <summary>  状态栏  </summary>
        public Transform StatusUI;
    }
}
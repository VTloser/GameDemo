/*
 * FileName:      BaseUnit.cs
 * Author:        摩诘创新
 * Date:          2023/07/31 16:42:37
 * Describe:      基础单位类
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DemoGame
{
    public class BaseUnit : MonoBehaviour
    {
        public UnityAction UpdateAction;
        public UnityAction ResleseAction;

        public virtual void Update()
        {
            UpdateAction?.Invoke();
        }

        public virtual void OnDestroy()
        {
            ResleseAction?.Invoke();
        }
    }
}

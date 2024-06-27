/*
 * FileName:      ComputerManager.cs
 * Author:        摩诘创新
 * Date:          2023/07/26 15:47:06
 * Describe:      对象池接口类
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */


using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DemoGame.Pool
{
    public class Pool<T> where T : Component, IPoolBase
    {

        /// <summary>
        /// 构造器 初始化时产生多少物体
        /// </summary>
        /// <param name="tempt"></param>
        /// <param name="parent"></param>
        /// <param name="size"></param>
        public Pool(T tempt, Transform parent, int size)
        {
            _ttype = tempt;
            MaxSize = size;
            Items = new T[MaxSize];
            Parent = parent;
            //首先初始化出MaxSize个
            for (int i = 0; i < MaxSize; i++)
            {
                Items[i] = _ttype.Create(_ttype, Parent, i);
            }
        }

        /// <summary>   父节点位置   </summary>
        private readonly Transform Parent;
        
        /// <summary>   类型   </summary>
        private T _ttype;
        
        /// <summary>   对象池最大容量   </summary>
        public int MaxSize;
        
        /// <summary>   对象池   </summary>
        public T[] Items;

        /// <summary>   对象池中所有激活的对象   </summary>
        public List<T> ActivateItems = new();
        
        /// <summary>   对象池激活数量   </summary>
        public int ActiveCount;

        /// <summary>
        /// 根据Num获取对象
        /// </summary>
        /// <returns></returns>
        public T GetObjectByNum(int Num)
        {
            //return ActivateItems.Find(X => X.Num == Num);
            return Items[Num];
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        public T GetObject()
        {
            for (int i = 0; i < MaxSize; i++)
            {
                if (!(Items[i].IsUse))
                {
                    Items[i].Get();
                    ActivateItems.Add(Items[i]);
                    ActiveCount++;
                    return Items[i];
                }
            }
            //遍历数组后没有不再使用的
            return DynamicAddSize();
        }

        /// <summary>
        /// 获取对象 带有回调方法
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public T GetObject(UnityAction<T> action)
        {
            for (int i = 0; i < MaxSize; i++)
            {
                if (!Items[i].IsUse)
                {
                    action?.Invoke(Items[i]);
                    Items[i].Get();
                    ActivateItems.Add(Items[i]);
                    ActiveCount++;
                    return Items[i];
                }
            }
            //遍历数组后没有不再使用的
            return DynamicAddSize();
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <param name="t"></param>
        public void DestObject(T t)
        {
            // Items[t.Num].Release();
            // ActivateItems.Remove( Items[t.Num]);
            
            t.Release();
            ActivateItems.Remove(t);
            ActiveCount--;
        }

        /// <summary>
        /// 销毁所有对象
        /// </summary>
        public void DestObjectAll()
        {
            ActiveCount = 0;
            foreach (var VARIABLE in Items)
            {
                if(VARIABLE.IsUse) VARIABLE.Release();
            }
        }

        /// <summary>
        /// 动态调整数组长度
        /// </summary>
        public T DynamicAddSize()
        {
            //Debug.Log($"数组长度不足");
            int n = 1;
            while (n <= MaxSize) n *= 2;
            T[] temp = new T[n];
            Array.Copy(Items, 0, temp, 0, Items.Length);
            Items = temp;
            for (int i = MaxSize; i < n; i++)
            {
                Items[i] = _ttype.Create(_ttype, Parent, i);
            }

            int recordNum = MaxSize;
            MaxSize = n;
            
            //Debug.Log($"数组长度不足，动态调整数组长度,调整后长度{MaxSize}");
            Items[recordNum].Get();
            ActivateItems.Add(Items[recordNum]);
            ActiveCount++;
            return Items[recordNum];
        }

        /// <summary>
        /// 批量式初始化
        /// </summary>
        /// <param name="size"></param>
        public void DynamicAddSize(int size)
        {
            Debug.Log("数组长度不足，动态调整数组长度");
            int n = 1;
            while (n <= size) n *= 2;
            T[] Temp = new T[n];
            Array.Copy(Items, 0, Temp, 0, Items.Length);
            Items = Temp;
            for (int i = MaxSize; i < n; i++)
            {
                Items[i] = _ttype.Create(_ttype, Parent, i);
            }
            MaxSize = n;
            Debug.Log($"调整后长度{MaxSize}");
        }


        /// <summary>
        /// TODO 暂时不知道怎么处理
        /// </summary>
        public void DynamciReduceSize()
        {
            Debug.Log("数组长度过长，动态调整数组长度");
            int n = MaxSize / 2;
            T[] Temp = new T[n];
            Array.Copy(Items, 0, Temp, 0, Items.Length);
            Items = Temp;
            for (int i = n; i < MaxSize; i++)
            {
                MonoBehaviour.Destroy(Items[i]);
            }
            MaxSize = n;
            Debug.Log($"调整后长度{MaxSize}");
        }
    }


    public interface IPoolBase
    {
        bool IsUse { get; set; }

        int Num { get; set; }

        /// <summary>    获取资源    </summary>
        public void Get();

        /// <summary>    释放资源    </summary>
        public void Release();

        /// <summary>   创建对象池物体   </summary>
        public virtual T Create<T>(T original, Transform parent, int number) where T : Component, IPoolBase
        {
            var t = GameObject.Instantiate<T>(original, parent);
            t.name = original.name;
            t.Num = number;
            t.Release();
            t.IsUse = false;
            return t;
        }
    }
}


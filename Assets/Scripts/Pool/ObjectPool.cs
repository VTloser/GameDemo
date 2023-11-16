using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace DemoGame
{
    public class Pool<T> where T : Component, IPoolBase
    {
        /// <summary>
        ///  构造器 初始化时产生多少物体
        /// </summary>
        /// <param name="Tempt"></param>
        /// <param name="trans"></param>
        /// <param name="Size"></param>
        public Pool(T Tempt, Transform parent, int Size)
        {
            t = Tempt;
            MaxSize = Size;
            Items = new T[MaxSize];
            Parent = parent;
            //首先初始化出MaxSize个
            for (int i = 0; i < MaxSize; i++)
            {
                Items[i] = t.Create(t, Parent, i);
            }
        }

        /// <summary>   父节点位置   </summary>
        private Transform Parent;
        
        /// <summary>   类型   </summary>
        private T t;
        
        /// <summary>   对象池最大容量   </summary>
        public int MaxSize;
        
        /// <summary>   对象池   </summary>
        public T[] Items;

        /// <summary>   对象池激活数量   </summary>
        public int ActiveCount;

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns></returns>
        public T GetObject()
        {
            for (int i = 0; i < MaxSize; i++)
            {
                if (!(Items[i].IsUse))
                {
                    Items[i].Get();
                    ActiveCount++;
                    return Items[i];
                }
            }
            //遍历数组后没有不再使用的
            return DynamicAddSize();
        }

        /// <summary>
        /// 获取对象
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
            Items[t.Num].Release();
            ActiveCount--;
        }

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
            int RecordNum;
            int n = 1;
            while (n <= MaxSize) n *= 2;
            T[] Temp = new T[n];
            Array.Copy(Items, 0, Temp, 0, Items.Length);
            Items = Temp;
            for (int i = MaxSize; i < n; i++)
            {
                Items[i] = t.Create(t, Parent, i);
            }
            RecordNum = MaxSize;
            MaxSize = n;
            //Debug.Log($"数组长度不足，动态调整数组长度,调整后长度{MaxSize}");
            Items[RecordNum].Get();
            return Items[RecordNum];
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
                Items[i] = t.Create(t, Parent, i);
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


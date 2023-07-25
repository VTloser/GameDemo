/*
 * FileName:      LP.cs
 * Author:        摩诘创新
 * Date:          2023/07/25 16:08:34
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace DemoGame
{
    public class LP : MonoBehaviour
    {
        public List<DemoDate> list1 = new List<DemoDate>();
        public List<DemoDate> list2 = new List<DemoDate>();

        void Start()
        {
            for (int i = 0; i < 1000; i++)
            {
                list1.Add(new DemoDate(Random.insideUnitSphere, -1));
            }
            for (int i = 0; i < 1000; i++)
            {
                list2.Add(new DemoDate(Random.insideUnitSphere, -1));
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < list1.Count; i++)
            {
                for (int j = 0; j < list2.Count; j++)
                {
                    if ((list1[i].pos - list2[j].pos).sqrMagnitude < list1[i].minDis)
                    {
                        var t = list1[i];
                        t.minDis = (list1[i].pos - list2[j].pos).sqrMagnitude;
                        t.index = j;
                        list1[i] = t;
                    }
                }
            }
        }
    }
}

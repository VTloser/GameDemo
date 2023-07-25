/*
 * FileName:      NewBehaviourScript.cs
 * Author:        摩诘创新
 * Date:          2023/07/25 14:03:11
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform[] Trans;
    void Start()
    {
        for (int i = 0; i < Trans.Length; i++)
        {
            GG(Trans[i], i);
        }
    }

    public void GG(Transform transform, int index)
    {
        index += 1000;
        float x = Mathf.Sin(index );
        float y = Mathf.Sin(index *1.2f);

        Debug.Log(x * x + y * y);

        Vector3 forward = new Vector3(x, y, -Mathf.Sqrt(1 - x * x - y * y));
        transform.position += forward*10;
    }

}

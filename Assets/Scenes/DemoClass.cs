/*
 * FileName:      DemoClass.cs
 * Author:        摩诘创新
 * Date:          2023/08/04 10:56:45
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DemoClass : MonoBehaviour
{
    public DemoC demoC = new DemoC();

    // Update is called once per frame
    void Update()
    {
        demoC.Pos = this.transform.position;
        demoC.Radio = this.transform.localScale.x / 2;
    }
}

[Serializable]
public class DemoC
{
    [SerializeField]
    public Vector2 Pos;
    public float Radio;
}
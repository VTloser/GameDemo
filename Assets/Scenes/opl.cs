/*
 * FileName:      opl.cs
 * Author:        摩诘创新
 * Date:          2023/10/24 15:58:04
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class opl : MonoBehaviour
{
    float a = 0;

    public float angle = 45f;
    public float angleTime = 1f;

    private void Awake()
    {
        // this.transform.DORotate(new Vector3(0, 0, angle / 2f), angleTime / 2f).SetRelative().OnComplete(() =>
        // {
        //
        // });
        
        Sequence se = DOTween.Sequence();
        se.AppendInterval(2);//等待一段时间
        se.Append(this.transform.DORotate(new Vector3(0, 0, angle), angleTime)).SetRelative(); //增加一段动画
        se.Append(this.transform.DORotate(new Vector3(0, 0,-angle*4), angleTime*4).SetRelative()); //增加一段动画
        se.SetLoops(-1, LoopType.Restart);
    }

    void Update()
    {
        a += 0.01f; //弧度每帧增加0.01f
        // 物体沿着y轴移动
        this.transform.Translate(Vector3.up * (10 * Time.deltaTime));
        
        
        
        
        
        //物体y轴方向移动是sin曲线形式（mathf.sin (a)表示将a这个弧度传入并转化为对应数值）（后面*0.1f是为了减弱速度）
    }
}
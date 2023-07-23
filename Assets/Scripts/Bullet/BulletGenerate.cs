/*
 * FileName:      BulletGenerate.cs
 * Author:        13022
 * Date:          2023/07/23 19:42:56
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoGame
{

    public class GenerateAttr
    {
        /// <summary>   生成数量   </summary>
        public float Count;
    }

    public abstract class BulletGenerate
    {
        public BulletDetail bulletDetail;

        public GenerateAttr generateAttr;

        public float LastGenerate;

        public BulletGenerate(BulletDetail _bulletDetail)
        {
            bulletDetail = _bulletDetail;
        }

        public abstract void Generate(Vector3 ForWard);
    }

    /// <summary>
    /// 圆环生成器
    /// </summary>
    public class CircleGenerate : BulletGenerate
    {
        public CircleGenerate(BulletDetail _bulletDetail) : base(_bulletDetail)
        {
            generateAttr = new GenerateAttr() { Count = 36 };
        }

        public override void Generate(Vector3 ForWard)
        {
            if (Time.time - LastGenerate < bulletDetail.bulletAttr.Interval) return;

            for (int i = 0; i < generateAttr.Count; i++)
            {
                var bullet = GameManager.Instance.BulletManager.GetBullet(bulletDetail.Clone() /*as BulletDetail*/);
                bullet.transform.up = Quaternion.Euler(new Vector3(0, 0, (360f / generateAttr.Count) * i)) * ForWard;
                bullet.transform.position = GameManager.Instance.Player.transform.position ;
            }
            LastGenerate = Time.time;
        }
    }

    /// <summary>
    /// 圆环方向生成器
    /// </summary>
    public class CircleDirGenerate : BulletGenerate
    {
        public float Radio;
        public CircleDirGenerate(BulletDetail bulletDetail, float radio) : base(bulletDetail)
        {
            generateAttr = new GenerateAttr() { Count = 36 };
            Radio = radio;
        }

        public override void Generate(Vector3 ForWard)
        {
            if (Time.time - LastGenerate < bulletDetail.bulletAttr.Interval) return;

            for (int i = 0; i < generateAttr.Count; i++)
            {
                var bullet = GameManager.Instance.BulletManager.GetBullet(bulletDetail.Clone() /*as BulletDetail*/);
                bullet.transform.up = ForWard;
                var angle = 360f / generateAttr.Count * i * Mathf.Deg2Rad;
                bullet.transform.position = GameManager.Instance.Player.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * Radio;
            }
            LastGenerate = Time.time;
        }
    }
}

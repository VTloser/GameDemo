/*
 * FileName:      BulletGenerate.cs
 * Author:        魏宇辰
 * Date:          2023/07/23 19:42:56
 * Describe:      子弹生成管理器
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using Codice.Client.BaseCommands.Merge.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DemoGame
{

    public abstract class BulletGenerate
    {
        public BulletDetail bulletDetail;

        public float Count;

        public float LastGenerate;

        public int WaitTime;

        public BulletGenerate(BulletDetail _bulletDetail, int waiting)
        {
            bulletDetail = _bulletDetail;
            WaitTime = waiting;
        }

        public abstract void Generate(Vector3 ForWard);
    }


    public class DirGenerate : BulletGenerate
    {
        public DirGenerate(BulletDetail _bulletDetail, int waiting) : base(_bulletDetail, waiting)
        {
            Count = 2;
        }

        public async override void Generate(Vector3 ForWard)
        {
            await Task.Delay(WaitTime);
            if (Time.time - LastGenerate < bulletDetail.bulletAttr.Interval) return;

            for (int i = 0; i < Count; i++)
            {
                var bullet = GameManager.Instance.BulletManager.GetBullet(bulletDetail.Clone());
                bullet.transform.up = Quaternion.Euler(new Vector3(0, 0, (i - (Count - 1) / 2) * 20f / Count)) * ForWard;
                bullet.transform.position = GameManager.Instance.Player.transform.position;
            }

            LastGenerate = Time.time;
        }
    }


    /// <summary>
    /// 圆环生成器
    /// </summary>
    public class CircleGenerate : BulletGenerate
    {
        public CircleGenerate(BulletDetail _bulletDetail, int waiting) : base(_bulletDetail, waiting)
        {
            Count = 36;
        }

        public async override void Generate(Vector3 ForWard)
        {
            await Task.Delay(WaitTime);
            if (Time.time - LastGenerate < bulletDetail.bulletAttr.Interval) return;
            
            for (int i = 0; i < Count; i++)
            {
                var bullet = GameManager.Instance.BulletManager.GetBullet(bulletDetail.Clone() /*as BulletDetail*/);
                bullet.transform.up = Quaternion.Euler(new Vector3(0, 0, (360f / Count) * i)) * ForWard;
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
        public CircleDirGenerate(BulletDetail _bulletDetail, int waiting, float radio) : base(_bulletDetail, waiting)
        {
            Count = 36;
            Radio = radio;
        }
        
        public async override void Generate(Vector3 ForWard)
        {
            await Task.Delay(WaitTime);
            if (Time.time - LastGenerate < bulletDetail.bulletAttr.Interval) return;

            for (int i = 0; i < Count; i++)
            {
                var bullet = GameManager.Instance.BulletManager.GetBullet(bulletDetail.Clone() /*as BulletDetail*/);
                bullet.transform.up = ForWard;
                var angle = 360f / Count * i * Mathf.Deg2Rad;
                bullet.transform.position = GameManager.Instance.Player.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * Radio;
            }
            LastGenerate = Time.time;
        }
    }
}

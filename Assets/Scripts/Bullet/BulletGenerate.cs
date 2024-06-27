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
using DemoGame.Bullet;
using UnityEngine;


namespace DemoGame
{
    /// <summary>
    /// 子弹生成抽象类
    /// </summary>
    public abstract class BulletGenerate
    {
        /// <summary> 子弹生成类型 </summary>
        public BulletDetail bulletDetail;

        /// <summary> 子弹生成数量 </summary>
        public float Count;

        /// <summary> 上次生成间隔 </summary>
        public int WaitTime;
        
        /// <summary> 上次子弹生成时间 </summary>
        public float LastGenerate;

        public BulletGenerate(BulletDetail _bulletDetail, int waiting)
        {
            bulletDetail = _bulletDetail;
            WaitTime = waiting;
        }

        /// <summary>
        /// 子弹生成
        /// </summary>
        /// <param name="ForWard"> 方向参数 </param>
        public abstract void Generate(Vector3 ForWard);
    }


    /// <summary>
    /// 直射子弹生成器
    /// </summary>
    public class DirGenerate : BulletGenerate
    {
        public DirGenerate(BulletDetail _bulletDetail, int waiting) : base(_bulletDetail, waiting)
        {
            Count = 1;
        }

        public override async void Generate(Vector3 ForWard)
        {
            await Task.Delay(WaitTime);
            if (Time.time - LastGenerate < bulletDetail.bulletAttr.Interval) return;
            for (int i = 0; i < Count; i++)
            {
                var bullet = GameManager.Instance.BulletManager.GetBullet(bulletDetail.Clone());
                bullet.transform.up =
                    Quaternion.Euler(new Vector3(0, 0, (i - (Count - 1) / 2) * 20f / Count)) * ForWard;
                bullet.transform.position = GameManager.Instance.Player.transform.position;
            }

            LastGenerate = Time.time;
        }
    }


    /// <summary>
    /// 圆环子弹生成器
    /// </summary>
    public class CircleGenerate : BulletGenerate
    {
        public CircleGenerate(BulletDetail _bulletDetail, int waiting) : base(_bulletDetail, waiting)
        {
            Count = 36;
        }

        public override async void Generate(Vector3 ForWard)
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

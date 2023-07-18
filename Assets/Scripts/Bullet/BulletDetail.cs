/*
 * FileName:      BulletDetail.cs
 * Author:        魏宇辰
 * Date:          2023/07/18 16:35:53
 * Describe:      子弹描述文件
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using UnityEngine;

namespace DemoGame
{

    public abstract class BulletDetail
    {
        /// <summary>  移动   </summary>
        public abstract void Move(Bullet bullet);

        /// <summary>  子弹生成 可视为一次开火  </summary>
        public abstract void Generate(Vector3 Pos, Vector3 ForWard, BulletDetail bulletDetail);

        /// <summary>  模型  </summary>
        public abstract void Mode();

        /// <summary>  判断命中  </summary>
        public abstract bool JudgeHit();

        /// <summary>  子弹种类  </summary>
        public BulletType bulletType;

        /// <summary>  子弹基础属性  </summary>
        public BulletAttr bulletAttr;

        protected float _LastGenerate;
    }


    public class DemoBulletDetail : BulletDetail
    {
        public DemoBulletDetail()
        {
            bulletType = BulletType.FastShoot;
            bulletAttr = GameManager.bulletFactory.GetBulletAttr(bulletType);
        }

        public override void Generate(Vector3 Pos, Vector3 ForWard, BulletDetail bulletDetail)
        {
            if (Time.time - _LastGenerate > bulletAttr.Interval*2)
            {
                for (int i = 0; i < 2; i++)
                {
                    var t = GameManager.BulletManager.GetBullet();
                    t.transform.forward = ForWard;
                    t.transform.position = Pos - t.transform.right * 0.5f * i;
                    t.Init(bulletDetail);
                }
                _LastGenerate = Time.time;
            }
        }

        public override bool JudgeHit()
        {
            throw new System.NotImplementedException();
        }

        public override void Mode()
        {
            throw new System.NotImplementedException();
        }

        public override void Move(Bullet bullet)
        {
            bullet.transform.Translate(Vector3.forward * Time.deltaTime * bulletAttr.MoveSpeed);
        }

    }
}

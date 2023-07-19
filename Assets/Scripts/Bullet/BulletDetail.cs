/*
 * FileName:      BulletDetail.cs
 * Author:        魏宇辰
 * Date:          2023/07/18 16:35:53
 * Describe:      子弹描述文件
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections.Generic;
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
        public abstract string Mode();

        /// <summary>  判断命中  </summary>
        public abstract bool JudgeHit(Transform transform);

        /// <summary>  判断命中  </summary>
        public abstract void Hit();

        /// <summary>  子弹种类  </summary>
        public BulletType bulletType;

        /// <summary>  子弹基础属性  </summary>
        public BulletAttr bulletAttr;

        /// <summary>  其他武器  </summary>
        public List<BulletDetail> ExtraDetail;
    }

    public class DemoBulletDetail : BulletDetail
    {

        public DemoBulletDetail()
        {
            ExtraDetail = new List<BulletDetail>();
            bulletType = BulletType.None;
            bulletAttr = GameManager.bulletFactory.GetBulletAttr(bulletType);
        }

        public override void Generate(Vector3 Pos, Vector3 ForWard, BulletDetail bulletDetail)
        {
            currentPenetrate = bulletAttr.Penetrate;

            var left = GameManager.BulletManager.GetBullet();
            left.transform.forward = ForWard;
            left.transform.position = Pos - left.transform.right * 0.25f;
            left.Init(bulletDetail);

            var right = GameManager.BulletManager.GetBullet();
            right.transform.forward = ForWard;
            right.transform.position = Pos + right.transform.right * 0.25f;
            right.Init(bulletDetail);

            foreach (var item in ExtraDetail)
            {
                item.Generate(Pos, ForWard, item);
            }
        }

        public override void Hit()
        {
            enemy.gameObject.SetActive(false);
        }

        Enemy enemy;
        float currentPenetrate;
        public override bool JudgeHit(Transform transform)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.1f))
            {
                if (hit.collider.tag == "Enemy")
                {
                    enemy = hit.collider.GetComponent<Enemy>();
                    Hit();
                    if (--currentPenetrate < 0)
                        return true;
                }
            }
            return false;
        }

        public override string Mode()
        {
            return "Cube";
        }

        public override void Move(Bullet bullet)
        {
            bullet.transform.Translate(Vector3.forward * Time.deltaTime * bulletAttr.MoveSpeed);
        }
    }



    public class SideBulletDetail : BulletDetail
    {

        public SideBulletDetail()
        {
            ExtraDetail = new List<BulletDetail>();
            bulletType = BulletType.None;
            bulletAttr = GameManager.bulletFactory.GetBulletAttr(bulletType);
        }

        public override void Generate(Vector3 Pos, Vector3 ForWard, BulletDetail bulletDetail)
        {
            currentPenetrate = bulletAttr.Penetrate;
            var left = GameManager.BulletManager.GetBullet();
            left.transform.right = ForWard;
            left.transform.position = Pos;
            left.Init(bulletDetail);

            var right = GameManager.BulletManager.GetBullet();
            right.transform.right = -ForWard;
            right.transform.position = Pos;
            right.Init(bulletDetail);

            foreach (var item in ExtraDetail)
            {
                item.Generate(Pos, ForWard, item);
            }
        }

        public override void Hit()
        {
            enemy.gameObject.SetActive(false);
        }

        Enemy enemy;
        float currentPenetrate;
        public override bool JudgeHit(Transform transform)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.1f))
            {
                if (hit.collider.tag == "Enemy")
                {
                    enemy = hit.collider.GetComponent<Enemy>();
                    Hit();
                    if (--currentPenetrate < 0)
                        return true;
                }
            }
            return false;
        }

        public override string Mode()
        {
            return "Sphere";
        }

        public override void Move(Bullet bullet)
        {
            bullet.transform.Translate(Vector3.forward * Time.deltaTime * bulletAttr.MoveSpeed);
        }
    }

}

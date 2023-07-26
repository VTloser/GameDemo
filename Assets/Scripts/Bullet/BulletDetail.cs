/*
 * FileName:      BulletDetail.cs
 * Author:        魏宇辰
 * Date:          2023/07/18 16:35:53
 * Describe:      子弹描述文件
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DemoGame
{
    public abstract class BulletDetail/*: ICloneable*/
    {
        /// <summary>  移动   </summary>
        public abstract void Move();

        /// <summary>  子弹生成 可视为一次开火  </summary>
        public abstract void Int(BulletAgaent _bulletAgaent);

        /// <summary>  判断命中  </summary>
        public abstract void JudgeHit();

        /// <summary>  判断命中  </summary>
        public abstract void Hit();

        /// <summary>  生命周期倒计时  </summary>
        public abstract IEnumerator LifeTime();

        /// <summary>  子弹销毁  </summary>
        public abstract void Die();

        public abstract BulletDetail Clone();

        /// <summary>  子弹种类  </summary>
        public BulletType bulletType;

        /// <summary>  子弹基础属性  </summary>
        public BulletAttr bulletAttr;

        /// <summary>  子弹代理  </summary>
        public BulletAgaent bulletAgaent;

        public ComputerDate computerDate;

        protected List<EnemyAgaent> HitEnemy;
    }

    public class FireBallDetail : BulletDetail
    {
        private EnemyAgaent enemy;

        private float currentPenetrate;

        RaycastHit2D rayHit;

        public FireBallDetail()
        {
            bulletType = BulletType.None;
            bulletAttr = GameManager.Instance.bulletFactory.GetBulletAttr(bulletType);
        }

        public override void Int(BulletAgaent _bulletAgaent)
        {
            currentPenetrate = bulletAttr.Penetrate;
            bulletAgaent = _bulletAgaent;

            bulletAgaent.Sprite.sprite = bulletAttr.Sprite;
            HitEnemy = new List<EnemyAgaent>();
            computerDate = new ComputerDate(bulletAttr.Radius);
            GameManager.Instance.BulletManager.AddBulletComputerDate(computerDate);
        }

        public override void Hit()
        {
            enemy._EnemyDetail.Injury(GameManager.Instance.MathManager.Damage(enemy._EnemyDetail.enemyAttr, this.bulletAttr));
            HitEnemy.Add(enemy);
        }

        public override void JudgeHit()
        {
            Debug.DrawLine(bulletAgaent.transform.position, bulletAgaent.transform.position + bulletAgaent.transform.up * bulletAttr.Radius);
            rayHit = Physics2D.Raycast(bulletAgaent.transform.position, bulletAgaent.transform.up, bulletAttr.Radius);
            if (rayHit.collider?.tag == "Enemy")
            {
                enemy = rayHit.collider.GetComponentInParent<EnemyAgaent>();
                if (HitEnemy.Contains(enemy)) return;
                Hit();
                if (--currentPenetrate < 0)
                    Die();
            }
        }

        public override void Move()
        {
            //bulletAgaent.transform.Translate(Vector3.up * Time.deltaTime * bulletAttr.MoveSpeed);
            computerDate.pos = bulletAgaent.transform.position;
        }

        public override IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(bulletAttr.LifeTime);
            Die();
        }

        public override void Die()
        {
            HitEnemy.Clear();
            GameManager.Instance.BulletManager.RemoveBulletComputerDate(computerDate);

            GameManager.Instance.BulletManager.Destroy(bulletAgaent);
        }

        public override BulletDetail Clone()
        {
            return new FireBallDetail();
        }

    }
}

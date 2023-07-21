/*
 * FileName:      BulletDetail.cs
 * Author:        魏宇辰
 * Date:          2023/07/18 16:35:53
 * Describe:      子弹描述文件
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoGame
{
    public abstract class BulletDetail
    {
        /// <summary>  移动   </summary>
        public abstract void Move();

        /// <summary>  子弹生成 可视为一次开火  </summary>
        public abstract void Generate(BulletAgaent _bulletAgaent);

        /// <summary>  判断命中  </summary>
        public abstract void JudgeHit();

        /// <summary>  判断命中  </summary>
        public abstract void Hit();

        /// <summary>  生命周期倒计时  </summary>
        public abstract IEnumerator LifeTime();

        /// <summary>  子弹销毁  </summary>
        public abstract void Die();

        /// <summary>  子弹种类  </summary>
        public BulletType bulletType;

        /// <summary>  子弹基础属性  </summary>
        public BulletAttr bulletAttr;

        /// <summary>  其他武器  </summary>
        public List<BulletDetail> ExtraDetail;

        /// <summary>  子弹代理  </summary>
        public BulletAgaent bulletAgaent;

    }


    public class SuperBulletDetail : BulletDetail
    {
        private EnemyAgaent enemy;

        private float currentPenetrate;

        private float LastGenerate;

        public SuperBulletDetail()
        {
            ExtraDetail = new List<BulletDetail>();
            bulletType = BulletType.None;
            bulletAttr = GameManager.Instance.bulletFactory.GetBulletAttr(bulletType);
        }

        public override void Generate(BulletAgaent _bulletAgaent)
        {
            if (Time.time - LastGenerate < bulletAttr.Interval) return;
            currentPenetrate = bulletAttr.Penetrate;
            bulletAgaent = _bulletAgaent;
            _bulletAgaent.Sprite.sprite = bulletAttr.Sprite;

            int count = 36;
            for (int i = 0; i < count; i++)
            {
                var left1 = GameManager.Instance.BulletManager.GetBullet();
                left1.transform.up = Quaternion.Euler(new Vector3(0, 0, (360f / count) * i)) * _bulletAgaent.transform.forward;
                left1.transform.position = _bulletAgaent.transform.position;
            }
            LastGenerate = Time.time;
        }

        public override void Hit()
        {
            enemy._EnemyDetail.Injury(GameManager.Instance.MathManager.Damage(enemy._EnemyDetail.enemyAttr, this.bulletAttr));
        }


        public override void JudgeHit()
        {
            var rayHit = Physics2D.Raycast(bulletAgaent.transform.position, bulletAgaent.transform.up, 0.05f);
            if (rayHit.collider?.tag == "Enemy")
            {
                enemy = rayHit.collider.GetComponent<EnemyAgaent>();
                Hit();
                if (--currentPenetrate < 0)
                    Die();
            }
        }

        public override void Move()
        {
            bulletAgaent.transform.Translate(Vector3.up * Time.deltaTime * bulletAttr.MoveSpeed);
        }

        public override IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(bulletAttr.LifeTime);
            Die();
        }

        public override void Die()
        {
            GameManager.Instance.BulletManager.Destroy(bulletAgaent);
        }
    }

}

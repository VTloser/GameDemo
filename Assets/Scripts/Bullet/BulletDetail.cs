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
using DemoGame.Bullet;
using UnityEngine;

namespace DemoGame
{
    
    /// <summary>
    /// 子弹细节描述类
    /// </summary>
    [Serializable]
    public abstract class BulletDetail /*: ICloneable*/
    {
        /// <summary>  移动细节   </summary>
        public abstract void Move(Transform tag = null);

        /// <summary>  子弹初始化  </summary>
        public abstract void Int(BulletAgent agentBullet);

        /// <summary>  判断命中  </summary>
        public abstract void JudgeHit(EnemyAgaent enemyAgent);

        /// <summary>  判断伤害  </summary>
        public abstract void Hit();

        /// <summary>  生命周期倒计时  </summary>
        public abstract IEnumerator LifeTime();

        /// <summary>  子弹销毁  </summary>
        public abstract void Die();

        public abstract BulletDetail Clone();

        public abstract ComputerDate GetData();

        /// <summary>  子弹种类  </summary>
        public BulletType bulletType;

        /// <summary>  子弹基础属性  </summary>
        public BulletAttr bulletAttr;

        /// <summary>  子弹代理  </summary>
        public BulletAgent bulletAgent;

        protected List<EnemyAgaent> HitEnemy;
        
    }
    
    /// <summary>
    /// 火球子弹细节
    /// </summary>
    [Serializable]
    public class FireBallDetail : BulletDetail
    {
        
        private EnemyAgaent enemy;

        private float currentPenetrate;

        public FireBallDetail()
        {
            bulletType = BulletType.FireBall;
            bulletAttr = GameManager.Instance.bulletFactory.GetBulletAttr(bulletType);
        }

        public override void Int(BulletAgent agentBullet)
        {
            currentPenetrate = bulletAttr.Penetrate;
            bulletAgent = agentBullet;

            bulletAgent.Sprite.material = bulletAttr.Material;
            bulletAgent.transform.localScale = bulletAttr.Size;
            HitEnemy = new List<EnemyAgaent>();
        }

        public override void Hit()
        {
            enemy._EnemyDetail.Injury(GameManager.Instance.MathManager.Damage(enemy._EnemyDetail.enemyAttr, this.bulletAttr));
            HitEnemy.Add(enemy);
        }

        public override void JudgeHit(EnemyAgaent enemyAgent)
        {
            enemy = enemyAgent;
            if(!enemy.IsUse) return;
            if (HitEnemy.Contains(enemy)) return;
            Hit();
            if (--currentPenetrate < 0)
                Die();
        }

        public override void Move(Transform tag = null)
        {
            bulletAttr.MoveType.Move(bulletAgent.transform, tag, bulletAttr.MoveSpeed);
        }

        public override IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(bulletAttr.LifeTime);
            Die();
        }
        
        bool isLive = true;
        bool isFollow = true;
        public override void Die()
        {
            HitEnemy.Clear();
            GameManager.Instance.BulletManager.Destroy(bulletAgent);
            isLive = false;
            isFollow = false;
        }

        public override BulletDetail Clone()
        {
            return new FireBallDetail();
        }

        public override ComputerDate GetData()
        {
            return new ComputerDate(bulletAgent.transform.position, bulletAttr.Radius, isLive, isFollow);
            //return new ComputerDate();
        }
    }
}

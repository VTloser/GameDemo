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
using DemoGame.Enemy;
using DemoGame.Manager.Computer;
using UnityEngine;

namespace DemoGame.Bullet
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
        public abstract void Int(BulletAgent agentBullet, BulletAttr bulletAttr);

        /// <summary>  判断命中  </summary>
        public abstract void JudgeHit(EnemyAgaent enemyAgent);

        /// <summary>  判断伤害  </summary>
        public abstract void Hit(EnemyAgaent _enemy);

        /// <summary>  生命周期倒计时  </summary>
        public abstract IEnumerator LifeTime();

        /// <summary>  子弹销毁  </summary>
        public abstract void Die();

        public abstract BulletComputerData GetData();

        /// <summary>  子弹种类  </summary>
        public BulletType bulletType;

        /// <summary>  子弹基础属性  </summary>
        public BulletAttr bulletAttr;

        /// <summary>  子弹代理  </summary>
        public BulletAgent bulletAgent;

        /// <summary>  伤害过的敌人列表  </summary>
        protected List<EnemyAgaent> HitEnemy;

        /// <summary>  当前生存时间  </summary>
        protected float CurrentLifeTime;
    }

    /// <summary>
    /// 火球子弹细节
    /// </summary>
    [Serializable]
    public class FireBallDetail : BulletDetail
    {
        /// <summary>  当前穿透数  </summary>
        private float _currentPenetrate;

        public override void Int(BulletAgent agentBullet, BulletAttr _bulletAttr)
        {
            bulletAgent = agentBullet;
            bulletAttr = _bulletAttr;
            
            _currentPenetrate = bulletAttr.Penetrate;
            bulletAgent.sprite.material = bulletAttr.Material;
            bulletAgent.transform.localScale = bulletAttr.ModelSize;
            HitEnemy = new List<EnemyAgaent>();
            CurrentLifeTime = Time.time;
            HitEnemy.Clear();
        }

        public override void Hit(EnemyAgaent enemy)
        {
            enemy.enemyDetail.Injury(
                GameManager.Instance.MathManager.Damage(enemy.enemyDetail.enemyAttr, bulletAttr));
            HitEnemy.Add(enemy);
        }

        public override void JudgeHit(EnemyAgaent enemyAgent)
        {
            if (!enemyAgent.IsUse || HitEnemy.Contains(enemyAgent)) return;
            Hit(enemyAgent);
            if (--_currentPenetrate < 0)
                Die();
        }

        public override void Move(Transform tag = null)
        {
            bulletAttr.MoveType.Move(bulletAgent.transform, tag, bulletAttr.MoveSpeed, Time.time - CurrentLifeTime);
        }

        public override IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(bulletAttr.LifeTime);
            Die();
        }

        public override void Die()
        {
            GameManager.Instance.BulletManager.Destroy(bulletAgent);
        }

        public override BulletComputerData GetData()
        {
            return new BulletComputerData(bulletAgent.transform.position, bulletAttr.HitRadius, bulletAttr.TrackRadius,
                bulletAgent.Num);
        }
    }
}
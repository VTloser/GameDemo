/*
 * FileName:      EnemyDetail.cs
 * Author:        魏宇辰
 * Date:          2023/07/19 14:27:11
 * Describe:      怪物描述文件
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoGame
{
    public abstract class EnemyDetail
    {
        /// <summary>  移动   </summary>
        public abstract void Move();

        /// <summary>  模型  </summary>
        public abstract string Mode();

        /// <summary>  判断命中  </summary>
        public abstract bool JudgeHit(Transform transform);

        /// <summary>  判断命中  </summary>
        public abstract void Hit();

        /// <summary>  收到伤害  </summary>
        public abstract void Injury(float damage);

        public abstract void Die();

        public abstract void Init(Enemy enemy);

        /// <summary>  敌人种类  </summary>
        public EnemyType enemyType;

        /// <summary>  敌人属性  </summary>
        public EnemyAttr enemyAttr;

        /// <summary>  当前HP  </summary>
        public float CurrentHp;

        /// <summary>  对应实体  </summary>
        public Enemy _Enemy;

    }

    public class DemoEnemyDetail : EnemyDetail
    {
        public DemoEnemyDetail()
        {
            enemyType = EnemyType.None;
            enemyAttr = GameManager.Instance.enemyFactory.GetEnemyAttr(enemyType);
            CurrentHp = enemyAttr.MaxHp;
        }

        public override void Die()
        {
            GameManager.Instance.EnemyManager.Destroy(_Enemy);
        }

        public override void Hit()
        {
            throw new System.NotImplementedException();
        }

        public override void Init(Enemy enemy)
        {
            _Enemy = enemy;
        }

        public override void Injury(float damage)
        {
            CurrentHp -= damage;
            if (CurrentHp <= 0)
            {
                Die();
            }
        }

        public override bool JudgeHit(Transform transform)
        {
            throw new System.NotImplementedException();
        }

        public override string Mode()
        {
            throw new System.NotImplementedException();
        }

        public override void Move()
        {
            if (enemyAttr.Tag != null)
            {
                _Enemy.transform.LookAt(enemyAttr.Tag);
                _Enemy.transform.Translate(Vector3.forward * Time.deltaTime * enemyAttr.MoveSpeed);
            }
        }
    }


}
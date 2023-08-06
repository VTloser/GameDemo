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

        /// <summary>  判断命中  </summary>
        public abstract bool JudgeHit();

        /// <summary>  判断命中  </summary>
        public abstract void Hit();

        /// <summary>  收到伤害  </summary>
        public abstract void Injury(float damage);

        public abstract void Die();

        public abstract void Init(EnemyAgaent enemy);

        public abstract ComputerDate GetData();

        /// <summary>  敌人种类  </summary>
        public EnemyType enemyType;

        /// <summary>  敌人属性  </summary>
        public EnemyAttr enemyAttr;

        /// <summary>  当前HP  </summary>
        public float CurrentHp;

        /// <summary>  对应实体  </summary>
        public EnemyAgaent _Enemy;

    }

    public class DemoEnemyDetail : EnemyDetail
    {
        public DemoEnemyDetail()
        {
            enemyType = EnemyType.None;
            enemyAttr = GameManager.Instance.enemyFactory.GetEnemyAttr(enemyType);
            CurrentHp = enemyAttr.MaxHp;
        }

        bool _ISLive = true;
        bool _ISNoFloow = true;
        public override void Die()
        {
            GameManager.Instance.EnemyManager.Destroy(_Enemy); 
            _ISLive = false;
            _ISNoFloow = false;
        }

        public override ComputerDate GetData()
        {
            return new ComputerDate(_Enemy.transform.position, enemyAttr.Radius, _ISLive, _ISNoFloow);
        }

        public override void Hit()
        {
            throw new System.NotImplementedException();
        }

        public override void Init(EnemyAgaent enemy)
        {
            _Enemy = enemy;

            //for (int i = 0; i < enemy.transform.childCount; i++)
            //{
            //    enemy.transform.GetChild(i).gameObject.SetActive(false);
            //}
            //if (enemy.transform.Find(enemyAttr.ModeName) == null)
            //{
            //    GameManager.Instance.ResourceManager.Load<GameManager>("Enemy/" + (enemyAttr.ModeName, enemy));
            //}
            //enemy.transform.Find(enemyAttr.ModeName).gameObject.SetActive(true);
        }

        public override void Injury(float damage)
        {
            CurrentHp -= damage;
            if (CurrentHp <= 0)
            {
                Die();
            }
        }

        public override bool JudgeHit()
        {
            throw new System.NotImplementedException();
        }


        public override void Move()
        {
            _Enemy.transform.Translate(Vector3.up * Time.deltaTime * enemyAttr.MoveSpeed);

            if (enemyAttr.Tag != null)
            {
                _Enemy.transform.rotation = Quaternion.FromToRotation(Vector3.right, enemyAttr.Tag.position - _Enemy.transform.position);
                _Enemy.transform.Translate(Vector3.right * Time.deltaTime * enemyAttr.MoveSpeed);
            }
        }

    }
}
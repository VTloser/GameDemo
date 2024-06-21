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
    /// <summary>
    /// 怪物细节类
    /// </summary>
    public abstract class EnemyDetail
    {
        /// <summary>  移动   </summary>
        public abstract void Move(Vector2 Dir);

        /// <summary>  判断命中  </summary>
        public abstract bool JudgeHit();
        
        /// <summary>  收到伤害  </summary>
        public abstract void Injury(float damage);
        
        /// <summary>  死亡  </summary>
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

    /// <summary>
    /// Demo敌人细节
    /// </summary>
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
            _ISLive = false;
            _ISNoFloow = false;
            
            GameManager.Instance.EnemyManager.Destroy(_Enemy); 
        }

        public override ComputerDate GetData()
        {
            return new ComputerDate(_Enemy.transform.position, enemyAttr.Radius, _ISLive, _ISNoFloow);
        }
        
        public override void Init(EnemyAgaent enemy)
        {
            _Enemy = enemy;
        }

        public override void Injury(float damage)
        {
            GameManager.Instance.FloatingWordMgr.Damage(_Enemy.transform.position, damage);

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

        private Vector2 _floowDir;
        public override void Move(Vector2 dir)
        {
            if (enemyAttr.Tag is not null)  //enemyAttr.Tag = null
            {

                _floowDir = (enemyAttr.Tag.position - _Enemy.transform.position).normalized;
                _Enemy.transform.Translate((_floowDir + dir * 10) * (Time.deltaTime * enemyAttr.MoveSpeed));

                //_Enemy.transform.rotation = Quaternion.FromToRotation(Vector3.right, enemyAttr.Tag.position - _Enemy.transform.position);
                //_Enemy.transform.Translate((Vector3.right * 0.1f + _Enemy.transform.worldToLocalMatrix.rotation * Dir) * Time.deltaTime * 30 * enemyAttr.MoveSpeed);
            }
            else
            {
                _Enemy.transform.Translate(dir * (Time.deltaTime * 20));
            }
        }
    }
}
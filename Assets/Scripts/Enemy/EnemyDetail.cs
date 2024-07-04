/*
 * FileName:      EnemyDetail.cs
 * Author:        魏宇辰
 * Date:          2023/07/19 14:27:11
 * Describe:      怪物描述文件
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using DemoGame.Manager.Computer;
using UnityEngine;

namespace DemoGame.Enemy
{
    /// <summary>
    /// 怪物细节类
    /// </summary>
    public abstract class EnemyDetail
    {
        /// <summary>  移动   </summary>
        public abstract void Move(Vector2 dir);

        /// <summary>  判断命中 与主角发生碰撞  </summary>
        public abstract bool JudgeHit();
        
        /// <summary>  受到伤害  </summary>
        public abstract void Injury(float damage);

        /// <summary>  攻击  </summary>
        public abstract void Hit( );
        
        /// <summary>  死亡  </summary>
        public abstract void Die();

        /// <summary>  初始化函数  </summary>
        public abstract void Init(EnemyAgaent _enemy, EnemyAttr _enemyAttr);
        
        /// <summary>  获取数据  </summary>
        public abstract EnemyComputerData GetData();
        
        /// <summary>  敌人共享属性  </summary>
        public EnemyAttr enemyAttr;

         /// <summary>  特殊怪属性  </summary>
        public EnemyAttr SpecialAttr;
        
        /// <summary>  当前HP  </summary>
        protected float CurrentHp;

        /// <summary>  对应实体  </summary>
        protected EnemyAgaent enemy;

    }
    
    /// <summary>
    /// 史莱姆
    /// </summary>
    public class SlimeDetail : EnemyDetail
    {
        public override void Hit()
        {
            GameManager.Instance.Player.Injury(
                GameManager.Instance.MathManager.Damage(GameManager.Instance.Player, enemyAttr));
            
        }

        public override void Die()
        {
            GameManager.Instance.EnemyManager.Destroy(enemy); 
        }

        public override EnemyComputerData GetData()
        {
            return new EnemyComputerData(enemy.transform.position, enemyAttr.Radius + (SpecialAttr?.Radius ?? 0),
                enemy.Num);
            //return new EnemyComputerData(enemy.transform.position, enemyAttr.Radius, enemy.Num);
        }

        public override void Init(EnemyAgaent _enemy, EnemyAttr _enemyAttr)
        {
            enemy = _enemy;
            enemyAttr = _enemyAttr;

            CurrentHp = enemyAttr.MaxHp + (SpecialAttr?.MaxHp ?? 0);
            enemy.sprite.material = SpecialAttr?.Material ? SpecialAttr?.Material : enemyAttr.Material;
            enemy.transform.localScale = enemyAttr.Size + (SpecialAttr?.Size ?? Vector2.zero);
            
        }
        
        public override void Injury(float damage)
        {
            GameManager.Instance.FloatingWordMgr.Damage(enemy.transform.position, damage);

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

        private Vector2 _followDir;
        public override void Move(Vector2 dir)
        {
            if (enemyAttr.Tag is not null)  //enemyAttr.Tag = null
            {
                _followDir = (enemyAttr.Tag.position - enemy.transform.position).normalized;
                enemy.sprite.flipX = _followDir.x > 0;
                enemy.transform.Translate((_followDir + dir * 50) *
                                          (Time.deltaTime * (enemyAttr.MoveSpeed + (SpecialAttr?.MoveSpeed ?? 0))));
                
            }
            else
            {
                enemy.transform.Translate(dir * (Time.deltaTime * 50));
            }
        }
    }

    /// <summary>
    /// 红色史莱姆
    /// </summary>
    public class RedSlime : SlimeDetail
    {
        
    }

    /// <summary>
    /// 绿色史莱姆
    /// </summary>
    public class GreenSlime : SlimeDetail
    {
        
    }
}
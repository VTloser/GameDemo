/*
 * FileName:      EnemyDetail.cs
 * Author:        魏宇辰
 * Date:          2023/07/19 14:27:11
 * Describe:      怪物描述文件
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoGame
{
    public abstract class EnemyDetail
    {
        /// <summary>  移动   </summary>
        public abstract void Move(Enemy enemy);

        /// <summary>  敌人生成  </summary>
        public abstract void Generate(Vector3 Pos, Vector3 ForWard, EnemyDetail enemyDetail);

        /// <summary>  模型  </summary>
        public abstract string Mode();

        /// <summary>  判断命中  </summary>
        public abstract bool JudgeHit(Transform transform);

        /// <summary>  判断命中  </summary>
        public abstract void Hit();

        /// <summary>  子弹种类  </summary>
        public EnemyType enemyType;

        /// <summary>  子弹基础属性  </summary>
        public EnemyAttr enemyAttr;

    }

    public class DemoEnemyDetail : EnemyDetail
    {
        public override void Generate(Vector3 Pos, Vector3 ForWard, EnemyDetail enemyDetail)
        {
            throw new System.NotImplementedException();
        }

        public override void Hit()
        {
            throw new System.NotImplementedException();
        }

        public override bool JudgeHit(Transform transform)
        {
            throw new System.NotImplementedException();
        }

        public override string Mode()
        {
            throw new System.NotImplementedException();
        }

        public override void Move(Enemy enemy)
        {
            
        }
    }


}
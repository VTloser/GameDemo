/*
 * FileName:      DemoPropsBase.cs
 * Author:        摩诘创新
 * Date:          2023/07/24 09:59:44
 * Describe:      示例道具
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine;


namespace DemoGame
{
    public class DemoPropsBase : PropsBase
    {
        public override void Get()
        {
            //加敌人血
            foreach (var item in GameManager.Instance.enemyFactory.EnemyAttrDB.Values)
            {
                item.MaxHp += 5;
            }
            
            //加子弹攻击力 与穿透
            foreach (var item in GameManager.Instance.bulletFactory.bulletAttrDB.Values)
            {
                item.Penetrate += 5;
                item.Damage += 5;
                item.MoveSpeed += 5;
            }
            
            //加额外一条弹道
            foreach (var item in GameManager.Instance.BulletManager.Generates.Values)
            {
                item.Count += 1;
            }
            
            //给子弹添加追踪功能
            //子弹变大
            //
        }


        public override void Dis()
        {
            foreach (var item in GameManager.Instance.enemyFactory.EnemyAttrDB.Values)
            {
                item.MaxHp -= 5;
            }

            //加子弹攻击力 与穿透
            foreach (var item in GameManager.Instance.bulletFactory.bulletAttrDB.Values)
            {
                item.Penetrate -= 5;
            }

            foreach (var item in GameManager.Instance.BulletManager.Generates.Values)
            {
                item.Count -= 1;
            }
        }
    }
}

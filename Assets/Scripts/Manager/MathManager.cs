/*
 * FileName:      MathManager.cs
 * Author:        摩诘创新
 * Date:          2023/07/20 13:16:27
 * Describe:      伤害计算模块
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoGame
{
    public class MathManager
    {
        public float Damage(EnemyAttr enemy, BulletAttr bullet)
        {
            float Demage = 0;

            //计算普通伤害
            Demage += enemy.Damage;
            //计算暴击伤害
            Demage += Random.Range(0, 101) > bullet.CritRate ? bullet.Damage * bullet.CritDamage / 100 : 0;

            return Demage;
        }
    }
}

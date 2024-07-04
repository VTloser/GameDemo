/*
 * FileName:      DemoPropsBase.cs
 * Author:        摩诘创新
 * Date:          2023/07/24 09:59:44
 * Describe:      示例道具
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using DemoGame.Bullet;
using DemoGame.Enemy;
using UnityEngine;

namespace DemoGame.Props
{
    /// <summary>
    /// 加子弹攻击力与穿透
    /// </summary>
    public class Props_1001 : PropsBase
    {
        public override string Name { get; } = "Props_1001";
        public override float ID { get; } = 1001;
        public override Rarity rarity { get; } = Rarity.N;

        public override void Get()
        {
            foreach (var item in GameManager.Instance.bulletFactory.bulletAttrDB.Values)
            {
                item.Penetrate += 1;
                item.Damage += 1;
                item.MoveSpeed += 1;
            }
        }

        public override void Dis()
        {
            foreach (var item in GameManager.Instance.bulletFactory.bulletAttrDB.Values)
            {
                item.Penetrate -= 1;
                item.Damage -= 1;
                item.MoveSpeed -= 1;
            }
        }
    }


    public class Props_1002 : PropsBase
    {
        public override string Name { get; } = "Props_1002";
        public override float ID { get; } = 1002;
        public override Rarity rarity { get; } = Rarity.N;

        public override void Get()
        {
            //加额外两个条弹道
            foreach (var item in GameManager.Instance.BulletManager.Generates.Values)
            {
                item.Count += 2;
            }
        }

        public override void Dis()
        {
            foreach (var item in GameManager.Instance.BulletManager.Generates.Values)
            {
                item.Count -= 2;
            }
        }
    }

    /// <summary>
    /// 增加敌人最大HP上限
    /// </summary>
    public class Props_1003 : PropsBase
    {
        public override string Name { get; } = "Props_1003";
        public override float ID { get; } = 1003;
        public override Rarity rarity { get; } = Rarity.N;

        public override void Get()
        {
            foreach (var item in GameManager.Instance.enemyFactory.EnemyAttrDB.Values)
            {
                item.MaxHp += 5;
            }
        }

        public override void Dis()
        {
            foreach (var item in GameManager.Instance.enemyFactory.EnemyAttrDB.Values)
            {
                item.MaxHp -= 5;
            }
        }
    }

    /// <summary>
    /// 使你的所有的子弹能够跟踪敌人，如果已经可以跟踪则略微提升伤害
    /// </summary>
    public class Props_1004 : PropsBase
    {
        public override string Name { get; } = "Props_1004";
        public override float ID { get; } = 1004;
        public override Rarity rarity { get; } = Rarity.N;

        private int i;
        private bool[] _originalTrack;

        public override void Get()
        {
            _originalTrack = new bool[GameManager.Instance.bulletFactory.bulletAttrDB.Values.Count];

            i = 0;
            foreach (var item in GameManager.Instance.bulletFactory.bulletAttrDB.Values)
            {
                _originalTrack[i] = item.TrackRadius == 0;

                if (_originalTrack[i])
                    item.TrackRadius = 5;
                else
                    item.Damage += 1;
                i++;
            }
        }

        public override void Dis()
        {
            i = 0;
            foreach (var item in GameManager.Instance.bulletFactory.bulletAttrDB.Values)
            {
                if (_originalTrack[i])
                    item.TrackRadius = 0;
                else
                    item.Damage -= 1;
                i++;
            }
        }
    }

    /// <summary>
    ///  让你的子弹看起来巨大
    /// </summary>
    public class Props_1005 : PropsBase
    {
        public override string Name { get; } = "Props_1005";
        public override float ID { get; } = 1005;
        public override Rarity rarity { get; } = Rarity.N;

        public override void Get()
        {
            foreach (var item in GameManager.Instance.bulletFactory.bulletAttrDB.Values)
            {
                item.ModelSize *= 3;
            }
        }

        public override void Dis()
        {
            foreach (var item in GameManager.Instance.bulletFactory.bulletAttrDB.Values)
            {
                item.ModelSize /= 3;
            }
        }
    }


    /// <summary>
    /// 使一个怪变成精英怪 
    /// </summary>
    public class Props_1006 : PropsBase
    {
        public override string Name { get; } = "Props_1006";
        public override float ID { get; } = 1006;
        public override Rarity rarity { get; } = Rarity.N;

        public override void Get()
        {
            EnemyAgaent item = null;

            while (item is null)
            {
                item = GameManager.Instance.EnemyManager.EnemyPool.ActivateItems[
                    Random.Range(0, GameManager.Instance.EnemyManager.EnemyPool.ActiveCount)];
                if (item.enemyDetail.SpecialAttr != null)
                    item = null;
            }

            EnemyAttr bossAttr = new EnemyAttr(2, 2, 1f, 0, 1, "DemoEnemy", 1f, 0.2f,
                null, new Vector2(2, 2));

            item.Mutations(bossAttr);
        }

        public override void Dis()
        {
            foreach (var item in GameManager.Instance.bulletFactory.bulletAttrDB.Values)
            {
                item.ModelSize /= 5;
            }
        }
    }
    
    /// <summary>
    /// 切换子弹移动类型
    /// </summary>
    public class Props_1007 : PropsBase
    {
        public override string Name { get; } = "Props_1007";
        public override float ID { get; } = 1007;
        public override Rarity rarity { get; } = Rarity.N;

        public override void Get()
        {
            GameManager.Instance.bulletFactory.bulletAttrDB[BulletType.FireBall].MoveType = new TrackingMove();
        }

        public override void Dis()
        {

        }
    }
}
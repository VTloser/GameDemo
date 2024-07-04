/*
 * FileName:      TestHelper.cs
 * Author:        摩诘创新
 * Date:          2023/07/19 16:58:25
 * Describe:      测试助手
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System;
using DemoGame.Bullet;
using DemoGame.Enemy;
using DemoGame.Props;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR

namespace DemoGame.Editor
{
    public class TestHelper : MonoBehaviour
    {
        [HorizontalGroup("AddEnemy")] public int EnemyNum;

        [Button(ButtonSizes.Medium), HorizontalGroup("AddEnemy"),]
        private void 圆环()
        {
            float outside = 30;
            float isside = 10;

            for (int i = 0; i < EnemyNum; i++)
            {
                var t = Random.Range(0, 1f) < 0.5f
                    ? GameManager.Instance.EnemyManager.GetEnemy(EnemyType.RedSlime)
                    : GameManager.Instance.EnemyManager.GetEnemy(EnemyType.GreenSlime);

                Vector2 random = Random.insideUnitSphere * outside; //外圈
                if (random.magnitude < isside)
                {
                    random += random.normalized * isside;
                }

                t.transform.position = random;
            }
        }

        [Button(ButtonSizes.Medium), HorizontalGroup("AddEnemy"),]
        private void 圆形()
        {
            for (int i = 0; i < EnemyNum; i++)
            {
                var t = GameManager.Instance.EnemyManager.GetEnemy(EnemyType.GreenSlime);
                Vector3 random = Random.insideUnitSphere * 15;
                random.z = 0;
                t.transform.position = random;
            }
        }


        [HorizontalGroup("AddBullet")] public int BulletNum;

        [Button(ButtonSizes.Medium), HorizontalGroup("AddBullet")]
        private void 圆内()
        {
            for (int i = 0; i < BulletNum; i++)
            {
                var t = GameManager.Instance.BulletManager.GetBullet(BulletType.FireBall);
                Vector3 random = Random.insideUnitSphere * 5;
                random.z = 0;
                var transform1 = t.transform;
                transform1.up = random;
                transform1.position = random;
            }
        }

        [Button(ButtonSizes.Medium)]
        private void 添加玩家()
        {
            Instantiate(GameManager.Instance.ResourceManager.Load<Player>(Path: "Player"));
        }

        int Count0 = 0;
        [Button(ButtonSizes.Medium), HorizontalGroup("添加子弹")]
        private void 添加方向子弹()
        {
            BulletDetail bullet = new FireBallDetail();
            GameManager.Instance.BulletManager.AddBulletType(
                GameManager.Instance.bulletFactory.GetDetail(BulletType.FireBall),
                new DirGenerate(BulletType.FireBall, 200 * Count0++));
        }


        int Count1;
        [Button(ButtonSizes.Medium), HorizontalGroup("添加子弹")]
        private void 添加圆环子弹()
        {
            GameManager.Instance.BulletManager.AddBulletType(
                GameManager.Instance.bulletFactory.GetDetail(BulletType.FireBall),
                new CircleGenerate(BulletType.FireBall, 200 * Count1++));
        }

        int Count2;
        [Button(ButtonSizes.Medium), HorizontalGroup("添加子弹")]
        private void 添加方向圆环子弹()
        {
            GameManager.Instance.BulletManager.AddBulletType(
                GameManager.Instance.bulletFactory.GetDetail(BulletType.FireBall),
                new CircleDirGenerate(BulletType.FireBall, 200 * Count2++,
                    2f));
        }
        
        
        [Button(ButtonSizes.Medium)]
        private void 添加Demo道具()
        {
            PropsBase demoPropsBase = new Props_1006();
            demoPropsBase.Get();
        }
    }
}
#endif
/*
 * FileName:      TestHelper.cs
 * Author:        摩诘创新
 * Date:          2023/07/19 16:58:25
 * Describe:      测试助手
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR


namespace DemoGame
{
    public class TestHelper : MonoBehaviour
    {


        [HorizontalGroup("AddEnemy")]
        public int EnemyNum;


        [Button(ButtonSizes.Medium), HorizontalGroup("AddEnemy"),]
        private void 圆环()
        {
            float outside = 30;
            float isside = 10;

            for (int i = 0; i < EnemyNum; i++)
            {
                var t = GameManager.Instance.EnemyManager.GetEnemy(new DemoEnemyDetail());
                Vector2 random = Random.insideUnitSphere * outside;  //外圈
                if (random.magnitude < isside)
                {
                    random += random.normalized * isside;
                }
                //random.z = 0;
                t.transform.position = random;
            }
        }
        [Button(ButtonSizes.Medium), HorizontalGroup("AddEnemy"),]
        private void 圆形()
        {
            for (int i = 0; i < EnemyNum; i++)
            {
                var t = GameManager.Instance.EnemyManager.GetEnemy(new DemoEnemyDetail());
                Vector3 random = Random.insideUnitSphere * 20;
                random.z = 0;
                t.transform.position = random;
            }
        }


        [HorizontalGroup("AddBullet")]
        public int BulletNum;
        [Button(ButtonSizes.Medium), HorizontalGroup("AddBullet")]
        private void 圆内()
        {
            for (int i = 0; i < BulletNum; i++)
            {
                var t = GameManager.Instance.BulletManager.GetBullet(new FireBallDetail());
                Vector3 random = Random.insideUnitSphere * 5;
                random.z = 0;
                t.transform.up = random;
                t.transform.position = random;
            }
        }

        [Button(ButtonSizes.Medium)]
        private void 添加玩家()
        {
            Instantiate(GameManager.Instance.ResourceManager.Load<Player>(Path: "Player"));
        }

        int Count0;
        [Button(ButtonSizes.Medium), HorizontalGroup("添加子弹")]

        private void 添加方向子弹()
        {
            BulletDetail bullet = new FireBallDetail();
            GameManager.Instance.BulletManager.AddBulletType(bullet, new DirGenerate(bullet, 500 * Count0++));
        }


        int Count1;
        [Button(ButtonSizes.Medium), HorizontalGroup("添加子弹")]
        private void 添加圆环子弹()
        {
            BulletDetail bullet = new FireBallDetail();
            GameManager.Instance.BulletManager.AddBulletType(bullet, new CircleGenerate(bullet, 500 * Count1++));
        }

        int Count2;
        [Button(ButtonSizes.Medium), HorizontalGroup("添加子弹")]
        private void 添加方向圆环子弹()
        {
            BulletDetail bullet = new FireBallDetail();
            GameManager.Instance.BulletManager.AddBulletType(bullet, new CircleDirGenerate(bullet, 500 * Count2++, 2f));
        }


        [Button(ButtonSizes.Medium)]
        private void 添加Demo道具()
        {
            DemoPropsBase demoPropsBase = new DemoPropsBase();
            demoPropsBase.Get();
        }

    }
}
#endif
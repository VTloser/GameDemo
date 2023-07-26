/*
 * FileName:      TestHelper.cs
 * Author:        摩诘创新
 * Date:          2023/07/19 16:58:25
 * Describe:      测试助手
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DemoGame
{
    public class TestHelper : MonoBehaviour
    {
        public int EnemyNum;

        [Button(ButtonSizes.Medium)]
        private void AddEnemy()
        {
            for (int i = 0; i < EnemyNum; i++)
            {
                var t = GameManager.Instance.EnemyManager.GetEnemy(new DemoEnemyDetail());
                Vector3 random = Random.insideUnitSphere * 50;
                random.z = 0;
                t.transform.position = random;
            }
        }
        public int BulletNum;
        [Button(ButtonSizes.Medium)]
        private void AddBullet()
        {
            for (int i = 0; i < BulletNum; i++)
            {
                var t = GameManager.Instance.BulletManager.GetBullet(new FireBallDetail());
                Vector3 random = Random.insideUnitSphere * 5;
                random.z = 0;
                t.transform.position = random;
            }
        }



        [Button(ButtonSizes.Medium)]
        private void AddPlayer()
        {
            Instantiate(GameManager.Instance.ResourceManager.Load<PlayerControl>(Path: "Player"));
        }

        int Count0;        
        [Button(ButtonSizes.Medium)]

        private void AddDirGenerateBullet()
        {
            BulletDetail bullet = new FireBallDetail();
            GameManager.Instance.BulletManager.AddBulletType(bullet, new DirGenerate(bullet, 500 * Count0++));
        }


        int Count1;
        [Button(ButtonSizes.Medium)]
        private void AddCircleGenerateBullet()
        {
            BulletDetail bullet = new FireBallDetail();
            GameManager.Instance.BulletManager.AddBulletType(bullet, new CircleGenerate(bullet, 500 * Count1++));
        }

        int Count2;
        [Button(ButtonSizes.Medium)]
        private void AddCircleDirGenerateBullet()
        {
            BulletDetail bullet = new FireBallDetail();
            GameManager.Instance.BulletManager.AddBulletType(bullet, new CircleDirGenerate(bullet, 500 * Count2++, 2f));
        }


        [Button(ButtonSizes.Medium)]
        private void GetDemoProps()
        {
            DemoPropsBase demoPropsBase = new DemoPropsBase();
            demoPropsBase.Get();
        }

    }
}

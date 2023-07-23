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
using UnityEditor;
using UnityEngine;

namespace DemoGame
{
    public class TestHelper : MonoBehaviour
    {
        public float EnemyNum;

        [Button(ButtonSizes.Medium)]
        private void AddEnemy()
        {
            for (int i = 0; i < EnemyNum; i++)
            {
                var t = GameManager.Instance.EnemyManager.GetEnemy(new DemoEnemyDetail());
                Vector3 random = Random.insideUnitSphere * 5;
                random.z = 0;
                t.transform.position = random;
            }
        }

        [Button(ButtonSizes.Medium)]
        private void AddPlayer()
        {
            Instantiate(GameManager.Instance.ResourceManager.Load<PlayerControl>("Player"));
        }

        [Button(ButtonSizes.Medium)]
        private void AddBullet()
        {
            BulletDetail bullet = new SuperBulletDetail();
            GameManager.Instance.BulletManager.AddBullet(bullet, new CircleDirGenerate(bullet,1));
        }


    }
}

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
                var t = GameManager.EnemyManager.GetEnemy();
                Vector3 random = Random.insideUnitSphere * 5;
                random.y = 0;
                t.transform.position = random;
            }
        }
    }
}

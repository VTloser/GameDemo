/*
 * FileName:      LevelManager.cs
 * Author:        魏宇辰
 * Date:          2023/07/19 16:24:23
 * Describe:      关卡生成器
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */


using Codice.Client.BaseCommands.BranchExplorer;
using log4net.Core;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

namespace DemoGame
{
    public class LevelManager
    {
        public List<LevelItem> Levels;

        private LevelItem CurrentLevel;


        public void Init()
        {
            Levels = new List<LevelItem>();
            LevelItem item = new LevelItem();
            item.GenerateSpeed = 10;
            Levels.Add(item);
        }

        public void BeginGame()
        {
            CurrentLevel = Levels[0];

            Generator();
        }

        CancellationTokenSource cts = new CancellationTokenSource();
        public async void Generator()
        {
            while (!cts.IsCancellationRequested)
            {
                float outside = 30;
                float isside = 10;

                for (int i = 0; i < CurrentLevel.GenerateSpeed; i++)
                {
                    var t = GameManager.Instance.EnemyManager.GetEnemy(new DemoEnemyDetail());
                    Vector2 random = Random.insideUnitSphere * outside;  //外圈
                    if (random.magnitude < isside)
                    {
                        random += random.normalized * isside;
                    }
                    t.transform.position = random;
                }
                await Task.Delay(1000);
            }
        }


        public void Stop()
        {
            cts.Cancel();
        }

    }
}

public class LevelItem
{

    /// <summary>   每秒生成速度     </summary>
    public int GenerateSpeed;
    /// <summary>    关卡开始回调     </summary>
    public UnityAction LevelBegin;
    /// <summary>    关卡成功回调     </summary>
    public UnityAction LevelSucceed;
    /// <summary>    生成失败     </summary>
    public UnityAction LevelFailed;

}
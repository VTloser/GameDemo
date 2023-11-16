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
        /// <summary>   总体关卡   </summary>
        private List<LevelItem> Levels;

        /// <summary>   当前关卡   </summary>
        private LevelItem CurrentLevel;
        
        /// <summary>   当前关卡序号   </summary>
        private int CuttentLevelCount = 0;

        /// <summary>   关卡失败数量    </summary>
        public int LevelFailedCount = 500;
        
        /// <summary>   是否产生敌人    </summary>
        private bool canGenerator = false;
        
        
        public void Init()
        {
            Levels = new List<LevelItem>();

            for (int i = 1; i <= 5; i++)
            {
                LevelItem item = new LevelItem(10 * i, 10 * i / 2f);
                Levels.Add(item);
            }
        }

        private bool waitNextLeave;
        private async void LevelSuccess()
        {
            if (!waitNextLeave)
            {
                waitNextLeave = true;
                //TODO UITip
                Debug.Log("关卡成功");
            
                canGenerator = false;
                EventCenter.Broadcast(GameEvent.LevelSuccess);
            
                await Task.Delay(3000);
                NextLevel();
            }
        }

        private void LevelFailde()
        {
            //TODO UITip
            Debug.Log("关卡失败");
            canGenerator = false;
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        public void GameEnd()
        {
            cts.Cancel();
        }
        
        public void LevelManagerUpdate(int currenEnenmyCount)
        {
            FailedJudgment(currenEnenmyCount);
            SuccessedJudgment();
        }
        
        /// <summary>
        /// 关卡失败监测
        /// </summary>
        /// <param name="currenEnenmyCount"></param>
        private void FailedJudgment(int currenEnenmyCount)
        {
            if (currenEnenmyCount >= LevelFailedCount)
            {
                LevelFailde();
                EventCenter.Broadcast(GameEvent.GameFailde);
            }
        }

        /// <summary>
        /// 关卡成功监测
        /// </summary>
        /// <param name="leaveTime"></param>
        private void SuccessedJudgment()
        {
            leaveTime += Time.deltaTime;
            if (leaveTime >= CurrentLevel.LevelLeaveTime)
            {
                if (CuttentLevelCount == Levels.Count - 1)
                {
                    EventCenter.Broadcast(GameEvent.GameSuccess);
                }
                else
                {
                    LevelSuccess();
                }
            }
        }

        private float leaveTime = 0;

        /// <summary>
        /// 下一关卡
        /// </summary>
        public void NextLevel()
        {            
            canGenerator = true;
            leaveTime = 0;
            waitNextLeave = false;

            CuttentLevelCount++;
            CurrentLevel = Levels[CuttentLevelCount];

            if (CuttentLevelCount == 0)
                Generator();
            
            //TODO UITip
            Debug.Log("下一关开始");
        }
        
        CancellationTokenSource cts = new CancellationTokenSource();

        public async void Generator()
        {
            while (true)
            {
                if (canGenerator)
                {
                    float outside = 30;
                    float inside = 10;

                    for (int i = 0; i < CurrentLevel.GenerateSpeed; i++)
                    {
                        var t = GameManager.Instance.EnemyManager.GetEnemy(new DemoEnemyDetail());
                        Vector2 random = Random.insideUnitSphere * outside; //外圈
                        if (random.magnitude < inside)
                        {
                            random += random.normalized * inside;
                        }
                        t.transform.position = random;
                    }
                }
                await Task.Delay(1000);
            }
        }

        public void GameStop()
        {
            canGenerator = false;
        }
    }
}


public class LevelItem
{
    /// <summary>   每秒生成速度     </summary>
    public int GenerateSpeed;

    // /// <summary>    关卡开始回调     </summary>
    // public UnityAction LevelBegin;
    // /// <summary>    关卡成功回调     </summary>
    // public UnityAction LevelSucceed;
    // /// <summary>    关卡失败回调     </summary>
    // public UnityAction LevelFailed;
    
    /// <summary>   关卡生存时间     </summary>
    public float LevelLeaveTime;
    
    public LevelItem(int generateSpeed, float levelLeaveTime)
    {
        GenerateSpeed = generateSpeed;
        LevelLeaveTime = levelLeaveTime;
    }
}
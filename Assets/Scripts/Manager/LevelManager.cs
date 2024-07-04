/*
 * FileName:      LevelManager.cs
 * Author:        魏宇辰
 * Date:          2023/07/19 16:24:23
 * Describe:      关卡生成器
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */


using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DemoGame.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DemoGame
{
    /// <summary>
    /// 关卡管理器
    /// </summary>
    public class LevelManager
    {
        
        /// <summary>   关卡失败数量    </summary>
        public int LevelFailedCount = 500;
        
        /// <summary>   怪物刷新地点    </summary>
        public Transform[] GeneratorPosition;
        
        
        /// <summary>   总体关卡列表   </summary>
        private List<LevelItem> Levels;

        /// <summary>   当前关卡   </summary>
        private LevelItem CurrentLevel;
        
        /// <summary>   当前关卡序号   </summary>
        private int CuttentLevelCount = 0;
        
        
        // private bool canGenerator = false;
        
        /// <summary>   取消产生敌人    </summary>
        CancellationTokenSource cts = new CancellationTokenSource();
        
        /// <summary>   关卡运行时间    </summary>
        private float leaveTime = 0;
        
        /// <summary>  等待关卡加载    </summary>
        private bool waitNextLeave;
        
        /// <summary>  当前怪物总数    </summary>
        private int _currentEnemyCount;
        
        /// <summary>  当前关卡预期加载怪物总数    </summary>
        private int _currentLevelCount;
        
        public void Init()
        {
            Levels = new List<LevelItem>();

            for (int i = 1; i <= 5; i++)
            {
                LevelItem item = new LevelItem(i * 100, 10 * i, 10 * i / 2f);
                Levels.Add(item);
            }
        }
        
        /// <summary>
        /// 关卡成功
        /// </summary>
        private async void LevelSuccess()
        {
            if (!waitNextLeave)
            {
                waitNextLeave = true;
                //TODO UITip
                Debug.Log("关卡成功");
                
                EventCenter.Broadcast(GameEvent.LevelSuccess);
            
                await Task.Delay(TimeSpan.FromSeconds(5));
                NextLevel();
            }
        }
        
        
        /// <summary>
        /// 关卡失败
        /// </summary>
        private void LevelFailde()
        {
            cts.Cancel();
            
            //TODO UITip
            Debug.Log("关卡失败");
            
            EventCenter.Broadcast(GameEvent.GameFailde);
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        public void GameEnd()
        {
            cts.Cancel();
        }
        
        
        /// <summary>
        /// 关卡管理 Update函数
        /// </summary>
        /// <param name="currenEnenmyCount"></param>
        public void LevelManagerUpdate(int currenEnenmyCount)
        {
            _currentEnemyCount = currenEnenmyCount;
            FailedJudgment(currenEnenmyCount);
            SuccessedJudgment();
        }
        
        /// <summary>
        /// 关卡失败检测
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
        /// 关卡成功检测
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
        
        /// <summary>
        /// 下一关卡
        /// </summary>
        public void NextLevel()
        {
            leaveTime = 0;
            waitNextLeave = false;

            CuttentLevelCount++;
            CurrentLevel = Levels[CuttentLevelCount];
            _currentLevelCount = CurrentLevel.GenerateNum;

            Generator();

            //TODO UITip
            Debug.Log("下一关开始");
        }


        /// <summary>
        /// 怪物生成
        /// </summary>
        public async void Generator()
        {
            while (_currentLevelCount >= _currentEnemyCount && !cts.IsCancellationRequested)
            {
                float outside = 30;
                float inside = 10;

                for (int i = 0; i < CurrentLevel.GenerateSpeed; i++)
                {
                    var t = GameManager.Instance.EnemyManager.GetEnemy(EnemyType.GreenSlime);
                    Vector2 random = Random.insideUnitSphere * outside; //外圈
                    if (random.magnitude < inside)
                    {
                        random += random.normalized * inside;
                    }

                    t.transform.position = random;
                }
                
                await Task.Delay(1000);
            }
        }

        public void GameStop()
        {
           
        }
    }
}


public class LevelItem
{
    
    /// <summary>   生成怪物总数     </summary>
    public int GenerateNum;
    
    /// <summary>   每秒生成速度     </summary>
    public int GenerateSpeed;
    
    /// <summary>   关卡存在时间     </summary>
    public float LevelLeaveTime;

    public LevelItem(int generateNum, int generateSpeed, float levelLeaveTime)
    {
        GenerateSpeed = generateSpeed;
        LevelLeaveTime = levelLeaveTime;
        GenerateNum = generateNum;
    }
    
}
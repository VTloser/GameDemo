using System;
using System.Collections.Generic;
using System.Resources;
using DemoGame.Bullet;
using UnityEditor.VersionControl;
using UnityEngine.UIElements;
using UnityEngine;
using Task = System.Threading.Tasks.Task;


namespace DemoGame
{
    public class GameManager : MonoBehaviour
    {
        /// <summary> GameManager 单例 </summary>
        public static GameManager Instance;
        
        /// <summary> GameManager 单例 </summary>
        public List<IMiniMap> MiniMapTail = new List<IMiniMap>();

        /// <summary> 玩家主角 </summary>
        private Player player;

        public Player Player { get => player; set { player = value; enemyFactory.ChangePlayer(player.transform); } }

        
        /// <summary> 事件管理器 </summary>
        public EventManager EventManager;
        
        /// <summary> 子弹属性工厂 </summary>
        public BulletFactory bulletFactory;
        
        /// <summary> 怪物属性工厂 </summary>
        public EnemyFactory enemyFactory;
        
        /// <summary> 输入管理器 </summary>
        public InputManager inputManager;
        
        /// <summary> 资源加载管理器 </summary>
        public ResourceManager ResourceManager;

        /// <summary> 子弹管理 </summary>
        public BulletManager BulletManager;
        
        /// <summary> 怪物管理 </summary>
        public EnemyManager EnemyManager;

        /// <summary> 伤害计算模块 </summary>
        public MathManager MathManager;
        
        /// <summary> 关卡管理 </summary>
        public LevelManager LevelManager;
        
        /// <summary> 飘字管理 </summary>
        public FloatingWordManager FloatingWordMgr;
        public Transform floatingWordFather;

        
        private void Awake()
        {
            Instance = this;
            ResourceManager = new ResourceManager();
            MathManager     = new MathManager();
            BulletManager   = new BulletManager();  BulletManager.Init();
            EnemyManager    = new EnemyManager();   EnemyManager.Init();
            inputManager    = new PCInputManager();
            bulletFactory   = new BulletFactory();
            enemyFactory    = new EnemyFactory();
            LevelManager    = new LevelManager();  LevelManager.Init();
            EventManager    = new EventManager();  EventManager.Init(this);
            FloatingWordMgr = new FloatingWordManager(); FloatingWordMgr.Init(floatingWordFather, Camera.main);
        }

        private async void  Start()
        {
            //await Task.Delay(5000); //等待系统卡顿延迟
            
            //Instantiate(GameManager.Instance.ResourceManager.Load<Player>(Path: "Player"));
            //LevelManager.NextLevel();
        }
        
        private void Update()
        {
            //LevelManager.LevelManagerUpdate(EnemyManager.EnemyPool.ActiveCount);
        }


        private void OnApplicationQuit()
        {
            EventCenter.Broadcast(GameEvent.GamePause);
        }

    }
}
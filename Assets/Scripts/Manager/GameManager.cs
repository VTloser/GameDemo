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
        public static GameManager Instance;

        public List<IMiniMap> MiniMapTail = new List<IMiniMap>();

        private Player player;

        public Player Player { get => player; set { player = value; enemyFactory.ChangePlayer(player.transform); } }

        public EventManager EventManager;

        public BulletFactory bulletFactory;

        public EnemyFactory enemyFactory;

        public InputManager inputManager;

        public ResourceManager ResourceManager;

        public BulletManager BulletManager;

        public EnemyManager EnemyManager;

        public MathManager MathManager;

        public LevelManager LevelManager;

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
            
            Instantiate(GameManager.Instance.ResourceManager.Load<Player>(Path: "Player"));
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
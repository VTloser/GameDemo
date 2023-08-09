using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.UIElements;

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

            EventManager    = new EventManager();  EventManager.Init();
        }

        private void Start()
        {
            //EventCenter.Broadcast(GameEvent.GameBegin);
        }

        private void OnApplicationQuit()
        {
            EventCenter.Broadcast(GameEvent.Stop);
        }

    }
}
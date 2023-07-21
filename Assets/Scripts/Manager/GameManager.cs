using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DemoGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public List<MiniMap> MiniMapTail = new List<MiniMap>();

        private PlayerControl player;

        public PlayerControl Player { get => player; set { player = value; enemyFactory.ChangePlayer(player.transform); } }

        public BulletFactory bulletFactory = new BulletFactory();

        public EnemyFactory enemyFactory = new EnemyFactory();

        public InputManager inputManager;

        private void Awake()
        {
            Instance = this;

            inputManager = new PCInputManager();
        }


        private ResourceManager resourceManager;
        public ResourceManager ResourceManager
        {
            get
            {
                if (resourceManager == null)
                    resourceManager = new ResourceManager();
                return resourceManager;
            }
        }

        private BulletManager bulletManager;
        public BulletManager BulletManager
        {
            get
            {
                if (bulletManager == null)
                {
                    bulletManager = new BulletManager();
                    bulletManager.Init();
                }
                return bulletManager;
            }
        }


        private EnemyManager enemyManager;
        public EnemyManager EnemyManager
        {
            get
            {
                if (enemyManager == null)
                {
                    enemyManager = new EnemyManager();
                    enemyManager.Init();
                }
                return enemyManager;
            }
        }

        private MathManager mathManager;
        public MathManager MathManager
        {
            get
            {
                if (mathManager == null)
                {
                    mathManager = new MathManager();
                }
                return mathManager;
            }
        }



    }
}
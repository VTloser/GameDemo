using System.Collections.Generic;
namespace DemoGame
{
    public class GameManager
    {
        public List<MiniMap> MiniMapTail = new List<MiniMap>();

        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameManager();
                return instance;
            }
        }

        private static ResourceManager resourceManager;
        public static ResourceManager ResourceManager
        {
            get
            {
                if (resourceManager == null)
                    resourceManager = new ResourceManager();
                return resourceManager;
            }
        }

        private static BulletManager bulletManager;
        public static BulletManager BulletManager
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


        private static EnemyManager enemyManager;
        public static EnemyManager EnemyManager
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


        public static BulletFactory bulletFactory = new BulletFactory();

        public static EnemyFactory enemyFactory = new EnemyFactory();

    }
}
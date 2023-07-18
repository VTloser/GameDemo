using System.Collections.Generic;
namespace DemoGame
{
    public class GameManager
    {
        public List<Entity> Entities = new List<Entity>();

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

        public static BulletFactory bulletFactory = new BulletFactory();
    }
}
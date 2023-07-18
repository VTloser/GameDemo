using UnityEngine;
using UnityEngine.UI;

namespace DemoGame
{
    public class Entity : MonoBehaviour
    {
        public Type type = Type.Entity;

        private void OnEnable()
        {
            GameManager.Instance.Entities.Add(this);
        }

        private void OnDisable()
        {
            GameManager.Instance.Entities.Remove(this);
        }
    }

    public interface Injured
    {
        public void Injured();
    }

    public interface Attack
    {
        public void Attack();
    }

    public interface Skill
    {
        public void Skill();
    }

    public interface MiniMap
    {

    }

    public interface Fire
    {
        public void Fire();
    }

    public enum Type
    {
        None,
        Player,//玩家
        Entity,//敌人
        Props, //道具
    }
}
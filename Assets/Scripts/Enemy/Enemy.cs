using UnityEngine;
using UnityEngine.UI;

namespace DemoGame
{
    public class Enemy : MonoBehaviour, IPoolBase, MiniMap
    {
        #region 对象池部分

        private bool _IsUse;
        public bool IsUse { get => _IsUse; set => _IsUse = value; }
        private int _Num;
        public int Num { get => _Num; set => _Num = value; }

        public void Get()
        {
            IsUse = true;
            this.gameObject.SetActive(true);
        }

        public void Release()
        {
            IsUse = false;
            this.gameObject.SetActive(false);
        }

        #endregion

        #region 小地图部分

        private void OnEnable()
        {
            GameManager.Instance.MiniMapTail.Add(this);
        }

        private void OnDisable()
        {
            GameManager.Instance.MiniMapTail.Remove(this);
        }

        public MiniType _MiniType { get => MiniType.Enemy; }

        public Transform _Transform { get => this.transform; }

        #endregion


        public void Injured()
        {
            throw new System.NotImplementedException();
        }

        public EnemyDetail _EnemyDetail;

        private void Update()
        {
            _EnemyDetail.Move(this);
        }

    }


    public enum MiniType
    {
        None,
        Player,//玩家
        Enemy,//敌人
        Props, //道具
    }
}
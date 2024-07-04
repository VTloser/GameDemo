/*
 * FileName:      EnemyAgaent.cs
 * Author:        魏宇辰
 * Date:          2023/07/21 16:33:57
 * Describe:      怪物代理类
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using DemoGame.Bullet;
using DemoGame.Pool;
using UnityEngine;

namespace DemoGame.Enemy
{
    /// <summary>
    /// 怪物代理
    /// </summary>
    public class EnemyAgaent : MonoBehaviour, IPoolBase, IMiniMap
    {
        #region 对象池部分

        public bool IsUse { get; set; }

        public int Num { get; set; }

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

        public MiniType _MiniType
        {
            get => MiniType.Enemy;
        }

        public Transform _Transform
        {
            get => this.transform;
        }

        #endregion

        public EnemyDetail enemyDetail;
        public SpriteRenderer sprite;
        public EnemyType enemyType;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_enemyType"></param>
        public void Init(EnemyType _enemyType)
        {
            enemyType = _enemyType;
            enemyDetail = GameManager.Instance.enemyFactory.GetEnemyDetail(enemyType);
            enemyDetail.Init(this, GameManager.Instance.enemyFactory.GetAttr(enemyType));
        }

        /// <summary>
        /// 属性突变 修改特殊属性
        /// </summary>
        /// <param name="specialAttr"></param>
        public void Mutations(EnemyAttr specialAttr)
        {
            //enemyDetail.SpecialAttr = specialAttr;
            enemyDetail.Init(this, GameManager.Instance.enemyFactory.GetAttr(enemyType));
        }
        
    }
}
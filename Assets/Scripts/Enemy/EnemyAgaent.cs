/*
 * FileName:      EnemyAgaent.cs
 * Author:        魏宇辰
 * Date:          2023/07/21 16:33:57
 * Describe:      怪物代理类
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using UnityEngine.UI;
using UnityEngine;


namespace DemoGame
{
    /// <summary>
    /// 怪物代理
    /// </summary>
    public class EnemyAgaent : MonoBehaviour, IPoolBase , IMiniMap
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

        public MiniType _MiniType { get => MiniType.Enemy; }

        public Transform _Transform { get => this.transform; }

        #endregion
        
        public EnemyDetail _EnemyDetail;

        ////关闭160-170
        private void Update()
        {
            //_EnemyDetail.Move(); //打开 60帧

            //全关闭 170-160帧
        }

    }
}
/*
 * FileName:      BulletAgaent.cs
 * Author:        魏宇辰
 * Date:          2023/07/19 16:58:25
 * Describe:      子弹代理类
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using UnityEngine;

namespace DemoGame
{
    [System.Serializable]
    public class BulletAgent : MonoBehaviour, IPoolBase
    {
        #region 对象池部分

        private bool _isUse;
        public bool IsUse { get => _isUse; set => _isUse = value; }

        private int _num;
        public int Num { get => _num; set => _num = value; }

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

        [SerializeField]
        public BulletDetail _BulletDetail;
        public SpriteRenderer Sprite;

        public void Init(BulletDetail bulletDetail)
        {
            _BulletDetail = bulletDetail;
            _BulletDetail.Int(this);
            Sprite.enabled = true;
            
            StartCoroutine(_BulletDetail.LifeTime());
        }

        //注释Update 180-190
        private void Update()
        {
            //_BulletDetail.Move();
        }

        //private void OnBecameInvisible()
        //{
        //    Debug.Log(111);
        //    Sprite.enabled = false;
        //}
    }
}
using Codice.CM.Common;
using System.Collections;
using System.Threading;
using UnityEditor.Overlays;
using UnityEngine;

namespace DemoGame
{
    [System.Serializable]
    public class BulletAgaent : MonoBehaviour, IPoolBase
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

        [SerializeField]
        public BulletDetail _BulletDetail;
        public SpriteRenderer Sprite;

        public void Init(BulletDetail bulletDetail)
        {
            _BulletDetail = bulletDetail;
            _BulletDetail.Int(this);
            Sprite.enabled = true;
            //StartCoroutine(_BulletDetail.LifeTime());
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
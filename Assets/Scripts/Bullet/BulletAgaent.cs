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
        #region ����ز���

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
        private BulletDetail _BulletDetail;
        public SpriteRenderer Sprite;

        public void Init(BulletDetail bulletDetail)
        {
            _BulletDetail = bulletDetail;
            _BulletDetail.Int(this);
            Sprite.enabled = true;
            StartCoroutine(_BulletDetail.LifeTime());
        }

        //ע��Update 180-190
        private void Update()
        {
            //180-170
            _BulletDetail.Move(); //��Move 150 -160
            _BulletDetail.JudgeHit();  //���˶� 150-140
            //ȫ��120-130
        }

        //private void OnBecameInvisible()
        //{
        //    Debug.Log(111);
        //    Sprite.enabled = false;
        //}
    }
}
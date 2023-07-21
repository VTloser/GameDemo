using System.Collections;
using System.Threading;
using UnityEditor.Overlays;
using UnityEngine;

namespace DemoGame
{
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

        private BulletDetail _BulletDetail;
        public SpriteRenderer Sprite;


        private void Update()
        {
            _BulletDetail.Move();
            try
            {
                _BulletDetail.JudgeHit();

            }
            catch
            {
                Debug.Log(_BulletDetail);
                Debug.Log(this.transform);
                throw;
            }
        }
    }
}
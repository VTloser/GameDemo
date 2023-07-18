using System.Collections;
using System.Threading;
using UnityEditor.Overlays;
using UnityEngine;

namespace DemoGame
{
    public class Bullet : MonoBehaviour, IPoolBase
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

        public BulletDetail _BulletDetail ;

        public void Init(BulletDetail bulletDetail)
        {
            _BulletDetail = bulletDetail;
            StartCoroutine(DieCountDown());
        }


        /// <summary>
        /// 死亡倒计时
        /// </summary>
        public IEnumerator DieCountDown()
        {
            yield return new WaitForSeconds(_BulletDetail.bulletAttr.LifeTime);
            Die();
        }

        /// <summary>
        /// 子弹销毁
        /// </summary>
        public void Die()
        {
            GameManager.BulletManager.Destroy(this);
        }


        private void Update()
        {
            _BulletDetail.Move(this);
            //if (BulletDetail.JudgeHit())
            //    Die();
        }

        public void SwitchType()
        {

        }
    }
}
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

        private BulletDetail LastBulletDetail;
        public BulletDetail _BulletDetail;

        public void Init(BulletDetail bulletDetail)
        {
            if(bulletDetail != LastBulletDetail)
            {
                _BulletDetail = bulletDetail;
                SwitchBulletDetail(_BulletDetail, LastBulletDetail);
                LastBulletDetail = bulletDetail;
            }
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
            if (_BulletDetail.JudgeHit(this.transform))
                Die();
        }

        public void SwitchBulletDetail(BulletDetail newDetail, BulletDetail oldDetail)
        {
            if (this.transform.Find(newDetail.Mode()) == null)
            {
                Instantiate(GameManager.ResourceManager.Load<GameObject>("Bullet/" + newDetail.Mode()), this.transform).name = newDetail.Mode();
            }
            this.transform.Find(newDetail.Mode()).gameObject.SetActive(true);

            if (oldDetail != null)
                this.transform.Find(oldDetail?.Mode())?.gameObject?.SetActive(false);
        }
    }
}
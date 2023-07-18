using System.Collections;
using System.Threading;
using UnityEditor.Overlays;
using UnityEngine;

namespace DemoGame
{
    public class Bullet : MonoBehaviour, IPoolBase
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
            StartCoroutine(DieCountDown());
        }

        public void Release()
        {
            IsUse = false;
            this.gameObject.SetActive(false);
        }

        #endregion


        /// <summary>
        /// ��������ʱ
        /// </summary>
        public IEnumerator DieCountDown()
        {
            yield return new WaitForSeconds(BulletDetail.bulletAttr.LifeTime);
            Die();
        }

        /// <summary>
        /// �ӵ�����
        /// </summary>
        public void Die()
        {
            GameManager.BulletManager.Destroy(this);
        }

        public BulletDetail BulletDetail = new DemoBullet();

        private void Update()
        {
            BulletDetail.Move(this);
            //if (BulletDetail.JudgeHit())
            //    Die();
        }

        public void SwitchType()
        {

        }
    }
}
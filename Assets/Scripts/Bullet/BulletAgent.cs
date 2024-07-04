/*
 * FileName:      BulletAgaent.cs
 * Author:        魏宇辰
 * Date:          2023/07/19 16:58:25
 * Describe:      子弹代理类
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using DemoGame.Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace DemoGame.Bullet
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
        
        public BulletDetail bulletDetail;
        [FormerlySerializedAs("Sprite")] public SpriteRenderer sprite;

        public void Init(BulletType bulletType)
        {
            bulletDetail = GameManager.Instance.bulletFactory.GetDetail(bulletType);
            bulletDetail.Int(this, GameManager.Instance.bulletFactory.GetAttr(bulletType));
            StartCoroutine(bulletDetail.LifeTime());
        }
    }
}
/*
 * FileName:      DamageText.cs
 * Author:        摩诘创新
 * Date:          2023/10/27 15:09:47
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DemoGame
{
    public class FloatingWordItem : MonoBehaviour, IPoolBase
    {
        #region 对象池部分

        private bool _IsUse;

        public bool IsUse
        {
            get => _IsUse;
            set => _IsUse = value;
        }

        private int _Num;

        public int Num
        {
            get => _Num;
            set => _Num = value;
        }

        public void Get()
        {
            this.gameObject.transform.localScale = Vector3.one;
        }

        public void Release()
        {
            this.gameObject.transform.localScale = Vector3.zero;
            IsUse = false;
        }

        #endregion

        [SerializeField] private Text damageText;

        private float moveY = 25;
        private float moveTime = 0.4f;
        private float fadeTime = 0.2f;

        public void Hit(Vector2 pos, float damage)
        {
            IsUse = true;
            this.transform.position = pos;
            damageText.text = damage.ToString();

            MoveUp();
        }

        private void MoveUpDown()
        {
            damageText.DOFade(1, 0);
            damageText.transform.DOMove(new Vector2(4, moveY), moveTime)
                .SetRelative()
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    damageText.DOFade(0, fadeTime).OnComplete(Release);
                });
        }

        private void MoveUp()
        {
            damageText.DOFade(1, 0);
            damageText.transform.DOMoveY(moveY, moveTime)
                .SetRelative()
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    damageText.DOFade(0, fadeTime).OnComplete(Release);
                });
        }
    }
}
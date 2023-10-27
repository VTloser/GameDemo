/*
 * FileName:      DamageTextManager.cs
 * Author:        摩诘创新
 * Date:          2023/10/27 16:23:05
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoGame
{
    public class FloatingWordManager
    {
        #region 对象池部分

        private FloatingWordItem damageTextItem;
        private Pool<FloatingWordItem> damageTextPool;
        
        #endregion

        public void Init(Transform fatherTrans)
        {
            //damageTextItem = GameManager.Instance.ResourceManager.Load<DamageTextItem>("DamageTextItem");
            damageTextItem = Resources.Load<FloatingWordItem>("FloatingWordItem");
            damageTextPool = new Pool<FloatingWordItem>(damageTextItem, fatherTrans, 64);
        }

        public void Damage(Vector2 pos, float damage)
        {
            damageTextPool.GetObject().Hit(pos, damage);
        }
    }
}
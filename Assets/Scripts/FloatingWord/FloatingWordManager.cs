/*
 * FileName:      DamageTextManager.cs
 * Author:        摩诘创新
 * Date:          2023/10/27 16:23:05
 * Describe:      飘字管理
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System.Collections;
using System.Collections.Generic;
using DemoGame.Pool;
using UnityEngine;


namespace DemoGame
{
    /// <summary>
    /// 飘字管理器
    /// </summary>
    public class FloatingWordManager
    {
        #region 对象池部分

        private FloatingWordItem damageTextItem;
        private Pool<FloatingWordItem> damageTextPool;

        #endregion

        //当前使用相机
        private Camera _camera;

        /// <summary>
        /// 飘字初始化
        /// </summary>
        /// <param name="fatherTrans"></param>
        public void Init(Transform fatherTrans, Camera camera)
        {
            //damageTextItem = GameManager.Instance.ResourceManager.Load<DamageTextItem>("DamageTextItem");
            damageTextItem = Resources.Load<FloatingWordItem>("FloatingWordItem");
            damageTextPool = new Pool<FloatingWordItem>(damageTextItem, fatherTrans, 64);

            _camera = camera;
        }

        /// <summary>
        ///  显示飘字
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="damage"></param>
        public void Damage(Vector2 pos, float damage)
        {
            damageTextPool.GetObject().Hit(_camera.WorldToScreenPoint(pos), damage);
        }
    }
}
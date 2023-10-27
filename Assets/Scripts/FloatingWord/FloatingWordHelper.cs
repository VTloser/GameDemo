/*
 * FileName:      DamageHelper.cs
 * Author:        摩诘创新
 * Date:          2023/10/27 16:28:52
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using UnityEngine;

namespace DemoGame
{
    public class FloatingWordHelper : MonoBehaviour
    {
        private FloatingWordManager floatingWordMgr;
        
        public Transform canvas;

        void Awake()
        {
            floatingWordMgr = new FloatingWordManager();
            floatingWordMgr.Init(canvas.transform);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                floatingWordMgr.Damage(Input.mousePosition, Random.Range(10f, 30f));
            }
        }
    }
}
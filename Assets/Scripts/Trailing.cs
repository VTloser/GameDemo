/*
 * FileName:      Trailing.cs
 * Author:        魏宇辰
 * Date:          2023/07/28 16:19:25
 * Describe:      人物拖尾
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoGame
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class Trailing : MonoBehaviour
    {
        public int TrailingCount;
        public int Interval = 1000;
        private SpriteRenderer SpriteRenderer;
        private List<GameObject> TrailingItem;

        private void Awake()
        {
            SpriteRenderer = this.GetComponent<SpriteRenderer>();

            TrailingItem.Add(new GameObject("TrailingItem", typeof(SpriteRenderer)));
        }


        private void Update()
        {
            // SpriteRenderer.re
        }
    }
}

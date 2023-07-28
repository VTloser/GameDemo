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
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace DemoGame
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class Trailing : MonoBehaviour
    {
        public int TrailingCount;
        public int Interval = 500;
        private SpriteRenderer SpriteRenderer;
        private List<SpriteRenderer> TrailingItem;

        private int useNum;//使用的序号

        public int _useNum { get { if (useNum > TrailingCount) useNum %= TrailingCount; return useNum; } }

        private void Awake()
        {
            SpriteRenderer = this.GetComponent<SpriteRenderer>();
            TrailingItem.Add(new GameObject("TrailingItem", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>());
        }

        private void Update()
        {
            // SpriteRenderer.re
        }

        public async void Generate()
        {
            while (true)
            {
                await Task.Delay(Interval);
                TrailingItem[_useNum].transform.position = this.transform.position;
                TrailingItem[_useNum].sprite = SpriteRenderer.sprite;
                //TrailingItem[_useNum].sprite.
            }
        }


        private void OnDestroy()
        {
            
        }
    }
}

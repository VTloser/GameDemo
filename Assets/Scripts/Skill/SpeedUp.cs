/*
 * FileName:      Trailing.cs
 * Author:        魏宇辰
 * Date:          2023/07/28 16:19:25
 * Describe:      人物拖尾
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using DG.Tweening;
using System.Collections.Generic;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine;


namespace DemoGame
{
    /// <summary>
    /// 加速技能  人物身后产生拖尾
    /// </summary>
    public class SpeedUp : Skills
    {
        /// <summary>  最大可同屏幕数  </summary>
        public int TrailingCount = 5;

        /// <summary>  产生距离  </summary>
        public float GenerateDistance = 3f;

        /// <summary>  渐隐时间  </summary>
        public float CutTime = 1;

        /// <summary>  精灵渲染器  </summary>
        private SpriteRenderer SpriteRenderer;
        
        /// <summary>  拖尾队列  </summary>
        private List<SpriteRenderer> TrailingItem = new List<SpriteRenderer>();
        
        private int _useNum; //使用的序号

        private int UseNum
        {
            get
            {
                if (_useNum >= TrailingCount) _useNum -= TrailingCount;
                return _useNum++;
            }
        }

        private Vector3 LastGenerate;

        public SpeedUp(BaseUnit owner, SkillType skillType) : base(owner, skillType)
        {
            SpriteRenderer = Owner.GetComponent<SpriteRenderer>();
            for (int i = 0; i < TrailingCount; i++)
            {
                TrailingItem.Add(new GameObject("TrailingItem", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>());
            }
            LastGenerate = Owner.transform.position;
            
            Owner.UpdateAction += ToDo;
        }

        public override void ToDo()
        {
            if (GameManager.Instance.inputManager.SpeedUp())
            {
                if (Vector3.Distance(LastGenerate, Owner.transform.position) > GenerateDistance)
                {
                    int Count = UseNum;
                    TrailingItem[Count].sprite = SpriteRenderer.sprite;
                    TrailingItem[Count].material = GameManager.Instance.ResourceManager.Load<Material>("Material/SpeedUp");
                    TrailingItem[Count].flipX = SpriteRenderer.flipX;
                    TrailingItem[Count].DOFade(1, 0f);
                    TrailingItem[Count].DOFade(0, CutTime);

                    var position = Owner.transform.position;
                    TrailingItem[Count].transform.position = position;
                    LastGenerate = position;
                }
            }
        }

        public override void Release()
        {
            Owner.UpdateAction -= ToDo;
        }
        
    }
}
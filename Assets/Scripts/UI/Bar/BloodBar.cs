/*
 * FileName:      BloodBar.cs
 * Author:        Administrator
 * Date:          2024/07/03 10:23:03
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.Utilities;

namespace DemoGame
{
    public class BloodBar : MonoBehaviour
    {
        /// <summary> 真实血条 </summary>
        public Image RealBar;

        /// <summary> 治疗缓动 </summary>
        public Image HealthBar;

        /// <summary> 表演血条 </summary>
        public Image ActionBar;
        
        /// <summary> 血条上限 </summary>
        public Image MaxBar;

        private RectTransform RealBartrasn;
        private RectTransform HealthBartrasn;
        private RectTransform ActionBartrasn;
        private RectTransform MaxBartrasn;
        
        /// <summary> 最大生命值 </summary>
        private float MaxHp = 100;
        

        /// <summary> 记录血量 </summary>
        private float RecordBlood;

        private void Awake()
        {
            RealBartrasn = RealBar.GetComponent<RectTransform>();
            HealthBartrasn = HealthBar.GetComponent<RectTransform>();
            ActionBartrasn = ActionBar.GetComponent<RectTransform>();
            MaxBartrasn = MaxBar.GetComponent<RectTransform>();

            // Init(MaxHp);
        }


        /// <summary>
        /// 血条初始化 
        /// </summary>
        public void Init(float maxBloodValue)
        {
            MaxHpChange(maxBloodValue);

            RealBar.DOFillAmount(1, 0.5f);
            HealthBar.DOFillAmount(1, 0.5f);
            ActionBar.DOFillAmount(1, 0.5f);
            MaxBar.DOFillAmount(1, 0.5f);

            MaxHp = maxBloodValue;
            RecordBlood = MaxHp;
        }

        /// <summary>
        /// 修改血条发生变化
        /// </summary>
        public void MaxHpChange(float maxBloodValue)
        {
            RealBartrasn.rect.SetWidth(400 + maxBloodValue);
            HealthBartrasn.rect.SetWidth(400 + maxBloodValue);
            ActionBartrasn.rect.SetWidth(400 + maxBloodValue);
            MaxBartrasn.rect.SetWidth(400 + maxBloodValue);
        }

        /// <summary>
        /// 血条改变
        /// </summary>
        public async void BloodChange(float bloodValue)
        {
            float rate = bloodValue / MaxHp;
            if (bloodValue - RecordBlood < 0.1f)
            {
                RealBar.fillAmount = rate;
                HealthBar.fillAmount = rate;

                await Task.Delay(200);
                ActionBar.DOFillAmount(rate, 0.5f);
            }
            else if (bloodValue - RecordBlood > 0.1f)
            {
                HealthBar.fillAmount = rate;

                await Task.Delay(200);
                ActionBar.DOFillAmount(rate, 0.5f);
                RealBar.DOFillAmount(rate, 0.5f);
            }

            RecordBlood = bloodValue;
        }
        
        
        
        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.Q))
        //     {
        //         CurrentValue -= 10;
        //         BloodChange(CurrentValue);
        //     }
        //     
        //     if (Input.GetKeyDown(KeyCode.E))
        //     {
        //         CurrentValue += 10;
        //         BloodChange(CurrentValue);
        //     }
        // }
        
    }
}
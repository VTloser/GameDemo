using System;
using DemoGame;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DemoGame.Base;
using DemoGame.Skill;
using DemoGame.UI;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace DemoGame
{
    /// <summary>
    /// Player类
    /// </summary>
    public class Player : BaseUnit, IMiniMap
    {
        
        #region 小地图部分
        
        public MiniType _MiniType
        {
            get => MiniType.Player;
        }

        public Transform _Transform
        {
            get => this.transform;
        }


        private void OnEnable()
        {
            GameManager.Instance.MiniMapTail.Add(this);
            GameManager.Instance.Player = this;
            PlayerCam = Camera.main;
        }

        private void OnDisable()
        {
            GameManager.Instance.MiniMapTail.Remove(this);
        }
        
        #endregion
        
        
        /// <summary>  初始移动速度  </summary>
        public float MoveSpeed = 10;

        /// <summary>  当前移动速度  </summary>
        private float CurrentSpeed;

        /// <summary>  最大生命值  </summary>
        public float MaxHp = 100;

        /// <summary>  当前生命值  </summary>
        public float CurrentHp;

        /// <summary>  碰撞范围  </summary>
        public float HitRange = 2;
        
        /// <summary>  无敌时间  </summary>
        public float ProtectionTime = 1; // 无敌时间 
        
        /// <summary>  是否正处于无敌状态中  </summary>
        public bool IsProtection;
        
        /// <summary>  是否死亡  </summary>
        public bool IsDie;
        
        public Camera PlayerCam;

        public BloodBar bloodBar;

        private SkillType PlayerSkill;

        private SpriteRenderer spriteRenderer;
        
        private void Awake()
        {
            spriteRenderer = this.GetComponent<SpriteRenderer>();
            PlayerSkill.Add(this, SkillType.SpeedUp);

            CurrentHp = MaxHp;
        }

        private void Start()
        {
            //加载血条UI
            BloodBar item = GameManager.Instance.ResourceManager.Load<BloodBar>("UI/BloodBar");
            bloodBar = Instantiate(item, UIMgr.Instance.StatusUI);
            bloodBar.Init(MaxHp);

            GameManager.Instance.inputManager.Init(this,
                () =>
                {
                    GameManager.Instance.BulletManager.Fire(GameManager.Instance.inputManager.FireDir(PlayerCam));
                },
                () =>
                {
                    CurrentSpeed = MoveSpeed * 2;
                    SkillFactor.GetSkills(this, PlayerSkill).ToDo();
                },
                () =>
                {
                    
                }
            );

        }

        public override void Update()
        {
            base.Update();
            
            this.transform.position +=
                GameManager.Instance.inputManager.MoveControl() * (Time.deltaTime * CurrentSpeed);

            spriteRenderer.flipX = GameManager.Instance.inputManager.MoveControl().x < 0;
        }
        
        /// <summary>
        /// 受到伤害
        /// </summary>
        public void Injury(float damage)
        {
            if (IsProtection || IsDie) return;
            GameManager.Instance.FloatingWordMgr.Damage(this.transform.position, damage);
            CurrentHp -= damage;
            bloodBar.BloodChange(CurrentHp);

            if (CurrentHp <= 0)
            {
                Die();
            }
            else
            {
                Protection();
            }
        }
        
        /// <summary>
        /// 受伤后的无敌时间
        /// </summary>
        public async void Protection()
        {
            using var isProtectionDis = new MyDisposable(value => IsProtection = value);
            await Task.Delay(TimeSpan.FromSeconds(ProtectionTime));
        }

        /// <summary>
        /// 死亡
        /// </summary>
        public void Die()
        {
            IsDie = true;
            Debug.Log("Game Over!");
        }

        
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public PlayerData GetData()
        {
            return new PlayerData(this.transform.position, HitRange);
        }
        
    }
}

public class MyDisposable : IDisposable
{
    private readonly Action<bool> _callBack;

    public MyDisposable(Action<bool> callBack)
    {
        _callBack = callBack;
        _callBack.Invoke(true);
    }

    public void Dispose() => _callBack.Invoke(false);
}
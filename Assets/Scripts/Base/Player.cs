using DemoGame;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
namespace DemoGame
{
    public class Player : BaseUnit, IMiniMap
    {
        public Camera PlayerCam;

        private SkillType PlayerSkill;

        public float MoveSpeed = 10;
        [SerializeField]
        private float CurrentSpeed;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = this.GetComponent<SpriteRenderer>();
            PlayerSkill.ADD(SkillType.SpeedUp);
            SkillFactor.GetSkills(this, PlayerSkill);
        }

        public override void Update()
        {
            base.Update();
            if (GameManager.Instance.inputManager.Fire())
            {
                GameManager.Instance.BulletManager.Fire(GameManager.Instance.inputManager.LookAt(PlayerCam, 0) - this.transform.position);
            }
            CurrentSpeed = MoveSpeed;
            if (GameManager.Instance.inputManager.SpeedUp())
            {
                CurrentSpeed *= 2;
            }

            this.transform.position += GameManager.Instance.inputManager.MoveControl() * Time.deltaTime * CurrentSpeed;

            spriteRenderer.flipX = GameManager.Instance.inputManager.MoveControl().x >= 0 ? false : true;

        }

        #region 小地图部分

        public MiniType _MiniType { get => MiniType.Player; }
        public Transform _Transform { get => this.transform; }


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
    }
}

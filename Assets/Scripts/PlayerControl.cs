using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
namespace DemoGame
{
    public class PlayerControl : MonoBehaviour, IMiniMap
    {
        
        public Camera PlayerCam;


        private void Update()
        {
            this.transform.position += GameManager.Instance.inputManager.MoveControl() * Time.deltaTime * 10;

            if (GameManager.Instance.inputManager.Fire())
            {
                GameManager.Instance.BulletManager.Fire(GameManager.Instance.inputManager.LookAt(PlayerCam, 0) - this.transform.position);
            }
        }

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

    }
}

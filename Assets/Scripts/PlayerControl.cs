using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
namespace DemoGame
{
    public class PlayerControl : MonoBehaviour, MiniMap
    {
        InputManager inputManager = new PCInputManager();
        public Camera PlayerCam;


        private void Update()
        {
            this.transform.position += inputManager.MoveControl() * Time.deltaTime * 10;

            this.transform.rotation = Quaternion.FromToRotation(Vector3.right, inputManager.LookAt(PlayerCam, 0) - this.transform.position);

            if (inputManager.Fire())
            {
                GameManager.Instance.BulletManager.Fire(this.transform.position, this.transform.forward);
            }
        }

        public MiniType _MiniType { get => MiniType.Player; }
        public Transform _Transform { get => this.transform; }

        private void OnEnable()
        {
            GameManager.Instance.MiniMapTail.Add(this);
            GameManager.Instance.Player = this;
        }

        private void OnDisable()
        {
            GameManager.Instance.MiniMapTail.Remove(this);
        }

    }
}

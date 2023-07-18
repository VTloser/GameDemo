using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
namespace DemoGame
{
    public class PlayerControl : MonoBehaviour
    {
        InputManager inputManager = new PCInputManager();


        public Camera PlayerCam;

        private void Update()
        {
            this.transform.position += inputManager.MoveControl() * Time.deltaTime * 10;

            this.transform.LookAt(inputManager.LookAt(PlayerCam, 0));

            if (inputManager.Fire())
            {
                GameManager.BulletManager.Fire(this.transform.position, this.transform.forward);
            }
        }
    }


}
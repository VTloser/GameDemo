/*
 * FileName:      InputManager.cs
 * Author:        魏宇辰
 * Date:          2023/07/18 15:33:39
 * Describe:      输入系统
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System.Security.Cryptography;
using UnityEngine;

namespace DemoGame
{
    public interface InputManager
    {
        public Vector3 MoveControl();

        public bool Jump();

        public Vector3 LookAt(Camera camera, float height);

        public bool Fire();
    }

    public class PCInputManager : InputManager
    {
        public Vector3 MoveControl()
        {
            return new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        }

        public bool Jump()
        {
            return Input.GetAxisRaw("Jump") > 0.5f;
        }

        public Vector3 LookAt(Camera camera, float Z)
        {
            Plane plane = new Plane(Vector3.forward, Z);
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            Vector3 GetPoint = Vector3.zero;
            if (plane.Raycast(ray, out float enter))
            {
                GetPoint = ray.GetPoint(enter);
            }
            return GetPoint;
        }

        public bool Fire()
        {
            return Input.GetMouseButton(0);
        }
    }
}
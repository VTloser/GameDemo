/*
 * FileName:      InputManager.cs
 * Author:        魏宇辰
 * Date:          2023/07/18 15:33:39
 * Describe:      输入系统
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using UnityEngine;
using UnityEngine.Events;

namespace DemoGame.Manager
{
    public interface InputManager
    {
        /// <summary>
        /// 移动控制
        /// </summary>
        /// <returns></returns>
        public Vector3 MoveControl();
        
        /// <summary>
        /// 射击朝向
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public Vector3 FireDir(Camera camera);
        
        /// <summary>
        /// 开火控制
        /// </summary>
        public KeyCode Fire { get; set; }
        public UnityAction FireCallBack { get; set; }
        
        /// <summary>
        /// 技能一
        /// </summary>
        public KeyCode Skill_One { get; set; }
        public UnityAction Skill_OneCallBack { get; set; }
        
        /// <summary>
        /// 技能二
        /// </summary>
        public KeyCode Skill_Two { get; set; }
        public UnityAction Skill_TwoCallBack { get; set; }

        
        /// <summary>
        /// 案件周期相应
        /// </summary>
        public void Tick();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(Player _player, UnityAction fire, UnityAction Skill_One, UnityAction Skill_Two);
    }

    public class PCInputManager : InputManager
    {
        private Player player;

        public PCInputManager()
        {

            Fire = KeyCode.Mouse0;
            Skill_One = KeyCode.LeftShift;
            Skill_Two = KeyCode.Q;
        }

        public void Init(Player _player, UnityAction fireCallBack, UnityAction skill_OneCallBack,
            UnityAction skill_TwoCallBack)
        {
            player = _player;
            FireCallBack = fireCallBack;
            Skill_OneCallBack = skill_OneCallBack;
            Skill_TwoCallBack = skill_TwoCallBack;
        }

        public Vector3 MoveControl()
        {
            return new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        }
        
        
        Plane _plane = new Plane(Vector3.forward,0);
        public Vector3 FireDir(Camera camera)
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            Vector3 getPoint = Vector3.zero;
            if (_plane.Raycast(ray, out float enter))
            {
                getPoint = ray.GetPoint(enter);
            }
            return getPoint - player.transform.position;
        }

        
        public KeyCode Fire { get; set; }
        public UnityAction FireCallBack { get; set; }
        public KeyCode Skill_One { get; set; }
        public UnityAction Skill_OneCallBack { get; set; }
        
        public KeyCode Skill_Two { get; set; }
        public UnityAction Skill_TwoCallBack { get; set; }

        
        public void Tick()
        {
            if (Input.GetKeyDown(Fire))
            {
                FireCallBack.Invoke();
            }
            
            if (Input.GetKeyDown(Skill_One))
            {
                Skill_OneCallBack.Invoke();
            }
            
            if (Input.GetKeyDown(Skill_Two))
            {
                Skill_TwoCallBack.Invoke();
            }
        }
        
        
    }
}
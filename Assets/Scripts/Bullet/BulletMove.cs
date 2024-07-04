/*
 * FileName:      BulletMove.cs
 * Author:        摩诘创新
 * Date:          2023/10/20 10:09:13
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System;
using DG.Tweening;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;


namespace DemoGame
{
    /// <summary>
    /// 子弹移动方式
    /// </summary>
    public abstract class BulletMove
    {
        public abstract void Move(Transform agent, Transform tag, float bulletAttr, float lifeTime);
    }
    
    /// <summary>
    /// 匀速直线跟踪移动
    /// </summary>
    public class StopMove : BulletMove
    {
        private float _trackWaitTime = 0f; // 追踪延迟时间
        
        public override void Move(Transform agent, Transform tag, float moveSpeed, float lifeTime)
        {
        }
    }
    
    
    /// <summary>
    /// 匀速直线跟踪移动
    /// </summary>
    public class TrackingMove : BulletMove
    {
        private float _trackWaitTime = 0f; // 追踪延迟时间
        
        public override void Move(Transform agent, Transform tag, float moveSpeed, float lifeTime)
        {
            
            if (tag is not null && lifeTime > _trackWaitTime)
            {
                Quaternion t = Quaternion.FromToRotation(Vector3.up, tag.position - agent.transform.position);
                agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, t, 0.05f);
            }

            agent.Translate(Vector3.up * (Time.deltaTime * moveSpeed));
        }
    }

    /// <summary>
    /// 变速跟踪运动
    /// </summary>
    public class TrackingVariableMove : BulletMove
    {
        public float angle = 45f;
        public float angleTime = 1f;

        public override void Move(Transform agent, Transform tag, float moveSpeed, float lifeTime)
        {
            if (tag is not null)
            {
                Quaternion t = Quaternion.FromToRotation(Vector3.up, tag.position - agent.transform.position);
                agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, t, 0.05f);
            }

            moveSpeed *= Math.Clamp(lifeTime * lifeTime * lifeTime * lifeTime * lifeTime, 0, 50);
            agent.Translate(Vector3.up * (Time.deltaTime * moveSpeed));
        }
    }
}
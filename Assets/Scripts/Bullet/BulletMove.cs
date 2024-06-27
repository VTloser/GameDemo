/*
 * FileName:      BulletMove.cs
 * Author:        摩诘创新
 * Date:          2023/10/20 10:09:13
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using DG.Tweening;
using UnityEngine;


namespace DemoGame
{
    /// <summary>
    ///     子弹移动方式
    /// 
    /// </summary>
    public abstract class BulletMove
    {
        public abstract void Move(Transform agent, Transform tag, float moveSpeed);
    }

    /// <summary>
    /// 直线移动
    /// </summary>
    public class DirMove : BulletMove
    {
        public override void Move(Transform agent, Transform tag, float moveSpeed)
        {
            agent.Translate(Vector3.up * (Time.deltaTime * moveSpeed));
        }
    }

    /// <summary>
    /// 跟踪移动
    /// </summary>
    public class TrackingMove : BulletMove
    {
        public override void Move(Transform agent, Transform tag, float moveSpeed)
        {
            if (tag is not null)
            {
                Quaternion t = Quaternion.FromToRotation(Vector3.up, tag.position - agent.transform.position);
                agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, t, 0.05f);
            }

            agent.Translate(Vector3.up * (Time.deltaTime * moveSpeed));
        }
    }

    /// <summary>
    /// 随机移动
    /// </summary>
    public class RandomMove : BulletMove
    {
        public float angle = 45f;
        public float angleTime = 1f;

        public override void Move(Transform agent, Transform tag, float moveSpeed)
        {
            // agent.transform.DORotate(new Vector3(0, 0, angle / 2f), angleTime / 2f).SetRelative().OnComplete(() =>
            // {
            //     Sequence se = DOTween.Sequence();
            //     se.Append(agent.transform.DORotate(new Vector3(0, 0, -angle), angleTime)).SetRelative(); //增加一段动画
            //     se.Append(agent.transform.DORotate(new Vector3(0, 0, angle), angleTime).SetRelative()); //增加一段动画
            //     se.SetLoops(-1, LoopType.Restart);
            // });

            agent.transform.Translate(Vector3.up * (Time.deltaTime * moveSpeed));
        }
    }
}
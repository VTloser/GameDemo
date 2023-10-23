/*
 * FileName:      BulletMove.cs
 * Author:        摩诘创新
 * Date:          2023/10/20 10:09:13
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using UnityEngine;

namespace DemoGame
{
    public abstract class BulletMove
    {
        public abstract void Move(Transform agent, Transform tag, float moveSpeed);
    }

    public class DirMove : BulletMove
    {
        public override void Move(Transform agent, Transform tag, float moveSpeed)
        {
            agent.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
    }

    public class TrackingMove : BulletMove
    {
        public override void Move(Transform agent, Transform tag, float moveSpeed)
        {
            if (tag != null)
            {
                Quaternion t = Quaternion.FromToRotation(Vector3.up, tag.position - agent.transform.position);
                agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, t, 0.05f);
            }

            agent.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
    }
}
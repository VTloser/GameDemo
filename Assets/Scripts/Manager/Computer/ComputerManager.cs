/*
 * FileName:      ComputerManager.cs
 * Author:        摩诘创新
 * Date:          2023/07/26 15:47:06
 * Describe:      ComputerShader 计算管理模块
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using DemoGame.Manager.Computer;
using UnityEngine;


namespace DemoGame
{
    /// <summary>
    ///  ComputerShader 计算管理模块
    /// </summary>
    public class ComputerManager : MonoBehaviour
    {
        /// <summary>  子弹怪物相关 ComputerShader </summary>
        public ComputeShader BulletEnemyCS; //子弹敌人计算
        
        /// <summary>  怪物间碰撞 ComputerShader </summary>
        public ComputeShader EnemyColliderCS;
        
        /// <summary>  怪物间碰撞 ComputerShader </summary>
        public ComputeShader EnemyColliderCSTemp;

        // public BulletEnemy bulletEnemy = new();
        // public EnemyCollider enemyCollider = new();
        public EnemyColliderTemp enemyColliderTemp = new();

        private void Awake()
        {
            // bulletEnemy.Init(BulletEnemyCS, this);
            // enemyCollider.Init(EnemyColliderCS);
            enemyColliderTemp.Init(EnemyColliderCSTemp);
        }

        private void Update()
        {
            // bulletEnemy.Tick();
            // enemyCollider.Tick();
            
            enemyColliderTemp.Tick();
        }

        private void OnDestroy()
        {
            // bulletEnemy.OnDestroy();
            // enemyCollider.OnDestroy();
        }
    }

    //[Serializable]
    public struct ComputerDate
    {
        public Vector2 pos; //等价于float2
        public float radius; //半径 如果半径小于等于0则认为接触到了
        public int Live; // 0 是默认状态 -1是死亡状态  1是存活状态  
        public int index; //序号

        public float floowRadius; //追踪半径
        public int floowindex;    //追踪序号
        public int Isfloow;       //正在追踪？

        public ComputerDate(Vector2 _pos, float _radius, bool _Live, bool _Isfloow) : this()
        {
            pos = _pos;
            radius = _radius;
            Live = _Live ? 1 : -1;
            Isfloow = _Isfloow ? 1 : -1;
            floowRadius = 5;
        }
    }

    public struct BulletComputerData
    {
        public Vector2 pos;        //等价于float2
        public float hitRange;     //伤害检测范围
        public int index;          //伤害范围内的最近的敌人,如果为-1则认为没有敌人在附近
        public float followRadius;  //寻敌半径, 如果为0 则认为没有追踪功能。
        public int followIndex;     //追踪目标序号，如果为-1 则认为没有在追踪。

        public BulletComputerData(Vector2 pos, float hitRange, float followRadius = 0)
        {
            this.pos = pos;
            this.hitRange = hitRange;
            this.index = -1;
            this.followRadius = followRadius;
            this.followIndex = -1;
        }
    }

    public struct EnemyComputerData
    {
        public Vector2 pos;     //等价于float2
        public float hitRange;  //受到伤害范围 

        public EnemyComputerData(Vector2 pos, float hitRange)
        {
            this.pos = pos;
            this.hitRange = hitRange;
        }
    }


}
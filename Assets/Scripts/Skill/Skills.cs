/*
 * FileName:      Skills.cs
 * Author:        魏宇辰
 * Date:          2023/07/31 16:12:47
 * Describe:      技能抽象类
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace DemoGame
{
    public enum SkillType
    {
        None = 0,
        SpeedUp = 1,
        Other = 1 << 1,
        Other1 = 1 << 2,
    }


    public abstract class Skills
    {
        protected BaseUnit Owner;
        protected SkillType skillType;

        protected Skills(BaseUnit owner, SkillType skillType)
        {
            Owner = owner;
            this.skillType = skillType;
        }

        public abstract void ToDO();

        public abstract void Release();
    }

    public static class SkillFactor
    {
        public static Skills GetSkills(BaseUnit owner, SkillType skillType) =>
            skillType switch
            {
                SkillType.None => throw new System.NotImplementedException(),
                SkillType.SpeedUp => new SpeedUp(owner, skillType),
                SkillType.Other => throw new System.NotImplementedException(),
                SkillType.Other1 => throw new System.NotImplementedException(),
                _ => throw new System.NotImplementedException(),
            };
    }

    public static class SkillHelper
    {
        /// <summary>
        /// 添加技能
        /// </summary>
        /// <param name="state"></param>
        /// <param name="_AddState"></param>
        public static void ADD(ref this SkillType state, BaseUnit Unit, SkillType _AddState)
        {
            state |= _AddState;
            SkillFactor.GetSkills(Unit, _AddState);
        }

        /// <summary>
        /// 删除技能
        /// </summary>
        /// <param name="state"></param>
        /// <param name="_SubState"></param>
        public static void Sub(ref this SkillType state, SkillType _SubState)
        {
            state &= ~_SubState;
        }

        /// <summary>
        /// 判断是否拥有技能
        /// </summary>
        /// <param name="state"></param>
        /// <param name="_JudgeState"></param>
        /// <returns></returns>
        public static bool Judge(ref this SkillType state, SkillType _JudgeState)
        {
            if ((state & _JudgeState) == _JudgeState)
                return true;
            else
                return false;
        }

    }

}

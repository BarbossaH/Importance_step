using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.Skill
{
    /// <summary>
    /// 技能伤害范围
    /// </summary>
    public interface IAttackSelector
    {
        // 如果想要更多的禁用启用，就使用GameObject,如果想要找位置，旋转，就使用Transform。都可以
        //skillTransform技能所在物体的变换组件
        //FanShapedAttackSelector
        ///<summary>
        ///第二个参数 transform 选择目标 skillTransform是发出技能的物体
        /// </summary>
        Transform[] SelectTarget(SkillData skillData, Transform skillTransform);
    }
}
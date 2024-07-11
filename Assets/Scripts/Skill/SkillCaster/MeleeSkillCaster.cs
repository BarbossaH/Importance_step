using System.Collections;
using System.Collections.Generic;
using GameName.Skill;
using UnityEngine;

//近身施放器 将这个脚本挂在近身技能预制体中
public class MeleeSkillCaster : SkillCaster
{
    public override void ApplySkill()
    {
        //根据不同的算法，比如椭圆形的，圆形或者扇形的区域选择目标，因为计算的方式不同，所以选择器就调用不同的算法
        CalculateTargets();
        //执行技能效果影响算法
        CastSkillToTargets();
    }
}

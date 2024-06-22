using System.Collections;
using System.Collections.Generic;
using GameName.Skill;
using UnityEngine;

//近身施放器 将这个脚本挂在近身技能预制体中
public class MeleeSkillCaster : SkillCaster
{
    public override void ApplySkill()
    {
        CalculateTargets();
        //执行技能效果影响算法
        CastSkillToTargets();
    }
}

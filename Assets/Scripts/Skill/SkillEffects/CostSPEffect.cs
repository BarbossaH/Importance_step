using System.Collections;
using System.Collections.Generic;
using GameName.Character;
using GameName.Skill;
using UnityEngine;

namespace GameName.Skill
{
    //类名必须与配置和命名规则是匹配的
    public class CostSPEffect : ISkillEffect
    {
        //其实我这里只是需要SkillData，但是因为在计算技能效果是，需要开协程，而开协程只有脚本才具备的功能。
        //比如要计算技能的持续效果时，就需要开协程
        public void Execute(SkillCaster skillCaster)
        {
            //依赖注入，控制反转
            var status = skillCaster.SkillData.owner.GetComponent<CharacterStatus>();
            status.MP -= skillCaster.SkillData.costMP;
        }


    }
}
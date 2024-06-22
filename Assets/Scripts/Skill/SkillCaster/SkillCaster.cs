using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.Skill
{
    public abstract class SkillCaster : MonoBehaviour
    {
        /*这是父类，只提供接口，让子类去做具体的施放算法
        */
        private SkillData skillData;
        //技能选区算法对象，计算攻击范围，攻击方式，比如近战攻击，远程，三角，矩形等等攻击模式
        public IAttackSelector skillSelector;
        //攻击效果，伤害，自己减少魔法，眩晕
        public ISkillEffect[] skillEffects;
        //由技能管理器提供
        public SkillData SkillData
        {
            get { return skillData; }
            set
            {
                skillData = value;
                //创建算法对象
                InitCaster();
            }
        }
        //创建算法对象
        private void InitCaster()
        {
            skillSelector = SkillCasterFactory.CreateAttackSelector(SkillData);

            skillEffects = SkillCasterFactory.CreateSkillEffect(SkillData);
        }
        //执行算法对象
        //执行算法对象包含选区和产生影响
        public void CalculateTargets()
        {
            skillData.attackTargets = skillSelector.SelectTarget(skillData, transform);
        }
        public void CastSkillToTargets()
        {
            for (int i = 0; i < skillEffects.Length; i++)
            {
                // skillEffects[i].接口方法
                skillEffects[i].Execute(this);
            }
        }

        //施放方式
        public abstract void ApplySkill();
    }
}
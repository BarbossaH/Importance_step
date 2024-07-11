using System;
using System.Collections;
using System.Collections.Generic;
using GameName.Skill;
using UnityEngine;

//技能施放器配置工厂:提供创建施放器各种算法对象的功能
//作用：将对象的创建与使用分离
public class SkillCasterFactory
{
    //选择技能伤害范围，比如椭圆还是矩形
    //命名规则：namespace+ 枚举名+AttackSelector, for example: ARPGDemo.Skill.SectorAttackSelector
    // Type type = Type.GetType(selectorClassName);
    // skillSelector = Activator.CreateInstance(type) as IAttackSelector;
    public static IAttackSelector CreateAttackSelector(SkillData skillData)
    {
        //根据skillData创建具体的选择器
        string selectorClassName = string.Format("GameName.Skill.{0}AttackSelector", skillData.selectorType);

        return CreateObject<IAttackSelector>(selectorClassName);
    }

    //技能效果影响
    //一个技能通常有多个影响效果，比如伤害，眩晕，毒，etc
    //命名规则：namespace+ skillData.skillEffects[i]+Effect, ARPGDemo.Skill.CostSPEffect
    public static ISkillEffect[] CreateSkillEffect(SkillData skillData)
    {
        ISkillEffect[] skillEffects = new ISkillEffect[skillData.skillEffects.Length];
        for (int i = 0; i < skillData.skillEffects.Length; i++)
        {
            string effectName = string.Format("GameName.Skill.{0}Effect", skillData.skillEffects[i]);
            skillEffects[i] = CreateObject<ISkillEffect>(effectName);
        }

        return skillEffects;
    }

    private static T CreateObject<T>(string className) where T : class
    {
        Type type = Type.GetType(className);
        return Activator.CreateInstance(type) as T;
    }
}

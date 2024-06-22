using System;
using UnityEngine;
namespace GameName.Skill
{
    //there is a person suggesting to use ScriptableObject
    [Serializable]
    public class SkillData
    {
        public int skillID;
        public int skillLevel;
        public string skillName;
        public string SkillDesc;
        public int coolTime;
        //技能冷却剩余时间
        public int coolTimeCounter;
        public int costMP;
        public float attackDistance;
        public float attackAngle;
        public string[] attackTargetTags = { "Enemy" };

        [HideInInspector]
        public Transform[] attackTargets; //将通过标签找到的敌人存入这个数组,这是执行查找目标的时候才去赋值的，不是策划配置的
        //技能影响类型
        public string[] skillEffects = { "CostSP", "Damage" };//通过这个字符串，使用反射创建对象
        public int nextSkillID;
        public float atkRatio; //比如一个会增加基础攻击力100%的伤害，或者造成300%基础攻击力的伤害
        public float durationTime; //技能持续时间
        public float atkInterval;//伤害间隔，比如用于持续性技能伤害
        [HideInInspector]
        public GameObject owner; //谁的技能管理器，代表谁释放的技能；

        public string prefabName;
        [HideInInspector]
        public GameObject skillPrefab; //技能预置体
        public string animationName; //这是动画状态机的trigger名字
        public string hitFxName; //攻击特效名 眩晕，冰冻，毒等等
        [HideInInspector]
        public GameObject hitFxPrefab;//受攻击特效预置体

        public SkillAttackType attackType; //单体还是群体
        public SelectorType selectorType; //扇形还是矩形
    }
}
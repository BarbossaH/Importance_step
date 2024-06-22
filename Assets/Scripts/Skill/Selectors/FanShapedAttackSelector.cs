using System.Collections;
using System.Collections.Generic;
using GameName.Common;
using UnityEngine;

namespace GameName.Skill
{
    /// <summary>
    ///扇形和圆形的选区
    /// </summary>
    public class FanShapedAttackSelector : IAttackSelector
    {
        //这个文件命名必须是枚举名加AttackSelector，否则创建的时候找不到。而且必须写命名空间。总之必须符合命名规则
        public Transform[] SelectTarget(SkillData skillData, Transform skillTransform)
        {
            //根据技能数据中的标签，获取所有目标
            //data.attackTargetTags
            //string[] --> Transform
            List<Transform> targets = new List<Transform>();
            for (int i = 0; i < skillData.attackTargetTags.Length; i++)
            {
                GameObject[] tempGOArray = GameObject.FindGameObjectsWithTag(skillData.attackTargetTags[i]);
                // GameObject[] tempGOArray = GameObject.FindGameObjectsWithTag("Enemy");
                //只返回tempGOArray的transform就行了
                targets.AddRange(tempGOArray.Select(g => g.transform));
            }
            //判断攻击范围
            targets = targets.FindAll(t =>
            Vector3.Distance(t.position, skillTransform.position) <= skillData.attackDistance &&
            Vector3.Angle(skillTransform.forward, t.position - skillTransform.position) <= skillData.attackAngle / 2
            );
            //筛选出活的角色
            targets = targets.FindAll(t => t.GetComponent<Character.CharacterStatus>().HP > 0);

            //返回目标单体和群体伤害
            // data.attackType
            Transform[] result = targets.ToArray();
            //群攻击或者没找到目标
            if (skillData.selectorType == SelectorType.Multiple || result.Length == 0)
            {
                return result;
            }
            //举例最近的点
            Transform min_distance = result.GetMin(t => Vector3.Distance(t.position, skillTransform.position));
            return new Transform[] { min_distance };
        }
    }
}
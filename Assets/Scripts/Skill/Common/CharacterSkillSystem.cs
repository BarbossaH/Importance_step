using System.Collections;
using System.Collections.Generic;
using GameName.Common;
using GameName.Skill;
using UnityEngine;
[RequireComponent(typeof(PlayerSkillManager))]
public class CharacterSkillSystem : MonoBehaviour
{
    //封装技能系统，提供简单的技能施放功能

    /*
    1.准备技能
    2.播放动画
    3. 生成技能
    4.如果单攻：朝向目标，选中目的
    */
    private PlayerSkillManager skillManger;
    private Animator anim;
    SkillData skillData;
    private void Start()
    {
        skillManger = GetComponent<PlayerSkillManager>();
        anim = GetComponent<Animator>();
    }

    public void UseAttackSkill(int skillID)
    {
        //准备技能
        skillData = skillManger.PrepareSkill(skillID);
        if (skillData == null) return;
        //播放动画
        anim.SetBool(skillData.animationName, true);
        //生成技能：note: 这里我不做生成技能，在动作事件器里做
        // GetComponent<AnimationEventBehavior>().attackHandler += CastSkill;

        //如果单体攻击：       1查找目标； 2选中目标：间隔指定时间后取消选中，选中a之后，在自动取消前，选择b将取消a；
        Transform target = SelectTarget();
        if (target != null)
        {
            transform.LookAt(target);
        }


    }
    private void CastSkill()
    {
        //这个函数是在动作播放到某个特定的帧上才去调用的    
        //生成技能
        #region AI生成的
        // SkillData skillData = skillManger.PrepareSkill(skillManger.currentSkillID);
        // if (skillData == null) return;
        //朝向目标，选中目标
        // Vector3 targetPos = Vector3.zero;
        // if (skillData.skillType == SkillType.Single)
        // {
        //     targetPos = skillData.targetPos;
        // }
        // else if (skillData.skillType == SkillType.Multi)
        // {
        //     targetPos = skillData.targetPosList[0];
        // }
        // else if (skillData.skillType == SkillType.AOE)
        // {
        //     targetPos = skillData.targetPosList[0];
        // }
        // //朝向目标
        // transform.LookAt(targetPos);
        // //选中目标
        // if (skillData.skillType == SkillType.Single)
        #endregion

        //在事件触发的那一刻，创建技能
        skillManger.CreateSkill(skillData);
    }
    //随机技能，给npc提供
    public void UseRandomSkill()
    {
        //从管理器中挑选出随机的技能
        /*先筛选出所有可以施放的技能，再生产出随机数
        */
        var useableSkills = skillManger.skillDatas.FindAll(s => skillManger.PrepareSkill(s.skillID) != null);
        if (useableSkills.Length == 0) return;
        int index = UnityEngine.Random.Range(0, useableSkills.Length);
        UseAttackSkill(useableSkills[index].skillID);
    }

    private Transform SelectTarget()
    {
        Transform[] targets = new FanShapedAttackSelector().SelectTarget(skillData, transform);
        return targets.Length >= 0 ? targets[0] : null;
    }
}

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
    private void Start()
    {
        skillManger = GetComponent<PlayerSkillManager>();
    }

    public void AttackUseSkill()
    {
        skillManger.skillDatas.FindAll(s => skillManger.PrepareSkill(s.skillID));
    }

    //随机技能，给npc提供
    public void UseRandomSkill()
    {
        //从管理器中挑选出随机的技能
        /*先筛选出所有可以施放的技能，再生产出随机数
        */
    }
}

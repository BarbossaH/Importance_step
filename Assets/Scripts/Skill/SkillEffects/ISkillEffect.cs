using System.Collections;
using System.Collections.Generic;
using GameName.Skill;
using UnityEngine;

public interface ISkillEffect
{
  //这里的代码很难写，是主程序写的
  //主要有什么效果呢？伤害敌人的生命
  // void Execute(SkillData data);

  //要开携程必须是脚本，所以参数不能给SkillData，

  void Execute(SkillCaster skillCaster);
}

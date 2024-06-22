using System.Collections;
using System.Collections.Generic;
using GameName.Character;
using GameName.Skill;
using UnityEngine;

public class DamageEffect : ISkillEffect
{
    private SkillData data;
    public void Execute(SkillCaster skillCaster)
    {
        // via skillCaster.SkillData.attackTargets to find CharacterStatus HP
        data = skillCaster.SkillData;
        // StartCoroutine,StartCoroutine is enabled to be written directly because it belongs to a component
        skillCaster.StartCoroutine(RepeatDamage(skillCaster));
    }

    private IEnumerator RepeatDamage(SkillCaster skillCaster)
    {
        float atkTime = 0;
        do
        {
            OnceDamage();
            yield return new WaitForSeconds(data.atkInterval);
            atkTime += data.atkInterval;
            //to avoid to keep damage when the target is not in the range of this attack.
            skillCaster.CalculateTargets();
        } while (atkTime < data.durationTime);
    }

    private void OnceDamage()
    {
        float attack = data.atkRatio * data.owner.GetComponent<CharacterStatus>().baseAttack;
        for (int i = 0; i < data.attackTargets.Length; i++)
        {
            var status = data.attackTargets[i].GetComponent<CharacterStatus>();

            status.TakeDamage(attack);
        }

        //创建攻击特效，动画等等
    }
}

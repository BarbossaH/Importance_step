using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using GameName.Common;
using System;
using Common;
//有人说把技能效果做成中介模式
namespace GameName.Skill
{

    public class PlayerSkillManager : MonoBehaviour
    {
        //这个表在实际工作中是读取策划的配置生成的，一般都是从本地读取配置文件生成技能内容
        public SkillData[] skillDatas;

        private void Start()
        {
            for (int i = 0; i < skillDatas.Length; i++)
            {
                InitSkill(skillDatas[i]);
            }
        }

        //初始化技能
        public void InitSkill(SkillData skillData)
        {
            /*
            资源映射表用于管理路径
            资源名称   资源完整路径
            用代码去生成这个表
            BaseMeleeAttackSkill=Skills/BaseMeleeAttackSkill
            */
            //根据预置体的名字找到预置体的对象,通过resource 文件夹下面的预置体生成路径.如果需要热更新，就需要用asset bundle，而不是这个resources
            // skillData.skillPrefab = Resources.Load<GameObject>("Skill/" + skillData.prefabName);
            skillData.skillPrefab = ResourceManager.Load<GameObject>(skillData.prefabName);
            skillData.owner = gameObject;//因为这个脚本是挂在到角色上的，所以这个gameObject就是角色
        }

        //技能释放条件：冷却，魔法值够不够
        public SkillData PrepareSkill(int skillID)
        {
            //according to the id to find the skill
            SkillData skillData = skillDatas.Find(s => s.skillID == skillID); //需要研究一下
            //这是演示委托的代码，没有用
            // SkillData data = FindSkill(s => s.skillID == skillID);

            float sp = GetComponent<Character.CharacterStatus>().MP;
            if (skillData != null && skillData.coolTimeCounter <= 0 && skillData.costMP <= sp)
            {
                return skillData;
            }
            return null;
        }
        private SkillData FindSkill(Func<SkillData, bool> handler)
        {
            for (int i = 0; i < skillDatas.Length; i++)
            {
                if (handler(skillDatas[i])) return skillDatas[i];
            }
            return null;
        }

        //生成技能
        public void CreateSkill(SkillData data)
        {
            //创建技能预置体
            // GameObject skillGo = Instantiate(data.skillPrefab, transform.position, transform.rotation);
            GameObject skillGo = GameObjectPool.Instance.CreateObject(data.skillID.ToString(), data.skillPrefab, transform.position, transform.rotation);

            SkillCaster skillCaster = skillGo.GetComponent<SkillCaster>();
            //传递技能数据
            skillCaster.SkillData = data; //内部创建算法对象，在赋值的这一刻
            skillCaster.ApplySkill();

            //如果技能是持续就在持续时间后销毁技能（以后要移走）
            // Destroy(skillGo, data.durationTime);
            GameObjectPool.Instance.RecycleObject(skillGo, data.durationTime);

            //技能冷却
            StartCoroutine(CoolTimeDown(data));
        }

        private IEnumerator CoolTimeDown(SkillData data)
        {
            data.coolTimeCounter = data.coolTime;
            while (data.coolTimeCounter > 0)
            {
                yield return new WaitForSeconds(1);
                data.coolTimeCounter--;
            }

        }
    }
}
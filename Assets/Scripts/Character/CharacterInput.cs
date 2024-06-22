using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameName.Skill;
namespace GameName.Character
{
    public class CharacterInput : MonoBehaviour
    {
        // private ETCJoustick eTCJoustick;
        // private CharacterMotor chMototor;
        private Animator animator;
        private CharacterStatus chStatus;
        // private ETCButton[] skillButtons;
        private void OnEnable()
        {
            // joystick.OnMove.AddListener(OnJoystickMove);
            // joystick.OnStart.AddListener(OnJoystickStart);
            // joystick.OnEnd.AddListener(OnJoystickEnd);

            // for(int i=0;i<skillButtons.Length;i++){
            //     skillButtons[i].OnDown.AddListener(OnSkillButtonDown);
            // }
        }
        private void OnDisable()
        {
            // joystick.OnMove.RemoveListener(OnJoystickMove);
            // joystick.OnStart.RemoveListener(OnJoystickStart);
            // joystick.OnEnd.RemoveListener(OnJoystickEnd);

            // for(int i=0;i<skillButtons.Length;i++){
            //     skillButtons[i].OnDown.RemoveListener(OnSkillButtonDown);
            // }
        }
        //多个按钮绑定一个方法，就需要事件参数，区分点击按钮
        private void OnSkillButtonDown(string buttonName)
        {
            int id = 0; //实际上这个id是从按钮上去取的
            switch (name)
            {
                case "BaseButton1":
                    id = 1001;
                    break;
                case "BaseButton2":
                    id = 1002;
                    break;
                case "BaseButton3":
                    id = 1003;
                    break;
            }
            PlayerSkillManager playerSkillManager = GetComponent<Skill.PlayerSkillManager>();
            SkillData data = playerSkillManager.PrepareSkill(id);
            if (data != null)
            {
                playerSkillManager.CreateSkill(data);
            }
        }
    }
}
using UnityEngine;

namespace GameName.Character
{
  public class CharacterStatus : MonoBehaviour
  {
    public float HP;
    public float maxHP;
    public float MP;
    public float maxMP;
    public float baseAttack;
    public float baseDefense;
    public float attackInterval;//攻击间隔，有点像攻击速度
    public float attackDistance;//攻击距离

    public void TakeDamage(float damage)
    {
      HP -= damage - baseDefense;
    }

    protected virtual void Death()
    {
      GetComponent<Animator>().SetBool(chParams.death, true);
    }
  }
}
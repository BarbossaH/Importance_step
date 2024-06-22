using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    首先一个技能就是一个类：技能包含数据，和方法。方法就是比如伤害对方的生命，提升攻击力状态，减少敌人防御的状态，眩晕对方等等。
    
    因为技能的方法非常可能有重复的，所以需要将方法单独提取出来。不然类就有很多重复的代码。也不能使用父类，因为技能之间类不完全相同，所有父类不能完全满足所有需求，灵活度不高。

    如何思考相同和不同？
    技能方法的类：所以把技能的方法封装成一个类，比如把嘲讽设计成一个类，继承一个父类，用父类去生成这些技能效果的类（工厂模式，或者变成接口）
    如何释放技能：可能施放技能需要多种方式，比如近身释放，远程释放等等，这也许需要考虑父类去管理释放方式


*/
#region 如何实际释放技能流程
/*
攻击方：有数个技能数据；包含多个技能；攻击方有技能数据类和技能管理器（复杂技能列表管理和生成技能）。这个生成技能就是把技能的预制体创建出来。

中间的媒介：释放器，用于释放技能（挂在技能预置体中，这非常重要）。（每个技能都有一个预置体的，而释放器应该做成脚本挂在这些技能预置体中）释放器主要包含：创建算法，和执行算法。
释放器根据什么创建算法？首选创建技能数据类，这个技能数据类是策划配置的（程序读取配置文件）。如果只是把这些类写成单独的类，这个类包含了所有的技能效果。技能效果也是一个类？比如造成伤害，增加buff等等。所有技能释放器创建算法就是创建某个技能效果的类，应该是通过工厂模式或者接口


挨打方：有状态类：包含属性，生命攻击力等等

*/
#endregion

#region 技能细则
/*
技能数据类：不用挂在任何对象上
技能管器：技能管理类挂在角色身上，首先明确这是arpg游戏
释放器：挂在技能预置体上。释放器是技能管理器创建出来的。技能管理器在创建释放器的时候，要传递技能数据，这样就可以创建技能释放器算法和执行算法。所以也需要一个父类。
各种技能效果类：普通c#类，不要挂在哪里
（个人感觉，只要牵涉到创建，并且会改变或者拓展的，就必须要使用工厂方法，使用父类创建拓展，通过参数创建子类）
*/
#endregion

#region 界面
/*
UI窗口类 UIWindow
--所有UI窗口的基类，用于层次化的方式管理具体的窗口类
--定义所有窗口共同的行为：显示隐藏，获取事件监听
UI管理类UIManager
--管理（记录，禁用，查找）所有的窗口
UI事件监听器
--提供当前UI所有的事件，具有事件参数类
*/

/*使用方式
定义UIXXXWindow 类，继承UIWindow，负责处理该窗口逻辑；
通过窗口基类的 GetUIEventListener的方法获取需要交互的UI事件监听器
通过事件监听器UIEventListener 提供的各种事件，实现交互行为。
通过UIManager访问各个窗口成员 UIManager.Instance.GetWindow<窗口类型>().成员
*/

/*
UIManager: AddWindow,GetWindow<T>
UIWindow: 字段：canvasGroup,uiCanvas,uiEventDIC;方法：GetUIEventListener, SetVisible
UIEventListener: 字段：GetListener;事件：PointerClick, PointerMove, PointerUp, PointerDown,
*/
#endregion
public class Basics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
/*
技能预制体（Skill Prefab）通常用于定义和封装游戏中的技能效果。它们通常包含以下元素：

1. **技能特效（Visual Effects）**：
    - **粒子系统（Particle Systems）**：例如火焰、冰霜、闪电等视觉效果。
    - **动画（Animations）**：技能施放时的动画，如角色动作或技能生成物的动画。
    - **灯光效果（Lighting Effects）**：例如爆炸时的光晕或光束效果。

2. **声音效果（Sound Effects）**：
    - **施法声音（Casting Sounds）**：技能施放时的声音效果。
    - **命中声音（Impact Sounds）**：技能命中目标时的声音效果。

3. **碰撞和触发器（Colliders and Triggers）**：
    - **碰撞检测（Collision Detection）**：用于检测技能是否命中目标。
    - **触发器（Triggers）**：用于触发特定的事件或效果，例如击中敌人后触发伤害计算。

4. **脚本（Scripts）**：
    - **技能行为脚本（Skill Behavior Scripts）**：定义技能的行为逻辑，例如施放、飞行路径、命中效果等。
    - **伤害计算脚本（Damage Calculation Scripts）**：处理技能的伤害计算和应用。

5. **附加效果（Additional Effects）**：
    - **状态效果（Status Effects）**：如中毒、减速、眩晕等效果。
    - **增益/减益（Buffs/Debuffs）**：如增加攻击力、减少防御力等。

6. **UI元素（UI Elements）**：
    - **技能冷却时间（Cooldown Timer）**：显示技能冷却时间的UI元素。
    - **技能图标（Skill Icon）**：显示技能图标的UI元素。

### 示例：技能预制体的结构

```yaml
SkillPrefab
  ├── VisualEffects
  │   ├── ParticleSystem
  │   ├── Animation
  │   └── LightingEffect
  ├── SoundEffects
  │   ├── CastingSound
  │   └── ImpactSound
  ├── CollidersAndTriggers
  │   ├── Collider
  │   └── Trigger
  ├── Scripts
  │   ├── SkillBehaviorScript
  │   └── DamageCalculationScript
  ├── AdditionalEffects
  │   ├── StatusEffect
  │   └── BuffDebuff
  └── UIElements
      ├── CooldownTimer
      └── SkillIcon
```

### 实际示例

假设你在Unity中创建了一个火球技能（Fireball Skill）的预制体，它可能包含以下组件：

1. **VisualEffects**：
    - 一个粒子系统（Particle System）来显示火焰。
    - 一个动画（Animation）来显示火球的飞行路径。

2. **SoundEffects**：
    - 一个施法声音（Casting Sound）来播放火球生成时的音效。
    - 一个命中声音（Impact Sound）来播放火球击中目标时的音效。

3. **CollidersAndTriggers**：
    - 一个碰撞体（Collider）来检测火球是否击中目标。
    - 一个触发器（Trigger）来处理火球命中后的效果。

4. **Scripts**：
    - 一个技能行为脚本（Skill Behavior Script）来控制火球的生成、飞行和消失。
    - 一个伤害计算脚本（Damage Calculation Script）来计算火球对目标造成的伤害。

5. **AdditionalEffects**：
    - 一个状态效果（Status Effect）脚本来处理火球命中目标后造成的灼烧效果。

6. **UIElements**：
    - 一个技能冷却时间（Cooldown Timer）UI元素来显示火球技能的冷却时间。
    - 一个技能图标（Skill Icon）来显示火球技能的图标。

通过这些组件，技能预制体可以提供完整的技能效果和行为，确保在游戏中正确地表现出技能的视觉、音效和逻辑效果。
*/
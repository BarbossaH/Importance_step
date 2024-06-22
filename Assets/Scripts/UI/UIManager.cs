
// public class UIManager : MonoSingleton<UIManager>
// {
//   private Dictionary<string, UIWindow> uiWindowDIC;
//   public override void Init()
//   {
//     base.Init();
//     uiWindowDIC = new Dictionary<string, UIWindow>();
//     UIWindow[] uiWindowArray = FindObjectsOfType<UIWindow>();
//     for (int i = 0; i < uiWindowArray.Length; i++)
//     {
//       uiWindowArray[i].SetVisible(false);
//       AddWindow(uiWindowArray[i]);
//     }
//   }

//   public void AddWindow(UIWindow uiWindow)
//   {
//     uiWindowDIC.Add(uiWindow);
//   }
//   public T GetWindow<T>() where T : class
//   {
//     string key = typeof(T).Name; //类名
//     if (!uiWindowDIC.ContainsKey(key)) return null;
//     return uiWindowDIC[key] as T;
//   }
// }
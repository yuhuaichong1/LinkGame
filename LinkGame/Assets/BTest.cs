using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XrCode;
using static UnityEngine.Application;

public class BTest : MonoBehaviour
{
    void Start()
    {
       
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.LogError(FacadeGuide.GetCurStep());
        }
    }

    public void Test()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaClass loginClass = new AndroidJavaClass("com.gg.user.Login");
        AndroidJavaObject loginInstance = loginClass.CallStatic<AndroidJavaObject>(
            "getInstance",
            currentActivity,
            new AndroidCallback()
        );

        loginInstance.Call("go");
    }

    public void Test2()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("com.gg.user.Login.getInstance", null);
    }

    public void Test3()
    {
        AndroidJavaClass loginClass = new AndroidJavaClass("com.gg.user.Login");
        AndroidJavaObject loginObject =
            loginClass.CallStatic<AndroidJavaObject>("getInstance", null, new AndroidCallback());
        loginObject.Call("go");
    }
}

//public class AndroidCallback : AndroidJavaProxy
//{
//    public AndroidCallback() : base("com.gg.user.Callback") { }

//    public void onSuccess(AndroidJavaObject obj)
//    {
//        bool go = bool.Parse(obj.Call<string>("toString"));
//        // 处理成功逻辑
//        Debug.LogError($"Success");
//    }


//    public void onSuccess(bool b)
//    {
//        // 处理成功逻辑
//        Debug.LogError($"Success" + b);
//    }

//    public void onFailed(int code, string msg)
//    {
//        Debug.LogError($"code: {code}, msg: {msg}");
//        // 处理失败逻辑
//    }
//}

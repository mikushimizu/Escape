using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject ReturnButton;

    void Update()
    {
        //zoomしたとき
        //[pilkul] 変数が義ローバルである必要を感じない。Get関数とかを作って適宜持ってこれるように使用
        /*
        if (CameraState.State)
        {
            ReturnButton.SetActive(true);
        }
        */
        
    }
    
    //[pilkul] returnはね。あのね。
    public void RunReturn()
    {
        ReturnButton.SetActive(false);
    }
}

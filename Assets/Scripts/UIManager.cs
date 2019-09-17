using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject zoomOutButton;
    PlayerCamera playerCamera;
    private void Start()
    {
        playerCamera = new PlayerCamera();
        Debug.Log(playerCamera.State);
    }

    void Update()
    {
        Debug.Log("UIManager playerCamera.State:" + playerCamera.State);

        //[pilkul] 変数がグローバルである必要を感じない。Get関数とかを作って適宜持ってこれるように使用
        //[shimi43] PlayerCameraではStateがDefault,Zoomで切り替わっているのに、UIManagerでgetしたStateが切り替わらない
        switch (playerCamera.State)
        {
            case PlayerCamera.CameraState.Default:
                break;

            case PlayerCamera.CameraState.Zoom:
                zoomOutButton.SetActive(true);
                break;
        }
    }
    
    public void DeleteZoomOutButton()
    {
        zoomOutButton.SetActive(false);
    }
}
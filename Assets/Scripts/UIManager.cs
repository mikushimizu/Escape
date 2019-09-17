using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject zoomOutButton;
    PlayerCamera playerCamera;

    void Start()
    {
        playerCamera = GameObject.Find("Main Camera").GetComponent<PlayerCamera>();
    }

    void Update()
    {
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
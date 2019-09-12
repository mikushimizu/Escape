using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject ReturnButton;
    
    void Start()
    {

    }

    void Update()
    {
        //zoomしたとき
        if (CameraScript.CameraStatus == "Zoom")
        {
            ReturnButton.SetActive(true);
        }
        
    }
    public void RunReturn()
    {
        ReturnButton.SetActive(false);
    }
}

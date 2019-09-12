using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    GameObject clickedGameObject;
    Vector3 rotationAngle;
    Vector3 defaultPosition;
    Vector3 DistCamToObj;
    float _DistCamToObjCamToObj;
    public static string CameraStatus; //Default, Zoom 視点状態の管理変数

    void Start()
    {
        rotationAngle = new Vector3(0.0f, 0.0f, 0.0f);
        defaultPosition = this.transform.position;
        _DistCamToObjCamToObj = 2.4f;
        CameraStatus = "Default";
    }

    void Update()
    {
        if(CameraStatus == "Default")
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                RotationMove(90.0f); //左に90度回転
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RotationMove(-90.0f); //右に90度回転
            }

            if (Input.GetMouseButton(0))
            {
                Ray();
                ZoomMove();
            }
        }
        if(CameraStatus == "Zoom")
        {
            if (Input.GetMouseButton(0))
            {
                Ray();
                ZoomMove();
                ItemCheck();
                ItemGet();
            }
        }
    }

    void Ray()
    {
        clickedGameObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            clickedGameObject = hit.collider.gameObject;
            //Debug.Log(clickedGameObject.name + ", position:" + clickedGameObject.transform.position);
        }
    }

    void RotationMove(float rotationAngleY)
    {
        //Debug.Log("Input Key   rotationAngle:" + rotationAngle);
        rotationAngle.y += rotationAngleY;
        this.transform.rotation = Quaternion.Euler(rotationAngle);
    }

    void ZoomMove()
    {
        if (clickedGameObject.tag == "Furniture_North")
        {
            DistCamToObj = new Vector3(0, 0, -_DistCamToObjCamToObj);
            CameraStatus = "Zoom";
            this.transform.position = clickedGameObject.transform.position + DistCamToObj;
        }
        else if (clickedGameObject.tag == "Furniture_West")
        {
            DistCamToObj = new Vector3(_DistCamToObjCamToObj, 0, 0);
            CameraStatus = "Zoom";
            this.transform.position = clickedGameObject.transform.position + DistCamToObj;
        }
        else if (clickedGameObject.tag == "Furniture_East")
        {
            DistCamToObj = new Vector3(-_DistCamToObjCamToObj, 0, 0);
            CameraStatus = "Zoom";
            this.transform.position = clickedGameObject.transform.position + DistCamToObj;
        }
        else if (clickedGameObject.tag == "Furniture_South")
        {
            DistCamToObj = new Vector3(0, 0, _DistCamToObjCamToObj);
            CameraStatus = "Zoom";
            this.transform.position = clickedGameObject.transform.position + DistCamToObj;
        }
        else
        {
        }
    }

    public void ReturnMove()
    {
        CameraStatus = "Default";
        this.transform.position = defaultPosition;
    }

    void ItemCheck()
    {
        
        int count = 0;
        GameObject watchedFurniture = clickedGameObject;
        foreach (Transform child in watchedFurniture.transform)
        {
            count++;
            Debug.Log("Child[" + count + "]" + child.name);
            child.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void ItemGet()
    {
        if (clickedGameObject.tag == "Item")
        {
            clickedGameObject.SetActive(false);
            Debug.Log(clickedGameObject);
        }
    }
}

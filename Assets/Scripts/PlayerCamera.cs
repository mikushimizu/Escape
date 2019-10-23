using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour
{
    private GameObject clickedGameObject;
    private Vector3 rotationAngle;
    private Vector3 defaultPosition;
    private Vector3 direction;
    private const float distance = 2.4f; //定数 Zoom時のオブジェクトからカメラまでの距離
    private Camera cam;
    private CameraState cameraState;

    public enum CameraState{
        Default,
        Zoom
    }

    public CameraState State
    {
        get { return cameraState; }
        set { cameraState = value; }
    }

    void Start()
    {
        rotationAngle = new Vector3(0.0f, 0.0f, 0.0f);
        defaultPosition = transform.position;
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        switch (cameraState)
        {
            case CameraState.Default:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Rotate(90.0f); //左に90度回転
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Rotate(-90.0f); //右に90度回転
                }

                if (Input.GetMouseButton(0))
                {
                    Ray();
                    Zoom();
                }
                break;

            case CameraState.Zoom:
                if (Input.GetMouseButtonDown(0))
                {
                    Ray();
                    Zoom();
                    CheckItem();
                    ObtainItem();
                }
                break;
        }
    }

    void Ray()
    {
        clickedGameObject = null;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            clickedGameObject = hit.collider.gameObject;
        }
    }

    void Rotate(float addAngle)
    {
        rotationAngle.y += addAngle;
        transform.rotation = Quaternion.Euler(rotationAngle);
    }

    void Zoom()
    {
        if(isFurniture(clickedGameObject.tag)){
            switch (clickedGameObject.tag)
            {
                case "Furniture_North":
                    direction = new Vector3(0, 0, -distance);

                    break;

                case "Furniture_West":
                    direction = new Vector3(distance, 0, 0);
                    break;

                case "Furniture_East":
                    direction = new Vector3(-distance, 0, 0);

                    break;

                case "Furniture_South":
                    direction = new Vector3(0, 0, distance);
                    break;
            }
            cameraState = CameraState.Zoom;
            transform.position = clickedGameObject.transform.position + direction;
        }
    }
    
    public void ZoomOut()
    {
        cameraState = CameraState.Default;
        transform.position = defaultPosition;
    }

    void CheckItem()
    {
        int count = 0;
        GameObject watchedFurniture = clickedGameObject;
        foreach (Transform child in watchedFurniture.transform)
        {
            count++;
            //childをGameObjectで取得しない。共通インターフェースを切ってBoxColliderを返す関数を作るべき
            //=> GetComponentなどの重い処理を回避できる
            child.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void ObtainItem()
    {
        switch (clickedGameObject.tag)
        {
            case "Item":
                //単一原則から、オブジェクトにクリックされたという通知を行い、実際の処理はアイテム側に記述すべき
                clickedGameObject.SetActive(false);
                break;
        }
    }
    
    bool isFurniture(string tag){
        if(tag=="Furniture_East" || tag == "Furniture_West" || tag == "Furniture_North" || tag == "Furniture_South") {return true;}
        else {return false;}
    }
}

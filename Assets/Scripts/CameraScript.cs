using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    Vector3 rotationAngle;
    GameObject clickedGameObject;
    Vector3 defaultPosition;
    public static string CameraStatus; //Default, Zoom

    void Start()
    {
        rotationAngle = new Vector3(0.0f, 0.0f, 0.0f);
        CameraStatus = "Default";
        defaultPosition = this.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            clickedGameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                clickedGameObject = hit.collider.gameObject;
                //Debug.Log(clickedGameObject.name + ", position:" + clickedGameObject.transform.position);
                // クリックしたオブジェクトの情報(名前、位置など)

                if(CameraStatus == "Default")
                {
                    //東西南北によってカメラとオブジェクトの位置関係が変わる
                    if (clickedGameObject.tag == "Furniture_North")
                    {
                        ZoomMove(0, 0, -2.4f);
                    }
                    if (clickedGameObject.tag == "Furniture_West")
                    {
                        ZoomMove(1.4f, 0, 0);
                    }
                    if (clickedGameObject.tag == "Furniture_East")
                    {
                        ZoomMove(-1.4f, 0, 0);
                    }
                    if (clickedGameObject.tag == "Furniture_South")
                    {
                        ZoomMove(0, 0, 2.4f);
                    }
                }
                if(CameraStatus == "Zoom")
                {
                    Debug.Log(clickedGameObject.name);
                    int count = 0;
                    GameObject watchedFurniture = clickedGameObject;
                    foreach(Transform child in watchedFurniture.transform)
                    {
                        count++;
                        Debug.Log("Child[" + count + "]" + child.name);
                        child.gameObject.GetComponent<BoxCollider>().enabled = true;
                    }
                    if (clickedGameObject.tag == "Item")
                    {
                        clickedGameObject.SetActive(false);
                        Debug.Log(clickedGameObject);
                    }
                }
            }
            
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
            RotationMove(90.0f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotationMove(-90.0f);
        }
    }

    void RotationMove(float rotationAngleY)
    {
        Debug.Log("Input Key   rotationAngle:" + rotationAngle);
        rotationAngle.y += rotationAngleY; //左隣の壁を向く
        this.transform.rotation = Quaternion.Euler(rotationAngle);
    }

    void ZoomMove(float distX, float distY, float distZ)
    {
        CameraStatus = "Zoom";
        this.transform.position = clickedGameObject.transform.position + new Vector3(distX, distY, distZ);
    }

    public void ReturnMove()
    {
        CameraStatus = "Default";
        this.transform.position = defaultPosition;
    }
}

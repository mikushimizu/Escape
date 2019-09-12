using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[pilkul] スクリプトであることは拡張子やアイコンから自明。よってScriptという名前をつけるのは冗長。
public class CameraScript : MonoBehaviour
{
    //[pilkul] 変数にはアクセス修飾子(privateやpublic)を明示的につける
    GameObject clickedGameObject;
    Vector3 rotationAngle;
    Vector3 defaultPosition;
    
    //[pilkul] 変数名の規則にキャメルとパスカルが入り混じっている
    Vector3 DistCamToObj;
    
    // 変数前の_の命名規則が不明。略語とか使ってるし記事丸パクリなのかな？んん？？
    float _DistCamToObjCamToObj;
    
    //[pilkul] ステータスという変数名に文字列なのは謎。ステータスを表すなら構造体、状態を表すなら列挙型(enum)を使うべき 
    public static string CameraStatus; //Default, Zoom 視点状態の管理変数
    //[shimi]このString型に直接文字列を打ち込んでるダサい状態管理をどうにかしたい
    //[shimi]クラス？enum？
    
    /*
    こんな感じ
    enum CameraState{
        Default,
        Zoom
    }
    CameraSatate cameraStatus;
    */

    void Start()
    {
        //[pilkul] Vector3.zero 見たいのあったべ。それ使おう。
        rotationAngle = new Vector3(0.0f, 0.0f, 0.0f);
        defaultPosition = this.transform.position;
        
        //[pilkul] 定数は "const float 変数名 = 値  // 値の意味" みたいな形で宣言するのがお作法
        _DistCamToObjCamToObj = 2.4f;
        CameraStatus = "Default";
    }

    void Update()
    {
    //[pilkul] 状態の管理にif文を使うべきではない。switch文で妥協点。理想はデザインパターンのステートパターンやストラテジーパターンを用いる
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
            //[pilkul] コメントアウトは残さない
            //Debug.Log(clickedGameObject.name + ", position:" + clickedGameObject.transform.position);
        }
    }

    //[pilkul] 関数名は動詞が先。
    // => インスタンス名が名詞推奨であるため、インスタンス.関数(引数)がでソースコードをSVCやSVOの文型の英文であるかのように読める
    void RotationMove(float rotationAngleY)
    {
        //[pilkul] コメントアウトは残さない
        //Debug.Log("Input Key   rotationAngle:" + rotationAngle);
        
        //[pilkul] 読みにくい。引数の命名が下痢
        rotationAngle.y += rotationAngleY;
        
        //[pilkul] thisいる？冗長じゃね？
        this.transform.rotation = Quaternion.Euler(rotationAngle);
    }

    //[pilkul] 関数名は動詞が先。
    void ZoomMove()
    {
    // 分岐はswitchもしくはステート、ストラテジー
        if (clickedGameObject.tag == "Furniture_North")
        {
            DistCamToObj = new Vector3(0, 0, -_DistCamToObjCamToObj);
            CameraStatus = "Zoom";
            
            //[pilkul] thisいる？冗長じゃね？
            this.transform.position = clickedGameObject.transform.position + DistCamToObj;
        }
        else if (clickedGameObject.tag == "Furniture_West")
        {
            DistCamToObj = new Vector3(_DistCamToObjCamToObj, 0, 0);
            CameraStatus = "Zoom";
            
            //[pilkul] thisいる？冗長じゃね？
            this.transform.position = clickedGameObject.transform.position + DistCamToObj;
        }
        else if (clickedGameObject.tag == "Furniture_East")
        {
            DistCamToObj = new Vector3(-_DistCamToObjCamToObj, 0, 0);
            CameraStatus = "Zoom";
            
            //[pilkul] thisいる？冗長じゃね？
            this.transform.position = clickedGameObject.transform.position + DistCamToObj;
        }
        else if (clickedGameObject.tag == "Furniture_South")
        {
            DistCamToObj = new Vector3(0, 0, _DistCamToObjCamToObj);
            CameraStatus = "Zoom";
            
            //[pilkul] thisいる？冗長じゃね？
            this.transform.position = clickedGameObject.transform.position + DistCamToObj;
        }
        else
        {
        //[pilkul] 謎の空間
        }
        
        //[pilkul] CameraStatus = "Zoom";を4回も書いてる。ifの前後どちらかに書けば1回で済む。
        //Zoomにならない可能性があるならこの関数を呼ぶべきではないし、どうしても呼びたなら早期returnも有効
    }

    //[pilkul] 関数名にリターンを使うのはreturnが既にあるため微妙。関係性を感じてしまう。あと名前から何やってるのかわからない
    public void ReturnMove()
    {
        CameraStatus = "Default";
        
        //[pilkul] thisいる？冗長じゃね？      
        this.transform.position = defaultPosition;
    }

    //[pilkul] 関数名は動詞が先。
    void ItemCheck()
    {
        int count = 0;
        GameObject watchedFurniture = clickedGameObject;
        foreach (Transform child in watchedFurniture.transform)
        {
            count++;
            
            //[pilkul] "Debug"を残さない
            //[pilkul] Debugを消すとcountも消えるので最高。count残しはforeachを活かせない
            Debug.Log("Child[" + count + "]" + child.name);
            
            //[pilkul] childをGameObjectで取得するのはゲロ。共通インターフェースを切ってBoxColliderを返す関数を作るべき
            //[pilkul] => GetComponentなどの重い処理を回避できる
            child.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    //[pilkul] 関数名は動詞が先。
    //[pilkul] GetやSetはプログラミングに置いて重要な意味を持つのでこういった使い方は誤解を招く
    void ItemGet()
    {
        //[pilkul] 分岐の可能性があるならswitch、ステートもしくはストラテジー
        if (clickedGameObject.tag == "Item")
        {
            //[pilkul] 単一原則から、オブジェクトにクリックされたという通知を行い、実際の処理はアイテム側に記述すべき
            clickedGameObject.SetActive(false);
            
            //[pilkul] "Debug"を残さない
            Debug.Log(clickedGameObject);
        }
    }
    /*
    bool isFurniture(string tag){
     家具系のタグなら return true;
     家具系以外のタグなら return false
    }
    */
}

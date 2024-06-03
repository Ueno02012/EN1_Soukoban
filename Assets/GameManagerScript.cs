//using JetBrains.Annotations;
//using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEditor.Compilation;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    //配列の宣言
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefab;
    public GameObject GoalPrefab;
    public GameObject Particlefab;

    public GameObject clearText;

    int[,] map;//レベルデザイン用の配列
    GameObject[,] field;//ゲーム管理用の配列

    Vector2Int GetPlayerIndex()
    {

        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null) { continue; }
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }


    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)//移動の可不可
    {

        //移動先が範囲外なら移動不可
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

      
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }
        Vector3 moveToPosition = new Vector3(
              moveTo.x, map.GetLength(0) - moveTo.y, 0
              );
        field[moveFrom.y, moveFrom.x].GetComponent<Move>().MoveTo(moveToPosition);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;

        

        return true;
    }




    void Start()
    {
        //GameObject instance = Instantiate(playerPrefab,
        //    new Vector3(0, 0, 0),
        //    Quaternion.identity
        //);

        Screen.SetResolution(1280, 720, false);

        map = new int[,]
        {
           {0,0,0,0,0,0,0,0,0,0,0,0},
           {0,0,0,3,0,3,0,0,0,0,0,0},
           {0,0,0,0,2,3,0,0,0,3,0,0},
           {0,2,0,0,0,2,0,0,0,0,0,0}, 
           {0,0,1,0,0,0,0,2,0,0,0,0},
           {0,0,0,0,3,0,0,0,0,0,2,0},
           {0,0,0,0,0,0,0,0,0,0,0,0}
        };
        field = new GameObject
        [
            map.GetLength(0),
            map.GetLength(1)
        ];

        string debugText = "";

        Debug.Log(debugText);
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {

                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate(
                        playerPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                       );
                }
                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(
                        boxPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }
                if (map[y, x] == 3)
                {
                    field[y, x] = Instantiate(
                        GoalPrefab,
                        new Vector3(x, map.GetLength(0) - y, 1),
                        Quaternion.identity);
                }

                debugText += map[y, x].ToString() + ",";
            }
            debugText += "\n";
        }
        Debug.Log(debugText);
    }
    bool IsCleard()
    {
        List<Vector2Int>goals= new List<Vector2Int>();

        for(int y = 0; y < map.GetLength(0); y++)
        {
            for(int x = 0;x < map.GetLength(1); x++)
            {
                if (map[y, x] == 3)
                {
                    //格納場所のインデックスを控えておく
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }
        for(int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                return false;
            }
        }

        return true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //メソッド化した処理
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber( playerIndex, playerIndex + new Vector2Int(1, 0));
            
            //PrintArray();
        }
        //左の移動
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber( playerIndex, playerIndex - new Vector2Int(1,0));
            //PrintArray();
        }

        //上
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex - new Vector2Int(0, 1));
        }//下
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));
        }

        if (IsCleard())
        {
            clearText.SetActive(true);
        }

    }
}
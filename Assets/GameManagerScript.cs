using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    //配列の宣言
    int[] map;
    int GetPlayerIndex()
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return 1;
            }
        }
        return -1;
    }

    void PrintArray()
    {
        //文字列の宣言と初期化
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            //文字列に結合していく
            debugText += map[i].ToString() + ",";
        }
        //結合した文字列を出力
        Debug.Log(debugText);
    }

    bool MoveNumber(int number,int moveFrom,int moveTo)//移動の可不可
    {
        if (moveTo < 0 || moveTo >= map.Length)
        {
            //動けない条件を先に書き、リターンする。早期リターン
            return false;
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }
    void Start()
    {
        map = new int[] { 0, 0, 0, 1, 0, 0, 0, 0, 0};
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //メソッド化した処理
            int playerIndex = GetPlayerIndex();

            //移動処理を関数化
            MoveNumber(1,playerIndex,playerIndex+1);
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] == 1)
                {
                    playerIndex = i;
                    break;
                }
            }
            /*playerIndex+1のインデックスの物と交換するので、
              playerIndex-1よりさらに小さいインデックスの時
　            のみ交換処理をする
            */
            if (playerIndex < map.Length - 1)
            {
                map[playerIndex + 1] = 1;
                map[playerIndex] = 0;
            }
            PrintArray();
        }
        //左の移動
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = GetPlayerIndex();

            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] == 1)
                {
                    playerIndex = i;
                    break;
                }
            }
            if (playerIndex >0)
            {
                map[playerIndex -1] = 1;
                map[playerIndex] = 0;
            }
            PrintArray();
        }

    }
}

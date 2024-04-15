using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    //�z��̐錾
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
        //������̐錾�Ə�����
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            //������Ɍ������Ă���
            debugText += map[i].ToString() + ",";
        }
        //����������������o��
        Debug.Log(debugText);
    }

    bool MoveNumber(int number,int moveFrom,int moveTo)//�ړ��̉s��
    {
        if (moveTo < 0 || moveTo >= map.Length)
        {
            //�����Ȃ��������ɏ����A���^�[������B�������^�[��
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
            //���\�b�h����������
            int playerIndex = GetPlayerIndex();

            //�ړ��������֐���
            MoveNumber(1,playerIndex,playerIndex+1);
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] == 1)
                {
                    playerIndex = i;
                    break;
                }
            }
            /*playerIndex+1�̃C���f�b�N�X�̕��ƌ�������̂ŁA
              playerIndex-1��肳��ɏ������C���f�b�N�X�̎�
�@            �̂݌�������������
            */
            if (playerIndex < map.Length - 1)
            {
                map[playerIndex + 1] = 1;
                map[playerIndex] = 0;
            }
            PrintArray();
        }
        //���̈ړ�
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

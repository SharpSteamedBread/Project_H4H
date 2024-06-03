using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEntity : MonoBehaviour
{
    private string entityName;

    //외부에서 에이전트 이름 정보를 열람할 수 있도록 Get 프로퍼티 정의
    public string EntityName => entityName;

    public virtual void Setup()
    {
        entityName = name;
    }
    
    public abstract void Updated();

    public void PrintText(string text)
    {
        Debug.Log($"Player_Idle");
    }

}

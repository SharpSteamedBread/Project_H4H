using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEntity : MonoBehaviour
{
    private string entityName;

    //�ܺο��� ������Ʈ �̸� ������ ������ �� �ֵ��� Get ������Ƽ ����
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

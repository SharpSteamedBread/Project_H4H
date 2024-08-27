using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageManager;

public enum PlayerDamageType
{
    ComboAttackZ, ComboAttackX,
    PlayerSkill1, PlayerSkill2, PlayerSkill3, PlayerSkill4, PlayerSkill5, PlayerSkill6
}

public enum ObjectDamageType
{
    Object_hide, Object_falling, Object_spinning, Object_thorn,
    Object_Beam
}

public enum EnemyDamageType
{
    Enemy2, Enemy3, Enemy4
}

public enum MidbossDamageType
{
    Pattern1, Pattern2, Pattern3, Pattern4, Pattern5
}

public class DamageInteractor : MonoBehaviour
{
    public int baseDamage;
    public int damageResult;

    public int criticalRate;
    public int randomCritical;

    public PlayerDamageType playerDamageType;
    public ObjectDamageType objectDamageType;
    public EnemyDamageType enemyDamageType;
    public MidbossDamageType midbossDamageType;


    public int CalculateDamage()
    {
        switch (playerDamageType)
        {
            case PlayerDamageType.ComboAttackZ:
                baseDamage = 23;
                break;
            case PlayerDamageType.ComboAttackX:
                baseDamage = 23;
                break;
            case PlayerDamageType.PlayerSkill1:
                baseDamage = 30;
                break;
            case PlayerDamageType.PlayerSkill2:
                baseDamage = 30;
                break;
            case PlayerDamageType.PlayerSkill3:
                baseDamage = 40;
                break;
            case PlayerDamageType.PlayerSkill4:
                baseDamage = 40;
                break;
            case PlayerDamageType.PlayerSkill5:
                baseDamage = 70;
                break;
            case PlayerDamageType.PlayerSkill6:
                baseDamage = 55;
                break;
            default:
                baseDamage = 23;
                break;
        }

        int randomCritical = Random.Range(1, 101);

        if (randomCritical < criticalRate)
        {
            damageResult = baseDamage * 2;
        }

        else
        {
            damageResult = baseDamage;
        }

        //Debug.Log($"가한 피해는 {damageResult}!");
        return damageResult;
    }

    public int GetDamageFromObj()
    {
        switch(objectDamageType)
        {
            case ObjectDamageType.Object_hide:
                baseDamage = 30;
                break;
            case ObjectDamageType.Object_falling:
                baseDamage = 25;
                break;
            case ObjectDamageType.Object_spinning:
                baseDamage = 20;
                break;
            case ObjectDamageType.Object_Beam:
                baseDamage = 40;
                break;
            case ObjectDamageType.Object_thorn:
                baseDamage = 7;
                break;
            default:
                baseDamage = 10;
                break;
        }

        damageResult = baseDamage;
        return damageResult;
    }

    public int GetDamageFromEnemy()
    {
        switch(enemyDamageType)
        {
            case EnemyDamageType.Enemy2:
                baseDamage = 3;
                break;
            case EnemyDamageType.Enemy3:
                baseDamage = 140;
                break;
            case EnemyDamageType.Enemy4:
                baseDamage = 120;
                break;
            default:
                baseDamage = 50;
                break;

        }

        damageResult = baseDamage;
        return damageResult;
    }

    public int GetDamageFromMidboss()
    {
        switch(midbossDamageType)
        {
            case MidbossDamageType.Pattern1:
                baseDamage = 100;
                break;
            case MidbossDamageType.Pattern2:
                baseDamage = 60;
                break;
            case MidbossDamageType.Pattern3:
                baseDamage = 60;
                break;
            case MidbossDamageType.Pattern4:
                baseDamage = 60;
                break;
            case MidbossDamageType.Pattern5:
                baseDamage = 60;
                break;
            default:
                baseDamage = 60;
                break;
        }
        damageResult = baseDamage;
        return damageResult;
    }

    private IEnumerator RandomNumber()
    {
        randomCritical = Random.Range(1, 101);
        yield return new WaitForSeconds(0.1f);
    }

    
}

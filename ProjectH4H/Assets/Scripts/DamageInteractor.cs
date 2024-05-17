using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageManager;

public enum PlayerDamageType
{
    ComboAttackZ, ComboAttackX,
    PlayerSkill1, PlayerSkill2, PlayerSkill3, PlayerSkill4
}

public class DamageInteractor : MonoBehaviour
{
    public int baseDamage;
    public int damageResult;

    public int criticalRate;
    public int randomCritical;

    public PlayerDamageType playerDamageType;

    public int CalculateDamage()
    {
        switch (playerDamageType)
        {
            case PlayerDamageType.ComboAttackZ:
                baseDamage = 10;
                break;
            case PlayerDamageType.ComboAttackX:
                baseDamage = 20;
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
            default:
                baseDamage = 10;
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

        return damageResult;
    }

    private IEnumerator RandomNumber()
    {
        randomCritical = Random.Range(1, 101);
        yield return new WaitForSeconds(0.1f);
    }

    
}

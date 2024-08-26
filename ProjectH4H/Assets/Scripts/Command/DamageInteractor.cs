using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageManager;

public enum PlayerDamageType
{
    ComboAttackZ, ComboAttackX,
    PlayerSkill1, PlayerSkill2, PlayerSkill3, PlayerSkill4, PlayerSkill5, PlayerSkill6
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

    private IEnumerator RandomNumber()
    {
        randomCritical = Random.Range(1, 101);
        yield return new WaitForSeconds(0.1f);
    }

    
}

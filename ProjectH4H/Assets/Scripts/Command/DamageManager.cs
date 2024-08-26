using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDamageTypeTest { ComboAttackZ, ComboAttackX, 
                               PlayerSkill1, PlayerSkill2, PlayerSkill3, PlayerSkill4 }

public class DamageManager
{
    public float baseDamage;
    public PlayerDamageTypeTest playerDamageType;

    [System.Serializable]
    public class DamageModel
    {
        public float baseDamage;
        public PlayerDamageTypeTest playerDamageType;

        public DamageModel(float baseDamage, PlayerDamageTypeTest playerDamageType)
        {
            this.baseDamage = baseDamage;
            this.playerDamageType = playerDamageType;
        }
    }

    public float CalculateDamage()
    {
        switch (playerDamageType)
        {
            case PlayerDamageTypeTest.ComboAttackZ:
                baseDamage = 10;
                break;
            case PlayerDamageTypeTest.ComboAttackX:
                baseDamage = 20;
                break;
            case PlayerDamageTypeTest.PlayerSkill1:
                baseDamage = 30;
                break;
            case PlayerDamageTypeTest.PlayerSkill2:
                baseDamage = 30;
                break;
            case PlayerDamageTypeTest.PlayerSkill3:
                baseDamage = 40;
                break;
            case PlayerDamageTypeTest.PlayerSkill4:
                baseDamage = 40;
                break;
            default:
                baseDamage = 10;
                break;
        }
        return baseDamage;
    }


    public interface IDamageable
    {
        void TakeDamage(DamageModel damageModel);
    }

    public class DamageController
    {
        public static void PutDamage(IDamageable damageable, DamageModel damageModel)
        {
            damageable.TakeDamage(damageModel);
        }
    }
}

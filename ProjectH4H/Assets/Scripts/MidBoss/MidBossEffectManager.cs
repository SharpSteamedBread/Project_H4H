using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossEffectManager : MonoBehaviour
{
    [SerializeField] private Transform midbossFlip;

    [Header("이펙트")]
    [SerializeField] private Transform objVFXLocationPattern1;
    [SerializeField] private GameObject objVFXPattern1;
    [SerializeField] private BoxCollider2D fallingRange;
    [SerializeField] private int fallingCount = 7;
    [SerializeField] private float fallingTerm = 0.3f;
    [SerializeField] private GameObject objVFXPattern1Falling;

    [Space(10)]
    [SerializeField] private Transform objVFXLocationPattern2;
    [SerializeField] private GameObject objVFXPattern2;

    [Space(10)]
    [SerializeField] private Transform objVFXLocationPattern3Spark;
    [SerializeField] private GameObject objVFXPattern3Spark;
    [SerializeField] private Transform objVFXLocationPattern3Projectile;
    [SerializeField] private GameObject objVFXPattern3Projectile;


    [Space(10)]
    [SerializeField] private Transform objVFXLocationPattern4;
    [SerializeField] private GameObject objVFXPattern4;

    [Header("히트박스")]
    [SerializeField] private DamageInteractor damageInteractor;


    private void Awake()
    {
        midbossFlip = gameObject.GetComponent<Transform>();
        damageInteractor.GetComponent<DamageInteractor>();

        
    }

    void Update()
    {
        damageInteractor.GetComponent<DamageInteractor>();
    }

    public void Pattern1EFF()
    {
        GameObject cloneVFXLocationPTN1 = Instantiate(objVFXPattern1, objVFXLocationPattern1.transform.position, objVFXLocationPattern1.transform.rotation);
        damageInteractor.midbossDamageType = MidbossDamageType.Pattern1;
    }

    public void Pattern1Falling()
    {
        StartCoroutine(Pattern1FallingObj());
    }

    public void Pattern2EFF()
    {
        objVFXPattern2.SetActive(true);
        damageInteractor.midbossDamageType = MidbossDamageType.Pattern2;
    }

    public void Pattern3EFF()
    {
        GameObject cloneVFX3Spark = Instantiate(objVFXPattern3Spark, objVFXLocationPattern3Spark.transform.position, objVFXLocationPattern3Spark.transform.rotation);
        GameObject cloneVFX3Projectile = Instantiate(objVFXPattern3Projectile, objVFXLocationPattern3Projectile.transform.position, objVFXLocationPattern3Projectile.transform.rotation);
    }

    public void Pattern4EFF()
    {
        GameObject cloneVFXLocationPTN4 = Instantiate(objVFXPattern4, objVFXLocationPattern4.transform.position, objVFXLocationPattern4.transform.rotation);
        damageInteractor.midbossDamageType = MidbossDamageType.Pattern4;
    }

    public IEnumerator Pattern1FallingObj()
    {
        for (int i = 0; i < 7; i++)
        {
            float fallingRangeX = fallingRange.bounds.size.x;
            float fallingRangeY = fallingRange.bounds.size.y;

            fallingRangeX = Random.Range(-(fallingRangeX / 2), (fallingRangeX / 2));
            fallingRangeY = Random.Range(-(fallingRangeY / 2), (fallingRangeY / 2));

            GameObject cloneFalling = Instantiate(objVFXPattern1Falling, new Vector3((objVFXLocationPattern1.position.x - fallingRangeX) + fallingRange.offset.x, 114, 0), Quaternion.identity);
            cloneFalling.GetComponent<ParticleSystem>().Play();

            yield return new WaitForSeconds(fallingTerm);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFourthSummon : MonoBehaviour
{
    [SerializeField] private MaterialPropertyBlock objMPB;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float stepValue = 0f;

    [SerializeField] private bool once = false;
    [SerializeField] private bool isStep = false;

    [SerializeField] private GameObject enemy4wakeup;
    [SerializeField] private Animator enemyAnim;

    private void Awake()
    {
        objMPB = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if(once == false)
        {
            StartCoroutine(AnimStep());
        }

        spriteRenderer.GetPropertyBlock(objMPB);
        objMPB.SetFloat("_step", stepValue);
        spriteRenderer.SetPropertyBlock(objMPB);

        if (stepValue >= 1)
        {
            StopCoroutine(AnimStep());

            gameObject.SetActive(false);

            enemy4wakeup.SetActive(true);
            enemyAnim.SetInteger("animNumber", 1);

        }
    }

    private IEnumerator AnimStep()
    {
        for (int i = 0; i < 20; i++)
        {
            stepValue += 0.01f;

            yield return new WaitForSeconds(0.5f);
        }

        once = true;
    }
}

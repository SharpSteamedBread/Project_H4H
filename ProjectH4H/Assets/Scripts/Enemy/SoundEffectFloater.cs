using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectFloater : MonoBehaviour
{
    [Header("이펙트")]
    [SerializeField] private GameObject vfx_bang;
    [SerializeField] private GameObject vfx_boom;
    [SerializeField] private GameObject vfx_clang;
    [SerializeField] private GameObject vfx_crash;
    [SerializeField] private GameObject vfx_kaboom;
    [SerializeField] private GameObject vfx_paw;
    [SerializeField] private GameObject vfx_zap;
    [SerializeField] private GameObject vfx_zoom;

    [Header("히트박스")]
    [SerializeField] private Vector2 soundVFXArea;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RandomVFXFloat();
    }

    private void RandomVFXFloat()
    {
        float rangeX = Random.Range(-soundVFXArea.x, soundVFXArea.x);
        float rangeY = Random.Range(0, soundVFXArea.y);

        GameObject clone = Instantiate(vfx_bang, new Vector2(rangeX, rangeY), Quaternion.identity);
        clone.GetComponent<ParticleSystem>().Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, soundVFXArea);
    }

}

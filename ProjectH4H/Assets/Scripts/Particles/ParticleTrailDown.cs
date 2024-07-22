using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrailDown : MonoBehaviour
{
    public int particleFallingLength = 5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0, -particleFallingLength, 0) * Time.deltaTime);
    }
}

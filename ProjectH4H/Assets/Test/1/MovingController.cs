using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    public float Speed = 10.0f;     // 움직이는 속도. public으로 설정하여 유니티 화면에서 조정할 수 있다.

    float h, v;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

        }

        h = Input.GetAxis("Horizontal");        // 가로축
        v = Input.GetAxis("Vertical");          // 세로축

        transform.position += new Vector3(h, 0, v) * Speed * Time.deltaTime;
    }
}

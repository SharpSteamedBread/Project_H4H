using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    public float Speed = 10.0f;     // �����̴� �ӵ�. public���� �����Ͽ� ����Ƽ ȭ�鿡�� ������ �� �ִ�.

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

        h = Input.GetAxis("Horizontal");        // ������
        v = Input.GetAxis("Vertical");          // ������

        transform.position += new Vector3(h, 0, v) * Speed * Time.deltaTime;
    }
}

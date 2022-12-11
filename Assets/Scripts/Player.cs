using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator m_ani;
    // Start is called before the first frame update
    void Start()
    {
        m_ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * 5f * z);
        transform.Rotate(Vector3.up * Time.deltaTime * 100f * x);

        if(x != 0 || z != 0)
        {
            m_ani.SetBool("run", true);
        }
        else
        {
            m_ani.SetBool("run", false);
        }
    }
}

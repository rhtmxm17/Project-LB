using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffTest : MonoBehaviour
{
    [SerializeField]
    ParticleSystem partSys;
    // Start is called before the first frame update
    void Start()
    {
        partSys = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            partSys.Stop();
            partSys.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshOff : MonoBehaviour
{
    private Renderer render;
    // Start is called before the first frame update
    void Start()
    {
        render = this.GetComponent<Renderer>();
        render.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

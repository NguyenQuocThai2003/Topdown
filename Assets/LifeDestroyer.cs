using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    public float Time;
    void Start()
    {
        Destroy(this.gameObject, Time);
    }

    
}

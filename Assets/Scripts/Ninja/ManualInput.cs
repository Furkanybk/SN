using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualInput : MonoBehaviour
{
    private NinjaController ninjaControl;

    private void Awake()
    {
        ninjaControl = this.gameObject.GetComponent<NinjaController>();
    }

    private void Update()
    { 

    }
}

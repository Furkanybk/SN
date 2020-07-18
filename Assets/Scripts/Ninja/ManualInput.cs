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
        if (VirtualInputManager.Instance.W)
        {
            ninjaControl.W = true;
        }
        else
        {
            ninjaControl.W = false;
        }

        if (VirtualInputManager.Instance.A)
        {
            ninjaControl.A = true;
        }
        else
        {
            ninjaControl.A = false;
        }

        if (VirtualInputManager.Instance.S)
        {
            ninjaControl.S = true;
        }
        else
        {
            ninjaControl.S = false;
        }

        if (VirtualInputManager.Instance.D)
        {
            ninjaControl.D = true;
        }
        else
        {
            ninjaControl.D = false;
        }
    }
}

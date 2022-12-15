using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class turnTablet : MonoBehaviour
{
    public XRDirectInteractor rHand;
    public XRDirectInteractor lHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rHand.selectTarget.name == "Tablet")
            gameObject.transform.Rotate(0, 180, 0);
        else
            gameObject.transform.Rotate(0, 0, 0);
    }
}

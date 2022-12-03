using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    [SerializeField] private float sliderSpeed;
    public SliderJoint2D sliderJoint2D;
   
    private void Start()
    {
        sliderJoint2D = gameObject.GetComponent<SliderJoint2D>();
    }

    private void Update()
    {
        CheckTranslationLimits();
    }
    private void CheckTranslationLimits() 
    {
        
        if (sliderJoint2D.limitState==JointLimitState2D.UpperLimit)
        {
            JointMotor2D motor = sliderJoint2D.motor;
            motor.motorSpeed = -sliderSpeed;
            sliderJoint2D.motor = motor;
        }
        else if(sliderJoint2D.limitState == JointLimitState2D.LowerLimit)
        {
            JointMotor2D motor = sliderJoint2D.motor;
            motor.motorSpeed = sliderSpeed;
            sliderJoint2D.motor = motor;
        }
    }
}

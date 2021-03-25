using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverAnimateSomethingElse : MonoBehaviour
{
    public enum AnimatorType
    {
        BOOL,
        TRIGGER,
        FLOAT,
        INT
    }
    public AnimatorType targetConditionType;
    [SerializeField] private Animator targetAnimator = null;
    [SerializeField] private string targetConditionName = null;

    [SerializeField] private bool defaultBoolTypeTrueFalse = true;
    [SerializeField] private int intTypeValue = 0;
    [SerializeField] private float floatTypeValue = 0;

    public void ActivateAnAnimationOnAnotherObjectWithAnAnimator()
    {
        switch (targetConditionType)
        {
            case AnimatorType.BOOL:
                targetAnimator.SetBool(targetConditionName, !defaultBoolTypeTrueFalse);
                defaultBoolTypeTrueFalse = !defaultBoolTypeTrueFalse;
                break;
            case AnimatorType.TRIGGER:
                targetAnimator.SetTrigger(targetConditionName);
                break;
            case AnimatorType.FLOAT:
                targetAnimator.SetFloat(targetConditionName, floatTypeValue);
                break;
            case AnimatorType.INT:
                targetAnimator.SetInteger(targetConditionName, intTypeValue);
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRuleHolder : MonoBehaviour
{
    [SerializeField] float min, max;
    [SerializeField] float y;


    public XRule xRule;
    private void Awake() {
        xRule = new XRule();
        xRule.min = min;
        xRule.max= max;
        xRule.y = y;
    }
}

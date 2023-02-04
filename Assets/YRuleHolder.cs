using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YRuleHolder : MonoBehaviour
{
    [SerializeField] float min, max;
    [SerializeField] float x;


    public YRule yRule;
    private void Awake() {
        yRule = new YRule();
        yRule.min = min;
        yRule.max= max;
        yRule.x = x;
    }
}

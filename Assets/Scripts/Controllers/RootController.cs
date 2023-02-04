using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public struct XRule
{
    public float min;
    public float max;
    public float y;
}

public struct YRule
{
    public float min;
    public float max;
    public float x;
}


public class RootController : MonoBehaviour
{
    Rigidbody2D rb;
    public Root root;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        root = gameObject.GetComponent<Root>();
    }
    [SerializeField] List<XRule> xRules;
    [SerializeField] List<YRule> yRules;
    [SerializeField] float hSpeed = 20f;
    [SerializeField] float vSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        xRules = new List<XRule>();
        yRules = new List<YRule>();

        var allGrass = GameObject.FindGameObjectsWithTag("Grass");

        foreach (var item in allGrass)
        {
            var xRule = item.GetComponent<XRuleHolder>();
            var yRule = item.GetComponent<YRuleHolder>();

            if (xRule != null) xRules.Add(xRule.xRule);
            if (yRule != null) yRules.Add(yRule.yRule);
        }
    }

    public static XRule CreateXRule(float min, float max, float y)
    {
        var rule = new XRule();
        rule.min = min;
        rule.max = max;
        rule.y = y;
        return rule;
    }

    public static YRule CreateYRule(float min, float max, float x)
    {
        var rule = new YRule();
        rule.min = min;
        rule.max = max;
        rule.x = x;
        return rule;
    }

    private void OnHorizontal(InputValue v)
    {
        foreach (var rule in xRules)
        {
            if (checkRule(rule))
            {
                HMove(v.Get<float>(), rule);
                break;
            }
        }
    }

    private void OnVertical(InputValue v) {
        foreach (var rule in yRules)
            {
                if (checkRule(rule))
                {
                    VMove(v.Get<float>(), rule);
                    break;
                }
            }
    }

    
    private void OnCancel() {
        root.Cancel();
    }

    private void OnAttack() {
        root.TryKill(transform.position);
    }
    
    void HMove(float h, XRule rule)
    {
        transform.position += new Vector3(h * hSpeed * Time.deltaTime, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, rule.min, rule.max), transform.position.y, transform.position.z);
    }

    void VMove(float v, YRule rule)
    {
        transform.position += new Vector3(0, v * vSpeed * Time.deltaTime, 0);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, rule.min, rule.max), transform.position.z);
    }

    bool checkRule(XRule rule)
    {
        return transform.position.y == rule.y && transform.position.x >= rule.min && transform.position.x <= rule.max;
    }

    bool checkRule(YRule rule)
    {
        return transform.position.x == rule.x && transform.position.y >= rule.min && transform.position.y <= rule.max;
    }
}

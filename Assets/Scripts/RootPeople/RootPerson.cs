using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

abstract public class RootPerson : MonoBehaviour
{
    DefaultController controller;

    Stack<Root> roots;
    new Collider2D collider;

    int health = 3;

    public MyControls controls;

    public Dictionary<GameObject, Collision2D> contacts;

    private void Awake()
    {
        controller = gameObject.AddComponent<DefaultController>();

        roots = new Stack<Root>();
        contacts = new Dictionary<GameObject, Collision2D>();

        collider = GetComponent<Collider2D>();

        controls = new MyControls();
    }

    public void NormalMode()
    {
        controller.enabled = true;
        if (roots.Count != 0) roots.Pop();
    }

    public void RootMode()
    {
        var t = contacts.Where(e => e.Key.tag == "Grass").ToList();
        if (t.Count != 0)
        {
            controller.enabled = false;
            var root = (GameObject)Instantiate(Resources.Load("Root"), transform.position, Quaternion.identity);
            root.GetComponent<Root>().owner = this;
            roots.Push(root.GetComponent<Root>());

            var c = t[0].Value;
            if (c.GetContact(0).normal == Vector2.up)
                root.transform.position = new Vector3(root.transform.position.x, c.gameObject.GetComponent<XRuleHolder>().xRule.y, root.transform.position.z);

            else
                root.transform.position = new Vector3(c.gameObject.GetComponent<YRuleHolder>().yRule.x, root.transform.position.y, root.transform.position.z);
        }
    }

    public abstract void UseSpecial();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    RootController controller;
    Vector3 startPosition;
    public RootPerson owner;

    private RootPerson rooted = null;

    private void Awake()
    {
        controller = gameObject.AddComponent<RootController>();
        startPosition = transform.position;
    }

    public void Cancel()
    {
        owner.NormalMode();
        GameObject.Destroy(gameObject);
    }


    public void TryKill(Vector2 position)
    {
        owner.NormalMode();
        GameObject.Destroy(controller);
        var flower = (GameObject)Instantiate(Resources.Load("Flower"));
        flower.transform.position = startPosition;
        // flower.transform.position += new Vector3(0, 2f, 0);
        
        
        // extract target from position, then if any are found:
        // Do 1 damage, then: if health == 0, kill the target, otherwise root them and leave a flower.
    }
}

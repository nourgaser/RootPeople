using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class DefaultController : MonoBehaviour
{
    [SerializeField] private float hSpeed = 12f;
    [SerializeField] private bool inAir = false;
    [SerializeField] private float jumpForce = 12f;


    private Rigidbody2D rb;
    public RootPerson owner;

    MyControls.NormalActions controls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        owner = gameObject.GetComponent<Warrior>();
        if (owner == null) owner = gameObject.GetComponent<Ranger>();

        controls = owner.controls.normal;
    }

    private void Update()
    {
        if (inAir)
        {
            rb.AddForce(Vector2.down * 1000 * Time.deltaTime);
        }
    }

    private void OnHorizontal(InputValue v)
    {
        Debug.Log("Horizontal: " + v.Get<float>());
        transform.position += new Vector3(v.Get<float>() * hSpeed * Time.deltaTime, 0, 0);
    }

    private void OnJump()
    {
        if (!inAir)
        {
            inAir = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnRootMode()
    {
        owner.RootMode();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        owner.contacts.Add(other.gameObject, other);
        foreach (var contact in other.contacts)
        {
            if (contact.normal == Vector2.up) inAir = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        foreach (var contact in owner.contacts[other.gameObject].contacts)
        {
            if (contact.normal == Vector2.up) inAir = true;
        }
        owner.contacts.Remove(other.gameObject);
    }
}
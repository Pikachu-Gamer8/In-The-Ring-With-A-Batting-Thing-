using JetBrains.Annotations;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rigid;
    public float speed = 7f;
    public Transform PlayerCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = this.gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 movement = (transform.forward * v + transform.right * h).normalized * speed;
        rigid.linearVelocity = new Vector3 (movement.x, rigid.linearVelocity.y, movement.z);
    }
}

using JetBrains.Annotations;
using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [SerializeField] FootPlacerTripleNEO LeftLegPlacer;
    [SerializeField] FootPlacerTripleNEO RightLegPlacer;
    public Rigidbody rigid;
    public float speed = 7f;
    public Transform PlayerCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = this.gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    IEnumerator LegUpdateCoroutine()
    {
        // Run continuously
        while (true)
        {
            do
            {
                LeftLegPlacer.TryMove();
                yield return null;

            } while (LeftLegPlacer.Moving);

            do
            {
                RightLegPlacer.TryMove();
                yield return null;
            } while (RightLegPlacer.Moving);
        }
    }
    private void Awake()
    {
       StartCoroutine(LegUpdateCoroutine());
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 movement = (transform.forward * v + transform.right * h).normalized * speed;
        rigid.linearVelocity = new Vector3 (movement.x, rigid.linearVelocity.y, movement.z);
    }
}

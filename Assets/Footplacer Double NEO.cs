using UnityEngine;
using UnityEngine.Rendering;

public class FootplacerDoubleNEO : MonoBehaviour
{
    public GameObject body;
    public bool Step;
    public float stepDistance, offset;
    GameObject sphere;
    public Material colour;
    public LayerMask TerrainLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.GetComponent<MeshRenderer>().material = colour;

        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        sphere.GetComponent<SphereCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*Ray ray = new Ray(body.transform.position + (body.transform.right * offset), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit info, 10, TerrainLayer.value))
        
        {

            transform.position = info.point;

        }*/
        //Left
        //Backward
        if (this.transform.position.z - body.transform.position.z > stepDistance && Step)
        {
            this.transform.position = new Vector3(body.transform.position.x - offset, body.transform.position.y, body.transform.position.z - 1f);
        }
        //Forward
        if (body.transform.position.z - this.transform.position.z > stepDistance && Step)
        {
            this.transform.position = new Vector3(body.transform.position.x - offset, body.transform.position.y, body.transform.position.z + 1f);
            sphere.transform.position = new Vector3(body.transform.position.x - offset, body.transform.position.y, body.transform.position.z + 1f);
        }
    }
}

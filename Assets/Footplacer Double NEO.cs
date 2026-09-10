using UnityEngine;
using UnityEngine.Rendering;

public class FootplacerDoubleNEO : MonoBehaviour
{
    public GameObject body;
    public bool Step;
    public float stepDistance, offset;
    GameObject sphere;
    public Material colour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

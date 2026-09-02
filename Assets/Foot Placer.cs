using UnityEngine;

public class FootPlacer : MonoBehaviour
{
    public float FootSpacing;
    public Transform body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(body.position + (body.up * FootSpacing), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, 10))
        {transform.position = info.point;}
    }
}

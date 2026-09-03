using UnityEngine;

public class FootPlacerNEO : MonoBehaviour
{
    public LayerMask TerrainLayer;
    public FootPlacerNEO OtherFoot;
    public float StepDistance, StepHeight, StepLength, FootSpacing, Speed;
    public Transform Body;
    public Vector3 FootOffset;
    Vector3 OldPosition, NewPosition, CurrentPosition;
    //Vector3 OldNormal, NewNormal, CurrentNormal;
    float Lerp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FootSpacing = transform.localPosition.x;
        OldPosition = NewPosition = CurrentPosition = transform.position;
        //OldNormal = NewNormal = CurrentNormal = transform.up;
        Lerp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CurrentPosition;
        //transform.up = CurrentNormal;
        Ray ray = new Ray(Body.position + (Body.right * FootSpacing), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10, TerrainLayer.value))
        {
            if (Vector3.Distance(NewPosition, hit.point) > StepDistance && !OtherFoot.IsMoving() && Lerp >= 1)
            {
                Lerp = 0;
                int direction = Body.InverseTransformPoint(hit.point).z > Body.InverseTransformPoint(NewPosition).z ? 1 : -1;
                NewPosition = hit.point + (Body.forward * direction * StepLength) + FootOffset;
                //NewNormal = hit.normal;
            }
        }
        if (Lerp < 1)
        {
            Vector3 TempPos = Vector3.Lerp(OldPosition , NewPosition, Lerp);
            TempPos.y += Mathf.Sin(Lerp * Mathf.PI) * StepHeight;
            CurrentPosition = TempPos;
            //CurrentNormal = Vector3.Lerp(OldNormal, NewNormal, Lerp);
            Lerp += Time.deltaTime * Speed;
        }
    else
        {
            OldPosition = NewPosition ; //OldNormal = NewNormal;
        }
    }
public bool IsMoving()
    {
        return Lerp < 1;
    }
}

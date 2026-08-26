using JetBrains.Annotations;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class HeadLook : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MultiAimConstraint Head;
    public float AngleLimit;
    public float BufferAngle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 TargetHead = Head.data.sourceObjects.GetTransform(0).position - Head.data.constrainedObject.transform.position;
        TargetHead.y = 0;
        float TargetWeight = 1f;
        float AngleToTarget = Vector3.Angle(Head.data.constrainedObject.transform.forward,TargetHead);
        if (AngleToTarget > AngleLimit)
        {TargetWeight = 0f;}
        else if (AngleToTarget > (AngleLimit - BufferAngle));
        {float Smooth = (AngleToTarget - (AngleLimit - BufferAngle)) / BufferAngle;
        TargetWeight = 1f - Smooth;}
        Head.weight = Mathf.MoveTowards(Head.weight, TargetWeight, Time.deltaTime * 2f);
    }
}

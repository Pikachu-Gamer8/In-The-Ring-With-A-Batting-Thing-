using System.Collections;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class FootPlacerTripleNEO : MonoBehaviour
{
    [SerializeField] Transform HomeTransform;
    [SerializeField] float WantStepAtDistance;
    [SerializeField] float MoveDuration;
    public bool Moving;
    [SerializeField] float StepOvershootFraction;
    public float InOut(float K)
    {
        if ((K *= 2f) > 1f) return 0.5f * K * K * K;
        return 0.5f * ((K -= 2f) * K * K + 2f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    IEnumerator MoveToHome()
    {
        Moving = true;

        Quaternion startRot = transform.rotation;
        Vector3 startPoint = transform.position;

        Quaternion endRot = HomeTransform.rotation;
        Vector3 endPoint = HomeTransform.position;

        float timeElapsed = 0;
        IEnumerator Move()
        {
            Moving = true;

            Vector3 startPoint = transform.position;
            Quaternion startRot = transform.rotation;

            Quaternion endRot = HomeTransform.rotation;

            Vector3 towardHome = (HomeTransform.position - transform.position);
            float overshootDistance = WantStepAtDistance * StepOvershootFraction;
            Vector3 overshootVector = towardHome * overshootDistance;
            overshootVector = Vector3.ProjectOnPlane(overshootVector, Vector3.up);

            Vector3 endPoint = HomeTransform.position + overshootVector;

            Vector3 centerPoint = (startPoint + endPoint) / 2;

            float timeElapsed = 0;
            do
            {
                timeElapsed += Time.deltaTime;
                float normalizedTime = timeElapsed / MoveDuration;

                normalizedTime = InOut(normalizedTime);
                transform.position =
                Vector3.Lerp(
                    Vector3.Lerp(startPoint, centerPoint, normalizedTime),
                    Vector3.Lerp(centerPoint, endPoint, normalizedTime),
                    normalizedTime
                  );

                transform.rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);

                yield return null;
            }
            while (timeElapsed < MoveDuration);

            Moving = false;
        }

        do
        {
            timeElapsed += Time.deltaTime;

            float normalizedTime = timeElapsed / MoveDuration;

            transform.position = Vector3.Lerp(startPoint, endPoint, normalizedTime);
            transform.rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);

            yield return null;
        }
        while (timeElapsed < MoveDuration);

        Moving = false;
    }
    public void TryMove()
    {
        if (Moving) return;

        float distFromHome = Vector3.Distance(transform.position, HomeTransform.position);

        if (distFromHome > WantStepAtDistance)
        {
            StartCoroutine(MoveToHome());
        }
    }
}

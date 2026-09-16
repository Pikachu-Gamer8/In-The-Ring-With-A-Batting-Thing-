using UnityEngine;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] Transform HomeTransform;
    [SerializeField] float WantStepAtDistance;
    [SerializeField] float MoveDuration;
    public bool Moving;
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
    void Update()
    {
        if (Moving) return;

        float distFromHome = Vector3.Distance(transform.position, HomeTransform.position);

        if (distFromHome > WantStepAtDistance)
        {
            StartCoroutine(MoveToHome());
        }
    }
}

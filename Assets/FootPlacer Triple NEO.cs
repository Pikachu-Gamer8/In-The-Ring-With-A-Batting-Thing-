using System.Collections;
using System.Net;
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
    public Rigidbody Rigid;
    public Material Material2, Material3, Material4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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

        Vector3 Direction = Rigid.linearVelocity;
        Vector3 EndPointDir = HomeTransform.position + (Direction / 2);
        Vector3 endPoint = HomeTransform.position + overshootVector;


        Vector3 centerPoint = (startPoint + EndPointDir) / 2;
        StartCoroutine(SphereSpawn(endPoint, startPoint, centerPoint));
        float timeElapsed = 0;
        do
        {
            timeElapsed += Time.deltaTime;
            float normalizedTime = timeElapsed / MoveDuration;

            normalizedTime = InOut(normalizedTime);
            transform.position =
            Vector3.Lerp(
                Vector3.Lerp(startPoint, centerPoint, normalizedTime),
                Vector3.Lerp(centerPoint, EndPointDir, normalizedTime),
                normalizedTime
              );

            transform.rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);

            yield return null;
        }
        while (timeElapsed < MoveDuration);

        Moving = false;
    }

    IEnumerator SphereSpawn(Vector3 endPoint, Vector3 startPoint, Vector3 centerPoint)
    {
        Debug.Log("spawn");
        Vector3 Direction = Rigid.linearVelocity;
        Vector3 EndPointDir = endPoint + (Direction / 2);
        GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sphere.name = "EndPointDir";
        Sphere.GetComponent<SphereCollider>().enabled = false;
        Sphere.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        Sphere.transform.position = EndPointDir;
        yield return new WaitForSeconds(1f);
        GameObject Sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sphere2.name = "centerPoint";
        Sphere2.GetComponent<SphereCollider>().enabled = false;
        Sphere2.GetComponent<Renderer>().material = Material2;
        Sphere2.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        Sphere2.transform.position = centerPoint;
        yield return new WaitForSeconds(1f);
        GameObject Sphere3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sphere3.name = "startPoint";
        Sphere3.GetComponent<SphereCollider>().enabled = false;
        Sphere3.GetComponent<Renderer>().material = Material3;
        Sphere3.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        Sphere3.transform.position = startPoint;
        yield return new WaitForSeconds(1f);
        GameObject Sphere4 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sphere4.name = "endPoint";
        Sphere4.GetComponent<SphereCollider>().enabled = false;
        Sphere4.GetComponent<Renderer>().material = Material4;
        Sphere4.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        Sphere4.transform.position = endPoint;
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(0.5f);

        /*Destroy(Sphere);
        Destroy(Sphere2);
        Destroy(Sphere3);*/
    }


    IEnumerator MoveToHome()
    {
        Moving = true;

        Quaternion startRot = transform.rotation;
        Vector3 startPoint = transform.position;

        Quaternion endRot = HomeTransform.rotation;
        Vector3 endPoint = HomeTransform.position;

        /*new Vector3(HomeTransform.position.x, HomeTransform.position.y, HomeTransform.position.z);
        Vector3*/

        /*StartCoroutine(SphereSpawn());
        IEnumerator SphereSpawn()
        {
            Vector3 Direction = Rigid.linearVelocity;
            Vector3 EndPointDir = endPoint + (Direction / 2);
            GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Sphere.GetComponent<SphereCollider>().enabled = false;
            Sphere.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            Sphere.transform.position = EndPointDir;
            GameObject Sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Sphere2.GetComponent<SphereCollider>().enabled = false;
            Sphere2.GetComponent<Renderer>().material = Material2;
            Sphere2.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            Sphere2.transform.position = centerPoint;
            GameObject Sphere3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Sphere3.GetComponent<SphereCollider>().enabled = false;
            Sphere3.GetComponent<Renderer>().material = Material3;
            Sphere3.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            Sphere3.transform.position = EndPointDir;
            yield return new WaitForSeconds(0.5f);
            Destroy(Sphere);
            Destroy(Sphere2);
            Destroy(Sphere3);
        }*/

        float timeElapsed = 0;
        /*IEnumerator Move()
        {
            Moving = true;

            Vector3 startPoint = transform.position;
            Quaternion startRot = transform.rotation;

            Quaternion endRot = HomeTransform.rotation;

            Vector3 towardHome = (HomeTransform.position - transform.position);
            float overshootDistance = WantStepAtDistance * StepOvershootFraction;
            Vector3 overshootVector = towardHome * overshootDistance;
            overshootVector = Vector3.ProjectOnPlane(overshootVector, Vector3.up);

            Vector3 endPoint = HomeTransform.position;
            Vector3 Direction = Rigid.linearVelocity;
            Vector3 EndPointDir = endPoint + (Direction / 2);
            /*Vector3 endPoint = HomeTransform.position + overshootVector;*/
            

            /*Vector3 centerPoint = (startPoint + EndPointDir) / 2;

            float timeElapsed = 0;
            do
            {
                timeElapsed += Time.deltaTime;
                float normalizedTime = timeElapsed / MoveDuration;

                normalizedTime = InOut(normalizedTime);
                transform.position =
                Vector3.Lerp(
                    Vector3.Lerp(startPoint, centerPoint, normalizedTime),
                    Vector3.Lerp(centerPoint, EndPointDir, normalizedTime),
                    normalizedTime
                  );

                transform.rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);

                yield return null;
            }
            while (timeElapsed < MoveDuration);

            Moving = false;
        }*/

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
            StartCoroutine(Move());
        }
    }
}

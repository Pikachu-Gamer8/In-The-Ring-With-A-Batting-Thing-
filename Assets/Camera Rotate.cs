using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Quaternion StartValue;
    public bool RotateTransition;
    public float Dampener, RotateValue, Speed, Direction;
    public float Euler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FixedUpdate()
    {
        if (RotateTransition)
        {
            /*this.transform.rotation = Quaternion.Slerp(this.transform.rotation, RotateValue, Time.deltaTime/Dampener);
            if(this.transform.rotation == RotateValue)
            {
                RotateTransition = false;
            }*/

            transform.Rotate(Vector3.up * Speed * Time.deltaTime * Direction);
            Speed += Time.deltaTime * 5f;
            Euler = transform.localEulerAngles.y;
            if(transform.localEulerAngles.y >= RotateValue && Direction == 1)
            {
                RotateTransition = false;
                Speed = 25f;
            }
            else if(transform.localEulerAngles.y <= RotateValue && Direction == -1)
            {
                RotateTransition = false;
                Speed = 25f;
            }
        }
    }

    // Update is called once per frame
    public void Main_Menu()
    {
        StartValue = this.transform.rotation;
        RotateValue = 0f;
        RotateTransition = true;
    }

    public void Settings()
    {
        StartValue = this.transform.rotation;
        RotateValue = 270f;
        RotateTransition = true;
        Direction = -1;
    }

    public void Controls()
    {
        StartValue = this.transform.rotation;
        RotateValue = 90f;
        RotateTransition = true;
        Direction 
    }

    public void Collection()
    {
        StartValue = this.transform.rotation;
        RotateValue = 180f;
        RotateTransition = true;
    }

    public void Back()
    {
        StartValue = this.transform.rotation;
        RotateValue = 0f;
        RotateTransition = true;
    }
}

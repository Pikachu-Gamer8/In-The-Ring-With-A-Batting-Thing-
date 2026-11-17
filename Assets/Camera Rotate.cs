using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Quaternion StartValue;
    public bool RotateTransition;
    public float Dampener, DampenerValue, RotateValue, Speed, Direction, TargetRotation, Progress;
    public float Euler;
    public float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FixedUpdate()
    {
        if (RotateTransition)
        {
            time += Time.deltaTime;
            /*this.transform.rotation = Quaternion.Slerp(this.transform.rotation, RotateValue, Time.deltaTime/Dampener);
            if(this.transform.rotation == RotateValue)
            {
                RotateTransition = false;
            }*/
            if ((transform.localEulerAngles.y - StartValue.y / RotateValue) != 0f)
            {
                Progress = ((transform.localEulerAngles.y - StartValue.y / RotateValue));
            }
            Dampener = DampenerValue * Progress;
            transform.Rotate(Vector3.up * Speed * Time.deltaTime * Direction);
            Speed -= Time.deltaTime;// * Dampener;
            Euler = transform.localEulerAngles.y;
            if(transform.localEulerAngles.y >= RotateValue && Direction == 1)
            {
                RotateTransition = false;
                Speed = 50f;
                transform.localEulerAngles = new Vector3(0, RotateValue, 0);
            }
            else if(transform.localEulerAngles.y <= RotateValue && Direction == -1)
            {
                RotateTransition = false;
                Speed = 50f;
                transform.localEulerAngles = new Vector3(0, RotateValue, 0);
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
        Direction = 1;
    }

    public void Collection()
    {
        StartValue = this.transform.rotation;
        RotateValue = 180f;
        RotateTransition = true;
        Direction = -1;
    }

    public void Back()
    {
        StartValue = this.transform.rotation;
        RotateValue = 0f;
        RotateTransition = true;
        if (Direction == -1)
        {
            Direction = 1;
        }
        else
        {
            Direction = -1;
        }
    }
}

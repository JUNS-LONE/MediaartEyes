using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gazing : MonoBehaviour
{
    // Start is called before the first frame update


    private Camera cam;
    public bool stopgaze = false;
    public URGSensorView URGSensorViewPosition;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stopgaze = true;
        }


        /*
        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log("mousePosition : " +  Input.mousePosition);
        Debug.Log("mouse : " + mouse);

        Vector3 dirt = mouse - (Vector3)transform.position;

        Debug.Log("dirt.normalized : " + dirt.normalized);

        transform.forward = dirt.normalized;
        */
        /*
        Event currEvent = Event.current;
        Vector3 point =
            cam.ScreenToWorldPoint(new Vector3(currEvent.mousePosition.x,
                                               cam.pixelHeight - currEvent.mousePosition.y,
                                               cam.nearClipPlane));

        Vector3 dirt = point - (Vector3)transform.position;

        transform.forward = dirt.normalized;
        */
    }

    private void OnGUI()
    {
        if (stopgaze == false)
        {
            Vector3 Pos = URGSensorViewPosition.temppos;
            //URGSensorViewPosition.temppos.y = Camera.main.nearClipPlane; //nearclipPlane 
            Vector3 worldPos = cam.ScreenToWorldPoint(Pos); //mousepod 2차원 , worldpos 3차원 

            Vector3 dirt = Pos - transform.position; // 마우스 좌표 - 물체 중심 위치 

            transform.forward = dirt.normalized;

            //if (Input.GetMouseButtonUp(0))
            {
                //transform.forward = - dirt.normalized;
            }
        }

    }

}

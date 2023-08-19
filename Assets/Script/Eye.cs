using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class EYE : MonoBehaviour
{
    Camera Charactercam;
    

    private void Awake()
    {
        Charactercam = GetComponentInChildren<Camera>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookMouseCursor();
        
        
    }

    public void LookMouseCursor()
    {
        /*
        Ray ray = Charactercam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mouse = new Vector3(hit.point.x, hit.point.y, hit.point.z) - transform.position;
            Debug.Log(hit.point.x+hit.point.y+hit.point.z);
        }
        */
    }


}

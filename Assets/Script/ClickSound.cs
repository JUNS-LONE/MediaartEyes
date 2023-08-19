using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public LayerMask target;
    public AudioSource audioSource;
    public int getcount = 0;
    public List<Gazing> lstGazing; // List <타입으로써의 클래스 > 변수 : 클래스의 기능을 쓸 수있는 리스트 생성 ; 
    public List<Rotation> lstRotation;
    
    void Start()
    {
        
    }

    void Update()
    {

        //Gazing newgaze = new Gazing();

        if (Input.GetMouseButtonDown(0))
        {
            
            /*if (newgaze.stopgaze == false)
            {
                newgaze.stopgaze = true;
            }
            else if (newgaze.stopgaze == true)
            {
                newgaze.stopgaze = false;
            }
            Debug.Log(newgaze.stopgaze);
            */ 

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log(target);




            if (Physics.Raycast(ray,out hit,target))
            {
                if ( hit.collider.gameObject.tag == "Eyes")
                {
                    audioSource.Play();
                    Debug.Log("play");


                    for (int i = 0; i < lstGazing.Count; i++)
                    {


                        lstGazing[i].stopgaze = true;
                        lstRotation[i].Rotatehit();
                        Debug.Log(lstGazing[i].stopgaze);
                        //lstRotation[i]
                    }
                    
                }

    
            }

            
        }

        
        
            
            


    }

    /*void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            audioSource.Play();
            Debug.Log("Mouse Click Count : " + eventData.clickCount);
        }

        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            audioSource.Play();
            Debug.Log("Mouse Click Count : " + eventData.clickCount);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            audioSource.Play();
            Debug.Log("Mouse Click Count : " + eventData.clickCount);
        }

        Debug.Log("Mouse Click Count : " + eventData.clickCount);
    }
    */


}

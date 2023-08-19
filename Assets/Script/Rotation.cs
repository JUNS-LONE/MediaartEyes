using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    float acctime = 0.0f;
    bool Rotate = false;
    public Gazing time;
    public Quaternion setrot;
    public float random;

    private Rigidbody rb;

    private int randomnumber = 0; // Ÿ �޼ҵ忡�� ������ ���� ���ϴ� ������ �ű�� �� �ʵ� 

    Vector3[] direction = new Vector3[] {Vector3.back ,Vector3.down,Vector3.left,Vector3.forward,Vector3.right};
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (acctime > 1.0) { Stop(); }

        if (Rotate)
        {
            rb.angularVelocity = Random.insideUnitSphere * random;
            acctime += Time.deltaTime;
            transform.Rotate(360.0f * Time.deltaTime * direction[randomnumber]); 

            
        }
    }

    public void Stop()
    {
        if (Rotate == false)
            return; //���� �� �� �Ƚ�Ű�� �뵵

        transform.rotation = setrot;
        Rotate = false;
        time.stopgaze = false;
    }

    public void Rotatehit()
    {
        int randnumber2 = Random.Range(0, 5);
        randomnumber = randnumber2;

        //System.Random rotdir = new System.Random();
        //int randnum = rotdir.Next(0, 5);
        //randomnumber = randnum;

        setrot = transform.rotation;
        acctime = 0.0f;
        Rotate = true;
      
    }


}

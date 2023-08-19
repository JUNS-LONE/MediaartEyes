using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Net.Sockets;
using SCIP_library;
using DG.Tweening;

public class URGSensorView : MonoBehaviour
{
    [SerializeField] private int _distanceGap = 100;
    [SerializeField] private int _minSize = 20;
    [SerializeField] private int _maxSize = 100;
    [SerializeField] private Vector2 _offsetXY_mm = Vector2.zero;
    [SerializeField] private float _offsetRot_deg = 0f;

    private URGSensor _urg;     // URG sensor

    public GameObject[] _flowers;
    private GameObject[] _spawn;

    private bool _calibrated;

    private Mesh _mesh;
    private List<Vector3> _vertices;
    private List<int> _triangles;
    [SerializeField] private Material _mat;

    public Vector3 temppos; // pp값을 받기위한 변수 
    public URGSensor startpoint;
    public float distance;


    void Start()
    {
        _urg = new URGSensor();
        _urg.OpenStream("192.168.0.10", 0, 1080);
        _spawn = new GameObject[1];
        _calibrated = false;

        var meshFilter = gameObject.AddComponent<MeshFilter>();
        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        _mesh = meshFilter.mesh;
        _mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        meshRenderer.sharedMaterial = _mat;

        _mesh.Clear();

        var step = _urg.Steps;
        _vertices = new List<Vector3>();
        for (int i = 0; i < step; i++)
        {
            _vertices.Add(Vector3.zero);
        }
        _vertices.Add(Vector3.zero);
        _mesh.SetVertices(_vertices);

        _triangles = new List<int>();
        for (int i = 0; i < step - 1; i++)
        {
            _triangles.Add(_vertices.Count - 1);
            _triangles.Add(i + 1);
            _triangles.Add(i);
        }
        _mesh.SetTriangles(_triangles, 0);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _urg.StoreCalibrationData();
            _calibrated = true;
            _mesh.Clear();
        }
        //인식범위 눈에 안보이게 비활성화(동작 되고있음)

        transform.localPosition = new Vector3(_offsetXY_mm.x, _offsetXY_mm.y, 0f) / 1000f;
        transform.localEulerAngles = new Vector3(0f, 0f, _offsetRot_deg);

        // set object detection param
        _urg.SetDetectParam(_distanceGap, _minSize, _maxSize);

        // get urg pose matrix
        _urg.Pose = transform.localToWorldMatrix;

        if (_calibrated == false)
        {
            var distance = _urg.Distances;
            for (int i = 0; i < distance.Length; i++)
            {
                _vertices[i] = _urg.CalcRawPos(i);
            }
            _mesh.SetVertices(_vertices);
            _mesh.RecalculateBounds();
            _mesh.RecalculateNormals();
        }

        var objs = _urg.Objs;
        if (objs == null) return;

        //float minDistance = 0; // 최소거리 갱신
        Vector3 minvector = Vector3.one * float.MaxValue;

        for (int i = 0; i < objs.Length; i++)
        {
            var p = objs[i];
            var pp = new Vector3(p.x, p.y, p.z);
            

            foreach (Vector3 v in objs)
            {
                minvector = Vector3.Min(minvector, v);

            }

            temppos = minvector;

            /*
            distance = Vector3.Distance(pp, startpoint.startpoint);// 새로구한 거리 
            if ( i == 0 )  // 맨 처음자리에는 
            {
                minDistance = distance;
            }
            else if ( minDistance > distance )
            {
                minDistance = distance;
            }
            */



            int freeId = -1;
            bool canBloom = true;
            for (int n = 0; n < _spawn.Length; n++)
            {
                if (_spawn[n] == null)
                {
                    freeId = n;
                    continue;
                }

                if ((_spawn[n].transform.localPosition - pp).magnitude < 0.2f)
                {
                    canBloom = false;
                }
            }
            
            if (canBloom)
            {
                Bloom(freeId, minvector);
            }
            
        }



        Debug.Log("position" + minvector);
        Debug.Log("frame end --------------------------------------" );
    }




    void Bloom(int n, Vector3 p)
    {
       

        _spawn[n] = GameObject.Instantiate(_flowers[n % _flowers.Length], p, Quaternion.identity);
        _spawn[n].transform.localScale = Vector3.zero;

        var seq = DOTween.Sequence();
        seq.Append(_spawn[n].transform.DOScale(Vector3.one * 0.25f, 0.5f));
        seq.Join(_spawn[n].transform.DOShakeRotation(0.5f, 30f));
        seq.AppendInterval(5f);
        seq.Append(_spawn[n].transform.DOScale(Vector3.zero, 1f));
        seq.Join(_spawn[n].transform.DOShakeRotation(1f, -30f));
        seq.OnComplete(() => {
            GameObject.Destroy(_spawn[n]);
            _spawn[n] = null;
        });


    }

    private void OnDestroy()
    {
        _urg.CloseStream();
    }
}
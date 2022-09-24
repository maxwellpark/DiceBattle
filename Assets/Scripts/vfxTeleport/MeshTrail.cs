using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{

    private bool _isTrailActive;
    public float _activeTime=2f;
    public float _meshDestroyDelay=0.001f;



    public Transform _posToSpawn;
    //use cube transform in the model


    //mesh stff
    public float _meshRefreshRate=0.1f;
    private SkinnedMeshRenderer[] _sknMesh;

    public Material _mat;




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //acctivate TRAIL
        if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.D)&& !_isTrailActive)
        {
            _isTrailActive=true;
            StartCoroutine(ActivateTrail(_activeTime));
        }

       


        IEnumerator ActivateTrail (float _timeActive)
        {

            while(_timeActive > 0)
            {
                _timeActive-=_meshRefreshRate;

                if(_sknMesh==null)
                {
                    _sknMesh=GetComponentsInChildren<SkinnedMeshRenderer>();
                }

            for(int i=0; i<_sknMesh.Length;i++)
            {
                GameObject _gObj= new GameObject();
                _gObj.transform.SetPositionAndRotation(_posToSpawn.position,_posToSpawn.rotation);
                //_gObj.transform.position=_posToSpawn.transform.position;
               MeshRenderer _mr= _gObj.AddComponent<MeshRenderer>();
               MeshFilter _mf= _gObj.AddComponent<MeshFilter>();

               Mesh mesh=new Mesh();
                _sknMesh[i].BakeMesh(mesh);

               _mf.mesh=mesh;
               _mr.material=_mat;

               Destroy(_gObj, _meshDestroyDelay);

            }   

            yield return new WaitForSeconds(_meshRefreshRate);

        }
        _isTrailActive=false;
        }



        
    }
}

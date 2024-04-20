using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private float _xVelocity;
    private float _yVelocity;
    private float _zVelocity;
    

    public bool isMeteorito;

    private float _velocityMultiplayer;
    private void Start()
    {
        _xVelocity = Random.Range(1f, 9f);
        _yVelocity = Random.Range(1f, 9f);
        _zVelocity = Random.Range(1f, 9f);
        if (isMeteorito)
        {
            _xVelocity *= 10;
            _yVelocity *= 10;
            _zVelocity *= 10;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(_velocityMultiplayer*_xVelocity*Time.deltaTime, _velocityMultiplayer*_yVelocity*Time.deltaTime,_velocityMultiplayer*_zVelocity*Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var beam = other.GetComponent<RayBeam>();
        // TODO Trigger damage effect
        Destroy(beam.gameObject);

        LeanTween.value(1, 70, 0.15f).setEaseOutCubic().setOnUpdate(x => _velocityMultiplayer = x).
            setOnComplete(x => LeanTween.value(70, 1, 1.0f).setEaseInCubic().setOnUpdate(x => _velocityMultiplayer = x));
        //LeanTween.moveLocalX(gameObject, 0.1f, 0.25f).setEaseShake().setRepeat(2);
        LeanTween.value(0f, 0.1f, 0.25f).setEaseShake().setRepeat(2).setOnUpdate(x=> transform.parent.localPosition = Vector3.one*x);
    }
    
    
}

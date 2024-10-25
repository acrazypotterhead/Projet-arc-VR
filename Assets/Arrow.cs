using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Transform tip;

    private bool _arrowShot = false;
    private Rigidbody _rigidBody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        PullInteraction.PullActionReleased += Release;
        _arrowShot = false;
        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        PullInteraction.PullActionReleased -= Release;
        _inAir = true;
        _arrowShot = true;
        gameObject.transform.parent = null;
        SetPhysics(true);

        Vector3 force = transform.forward * value * speed;
        _rigidBody.AddForce(force, ForceMode.Impulse);

        StartCoroutine(RotateWithVelocity());

        _lastPosition = tip.position;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidBody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (_inAir)
        {
            //CheckCollision();
            _lastPosition = tip.position;
        }
    }

    private void CheckCollision()
    {
        if (Physics.Raycast(_lastPosition, tip.position, out RaycastHit hitInfo))
        {
            if (!hitInfo.transform.gameObject.CompareTag("Body") && !hitInfo.transform.gameObject.CompareTag("Bow"))
            {
                if (hitInfo.transform.TryGetComponent(out Rigidbody body))
                {
                    _rigidBody.interpolation = RigidbodyInterpolation.Extrapolate;
                    transform.parent = hitInfo.transform;
                    body.AddForce(_rigidBody.velocity, ForceMode.Impulse);
                }
                Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider hitInfo)
    {
        if (!hitInfo.transform.gameObject.CompareTag("Body") && !hitInfo.transform.gameObject.CompareTag("Bow"))
        {
            if (hitInfo.transform.TryGetComponent(out Rigidbody body))
            {
                _rigidBody.interpolation = RigidbodyInterpolation.Extrapolate;
                transform.parent = hitInfo.transform;
                body.AddForce(_rigidBody.velocity, ForceMode.Impulse);
            }
            Stop();
        }
    }

    private void Stop()
    {
        _inAir = false;
        SetPhysics(false);
        if (_arrowShot)
        {
            StartCoroutine(waitAndDestroy());
        }
    }

    IEnumerator waitAndDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidBody.useGravity = usePhysics;
        _rigidBody.isKinematic = !usePhysics;
    }
}
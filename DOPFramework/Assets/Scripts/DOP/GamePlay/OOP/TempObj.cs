using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempObj : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _vec;
    public float _speed = 1;
    private void Awake()
    {
        _transform = transform;
        _vec = _transform.position;
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        _vec.y += _speed * Time.deltaTime;
        if (_vec.y > 5)
        {
            _vec.y = -5;
        }
        _transform.position = _vec;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agente_Old : MonoBehaviour
{
    private float _speed = 1; //como pegar o gameSpeed do Agente manager?
    public float GameSpeed  { get => _speed; set => _speed = value; }
    protected float _timeCorroutine;
    public float TimeCorroutine  { get => _timeCorroutine; set => _timeCorroutine = value; }
    private GameObject _mapaManager;
    private Vector3 _target, _targetAux;
    private int _targetID, _currentID, _score;
    public int TargetID { get => _targetID; set => _targetID = value; }
    public int Score { get => _score; set => _score = value; }
    public int CurrentID { get => _currentID; set => _currentID = value; }
    public Vector3 TargetPos { get => _target;}
    public Vector3 TargetAux { get => _targetAux; set => _targetAux = value; }
    private bool _moving = false;
    public bool Moving { get => _moving; set => _moving = value; }
    // Start is called before the first frame update
    public virtual void Start() //ta certo isso?
    {
        _timeCorroutine = 2f / _speed;
        _target = transform.position;
        StartCoroutine(Step());
    }
    
    public void SetTargetPos(Vector3 pos)
    {
        _target = pos;
        _target.y = 0f;
    }

    private void OnTriggerEnter(Collider collision)
    {
        _targetID = int.Parse(collision.gameObject.name);
        _targetAux = collision.gameObject.transform.position;
        _targetAux.y = 0;
    }

    IEnumerator Step()
    {
        var step = Time.deltaTime * _speed;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, step);
            yield return null;
        }
    }

    public void RotateLeft()
    {
        transform.Rotate(0, -90, 0, Space.World);
    }

    public void RotateRigth()
    {
        transform.Rotate(0, 90, 0, Space.World);
    }
}
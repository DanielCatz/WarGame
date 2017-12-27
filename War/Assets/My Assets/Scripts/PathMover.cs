using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathMover : MonoBehaviour {

    private NavMeshAgent navMeshAgent;
    private Queue<Vector3> pathPoints = new Queue<Vector3>();
    private bool isNewPath;
    private SelectUnit selectUnit;
    public int health;
    public int damage;
    public float vision;
    public float range;
    public float velocity;
    public PathMover enemyTarget;


    private void Awake()
    {
        health = 100;
        damage = 1;
        range = 3;
        isNewPath = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        FindObjectOfType<PathCreator>().OnNewPathCreated += SetPoints;
        selectUnit = GameObject.Find("UnitSelection").GetComponent<SelectUnit>();
        InitRange();
    }

    private void SetPoints(IEnumerable<Vector3> points, bool isNewPath) {
        if (selectUnit.selectedUnit == this.gameObject)
        {
            pathPoints = new Queue<Vector3>(points);
            this.isNewPath = isNewPath;
            Debug.Log("here");
        }
    }


    // Update is called once per frame
    void Update () {
        
        //check if alive
            UpdatePathing();
        //    debugValue();

        UpdateAttack();
    }

    private void UpdateAttack()
    {
        //check cooldown
        if(enemyTarget!=null&& velocity<0.5f) {
            enemyTarget.takeDamage(damage);
        }


    }

    private void takeDamage(int damage)
    {
        health -= damage;
    }

    private void UpdatePathing()
    {

        velocity = navMeshAgent.velocity.magnitude;
            if (isNewPath)
            {
                isNewPath = false;
                navMeshAgent.SetDestination(pathPoints.Dequeue());
            }
            else if (ShouldSetDestination())
            {
                navMeshAgent.SetDestination(pathPoints.Dequeue());
            }
        
    }

    private bool ShouldSetDestination()
    {
        if (pathPoints.Count == 0) {
           
            return false;
        }

       
        if (navMeshAgent.hasPath == false || navMeshAgent.remainingDistance<0.5f)
        {         
            return true;
        }
        return false;
    }

    private void InitRange()
    {
        
    }


    private void debugValue()
    {
        SphereCollider rad = GameObject.Find("Range").GetComponent<SphereCollider>();
        GameObject rangeMarkerUI = GameObject.Find("Marker").gameObject;
        rangeMarkerUI.gameObject.transform.localScale = new Vector3(range, 0f, range);
        
        rad.radius =  range/2;

    }

    public void SetTarget(GameObject enemy, bool switchNow) {
        Debug.Log("Set");
        if (switchNow) {
            if (enemy == null)
            
                enemyTarget = null;
                else
            enemyTarget = enemy.GetComponent<PathMover>();            
        }
    }
}

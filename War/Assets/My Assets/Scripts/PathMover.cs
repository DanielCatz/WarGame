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
    public float vision;
    public float velocity;
    public PathMover enemyTarget;
    public BasicGroundAttacker groundAttack;



    private void InitStats() {
        groundAttack = new BasicGroundAttacker(10,200f, 3);
        health = 100;
        //damage = 10;
        //range = 3;
        isNewPath = false;
       // basicCooldownStat = 200f;
        //basicCooldown = basicCooldownStat;
    }
    private void Awake()
    {
        InitStats();
       
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
        UpdateAttack();
        //    debugValue();


    }

    private void UpdateAttack()
    {
        if (groundAttack.HasCooldownElapsed())
        {
            if (enemyTarget != null && velocity < 0.5f)
            {
                enemyTarget.TakeDamage(groundAttack.damage);//to replace with polymorph
                groundAttack.ResetBasicCooldown();
            }
        }
        groundAttack.UpdateBasicCooldown();
    }

    private void TakeDamage(int damage)
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


    private void DebugValue()
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

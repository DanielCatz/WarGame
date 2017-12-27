using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathCreator : MonoBehaviour {

    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();
    public System.Action<IEnumerable<Vector3>,bool> OnNewPathCreated = delegate{ };
    



    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            points.Clear();
        }

        if (Input.GetButton("Fire1"))
        {
            

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;// 2d this
            if (Physics.Raycast(ray, out hitInfo))
            {

                if (DistanceToLastPoint(hitInfo.point) > 1f && hitInfo.transform.tag=="Ground")
                {
                    points.Add(hitInfo.point);
                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPositions(points.ToArray());
                }

            }
        }
        else if (Input.GetButtonUp("Fire1")) {
            OnNewPathCreated(points,true);
        } 
            

    }

    private float DistanceToLastPoint(Vector3 point) {
        if (!points.Any()) {
            return Mathf.Infinity;
        }
        return Vector3.Distance(points.Last(),point);
    }
}

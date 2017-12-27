using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnit : MonoBehaviour {

    public GameObject selectedUnit;
    RaycastHit hit;
	// Update is called once per frame
	void Update () {

        if(selectedUnit==null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.transform.tag == "SelectableUnit")
                    {
                        selectedUnit = hit.transform.gameObject;
                        selectedUnit.transform.Find("Marker").gameObject.SetActive(true);
                    }

                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.transform.tag == "SelectableUnit")
                    {
                        selectedUnit.transform.Find("Marker").gameObject.SetActive(false);
                        selectedUnit = null;
                        selectedUnit = hit.transform.gameObject;
                        selectedUnit.transform.Find("Marker").gameObject.SetActive(true);
                    }

                }
            }
        }

        
	}
}

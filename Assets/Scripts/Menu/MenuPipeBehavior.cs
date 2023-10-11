using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPipeBehavior : MonoBehaviour
{
    public GameObject menuProps;
    public GameObject marioProp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveMenuPropsCallback()
    {
        marioProp.SetActive(true);
        menuProps.GetComponent<MenuPropsBehavior>().MoveDownCallback();
    }
}

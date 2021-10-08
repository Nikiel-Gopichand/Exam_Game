using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{ public Camera cam;
    public Interactables focus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e")) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit,100)) {
                Interactables interactable = hit.collider.GetComponent<Interactables>();
                if (interactable!=null) {
                    SetFocus(interactable);
                }
            }
        }
        
    }
    void SetFocus(Interactables newFocus) {
        focus = newFocus;
    }
}

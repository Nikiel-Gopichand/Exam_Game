using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaypointController : MonoBehaviour
{
    public bool isActive = false;
    public RawImage img;
    public Transform objective;
    public Text metreRange;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == true)
        {
            img.enabled = true;
            float minX = img.GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;

            float minY = img.GetPixelAdjustedRect().width / 2;
            float maxY = Screen.width - minY;

            Vector2 pos = Camera.main.WorldToScreenPoint(objective.position + offset);

            if (Vector3.Dot((objective.position - transform.position), transform.forward) < 0)
            {
                if (pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }


            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            img.transform.position = pos;
            metreRange.text = ((int)Vector3.Distance(objective.position, transform.position) - 2).ToString() + "m";

        }
        else { img.enabled = false; }

    }
}

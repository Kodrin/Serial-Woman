using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public Milk milk;
    public bool spill;

    private void Update()
    {
        if (spill)
        {
            Spill();
            spill = false;
        }
    }
    public void Spill()
    {
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z + 47.3f);
        milk.gameObject.SetActive(false);
    }

}

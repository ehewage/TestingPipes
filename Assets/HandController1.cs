using UnityEngine;
using System.Collections;

public class HandController1 : MonoBehaviour {

    public GameObject rightHandFinger0;
    public GameObject rightHandFinger1;

    private Transform rightHandFinger0_1;
    // Use this for initialization
	void Start () {
        rightHandFinger0_1 = rightHandFinger0.transform.GetChild(0).GetChild(0);
        //meeple = this.gameObject.transform.GetChild(0);

    }
	
	// Update is called once per frame
	void Update () {
        //rightHandFinger0.transform.Translate(transform.up * Time.deltaTime * 1);
        rightHandFinger0_1.transform.Rotate(Vector3.up * Time.deltaTime * 10f);
        rightHandFinger0.transform.Translate(Vector3.up * Time.deltaTime * 0.1f); 
        rightHandFinger1.transform.position = rightHandFinger1.transform.position + new Vector3(1, 1, 1)*Time.deltaTime*0.1f;
    }
}

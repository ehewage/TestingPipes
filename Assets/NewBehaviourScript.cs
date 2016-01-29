using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

    public Text text;
    private int counter = 0;
	// Use this for initialization
	void Start () {
        text.text = "Counter";
	}
	
	// Update is called once per frame
	void Update () {

        counter++;
        text.text = "Counter = "; //+ counter.ToString();
	}
}

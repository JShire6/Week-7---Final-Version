using UnityEngine;
using System.Collections;

public class CoinSpin : MonoBehaviour {

    public int spin = 5;
	// Use this for initialization
	void Start () {
       
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, spin, 0, Space.World);
	
	}
}

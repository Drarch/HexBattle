using UnityEngine;
using System.Collections;

public class AnimationHelper : MonoBehaviour {

    public bool delete = false;

	// Use this for initialization
	void Start ()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        if (delete) Delete();
	}

    public void Delete()
    {
        Destroy(this.transform.parent.gameObject);
    }
}

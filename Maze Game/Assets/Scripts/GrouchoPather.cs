using UnityEngine;
using System.Collections;
using Pathfinding;

public class GrouchoPather : MonoBehaviour {

	public Path path;
	public float speed;

	private Transform target;
	private int currentWaypoint;
	private CharacterController charController;
    private bool havePath = false; //seemed to be trying to path before grid was calculated

	// Use this for initialization
	void Start () {
		StartCoroutine(PathUpdater());
	}
	
	// Update is called once per frame
	void Update () {
        BeginPath();
	}

    void BeginPath()
    {
        if (!havePath)
        {

            // Randomly determine initial target
           /* GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
            if (cells.Length == 0)
            {
                target = transform;
               Debug.Log("Had to make a Groucho target himself. QQ");
            }
            else
            {
                target = cells[Random.Range(0, cells.Length - 1)].transform;
            }*/
            target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            Seeker seeker = GetComponent<Seeker>();
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            currentWaypoint = 0;
            if (speed == null || speed == 0)
            {
                speed = 1;
            }
            charController = GetComponent<CharacterController>();
            havePath = true;
        }
    }
	
	public void OnPathComplete(Path p){
		if(p.error){
			Debug.Log ("Failed to generate a path.");
			return;
		}
		path = p;
	}

	IEnumerator PathUpdater(){
		float waitTime = 0.25f;
		do{
			yield return new WaitForSeconds(waitTime);
		}while(false);

		target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
		Seeker seeker = GetComponent<Seeker>();
		seeker.StartPath(transform.position, target.position, OnPathComplete);
	}

	void FixedUpdate(){
		if(path == null)
			return;
		if(currentWaypoint >= path.vectorPath.Count)
			return;
		Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized * speed;
		charController.SimpleMove(direction);
		Vector3 lookDirection = direction;
		lookDirection.Set(lookDirection.x, 0f, lookDirection.z);
		transform.rotation = Quaternion.LookRotation(lookDirection);
		
		if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < 0.1f)
			currentWaypoint++;
		
	}
}

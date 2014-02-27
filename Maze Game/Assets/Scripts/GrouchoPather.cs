using UnityEngine;
using System.Collections;
using Pathfinding;

public class GrouchoPather : MonoBehaviour {

	// The path this Groucho is currently on. Will be set by Seeker.StartPath
	public Path path;
	// The speed of this Groucho
	public float speed;

	// The transform to which this Groucho will walk
	private Transform target;
	// The current waypoint along the path this Groucho is on
	private int currentWaypoint;
	// The Character Controller component of this Groucho
	private CharacterController charController;
	
	// Tells the Groucho whether it is following the player so it knows when to stop
	private bool followingPlayer = false;
    private bool havePath = false; //seemed to be trying to path before grid was calculated

	// Use this for initialization
	/**
	* Run when Groucho is instantiated
	* Sets speed of the monssers.
	*/
	void Start () {
		speed = speed * End.GetLevel ();
	}
	
	// Update is called once per frame
	/**
	* Checks to see if the player is close and, if s/he is, starts the monster
	* moving toward the player. Once aggro'd on the player, will de-aggro if
	* the player moves too far away.
	* Otherwise, allows the monster to continue on his randomly-assiged path.
	* Runs on each frame update.
	*/
	void Update () {
		Transform player = GameObject.FindGameObjectsWithTag ("Player") [0].transform;
		if ((player.position - transform.position).magnitude < (5 * speed)) {
			target = player;
			Seeker seeker = GetComponent<Seeker>();
			seeker.StartPath (transform.position, target.position, OnPathComplete);
			havePath = true;
			followingPlayer = true;
			Debug.Log ("Groucho going toward player now!");
		}else if((player.position - transform.position).magnitude < (15 * speed) && followingPlayer){
			GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
			if (cells.Length == 0){
				target = transform;
				Debug.Log("Had to make a Groucho target himself. QQ");
			}
			else{
				target = cells[Random.Range(0, cells.Length - 1)].transform;
			}
			followingPlayer = false;
			Debug.Log ("No longer following player. Sending a Groucho to " + target.name);
		}
        BeginPath();
	}

	/**
	* When we don't have a path, will pick a random cell in the maze
	* and move the monster to that cll via A*. Otherrwise, returns.
	*/
    void BeginPath()
    {
        if (!havePath)
        {

            // Randomly determine initial target
            GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
            if (cells.Length == 0){
            	target = transform;
            	Debug.Log("Had to make a Groucho target himself. QQ");
            }
            else{
                target = cells[Random.Range(0, cells.Length - 1)].transform;
            }
			Debug.Log ("Sending a Groucho to " + target.name);
            //target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
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
	
	/**
	* Logs an error to the Debug console if the path had an issue while
	* generating, otherwise sets sets our local Path to the path that was
	* generated.
	* This method should not be used explicitly, but rather by passing it
	* as an argument to the Seeker.StartPath method.
	* @param p The path that was generated.
	*/
	public void OnPathComplete(Path p){
		if(p.error){
			Debug.Log ("Failed to generate a path.");
			return;
		}
		path = p;
	}

	/**
	* If we do not have a path, returns. Otherwise, checks to see if we have completed
	* our path and, if we have, chooses a new random cell to send the monster to.
	* If we have not completed it, we move toward the next waypoint in the path based on
	* our speed and the amount of time that has passed since the last call to this method.
	* Also rotates the monster to face the direction he is moving.
	* Runs 60 times per second.
	*/
	void FixedUpdate(){
		if(path == null)
			return;
		if(currentWaypoint >= path.vectorPath.Count){
			GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
			if (cells.Length == 0){
				target = transform;
				Debug.Log("Had to make a Groucho target himself. QQ");
			}
			else{
				target = cells[Random.Range(0, cells.Length - 1)].transform;
			}
			Debug.Log ("Sending a Groucho to " + target.name);
			//target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
			Seeker seeker = GetComponent<Seeker>();
			seeker.StartPath(transform.position, target.position, OnPathComplete);
			currentWaypoint = 0;
			return;
		}
		Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized * speed;
		charController.SimpleMove(direction);
		Vector3 lookDirection = direction;
		lookDirection.Set(lookDirection.x, 0f, lookDirection.z);
		transform.rotation = Quaternion.LookRotation(lookDirection);
		
		if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < 0.1f)
			currentWaypoint++;
		
	}
}

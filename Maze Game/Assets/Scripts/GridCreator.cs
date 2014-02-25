using UnityEngine; 
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

/**
 * Creates a grid of specified dimensions and generates a procedural maze using a
 * modified form of Prim's Algorithm.
 * Applies Procedural Materials to the maze as well as creates external boundaries for the maze.
 * 
 * @author Shaun Howard
 * @author smh150
 * @author Timothy Sesler
 * @author tds45
 * @date 4 February 2014
 * @date modified 15 February 2014
 * 
 * Adapted from work provided online by Austin Takechi 
 * Contact: MinoruTono@Gmail.com
 */ 
public class GridCreator : MonoBehaviour {

    // Multiplies the size of the maze; initially set to one.
    public float MazeMultiplier = 1;

	// Prefab of maze cell, very basic default diffuse, loaded in Inspector.
	public Transform CellPrefab;

	// Prefab of the monster enemy
	public Transform Monster;

	//Prefab of the battery
	public Transform Battery;

	// The size of our grid (x, z) components.
	public Vector3 Size;

	// Grid array for storing maze cells.
	public Transform[,] Grid;

	// Maximum number of enemies to place
	public int MaxMonsters;

	//Maximum number of batteries to place
	public int MaxBatteries;

	// Procedural material of the wall, loaded in Inspector.
	public ProceduralMaterial WallSubstance;

	// Procedural material of the path, loaded in Inspector.
	public ProceduralMaterial PathSubstance;

	// An instance of the wall material.
	private ProceduralMaterial WallInstance;

	// Tells us whether the first wall material was generated.
	private bool FirstWallGeneration = true;

	// Tells the monster generation function that we're still making the grid
	private bool gridBeingGenerated = true;

	// These properties may be implemented in a GUI interface for changing materials upon start up.
//	private ProceduralPropertyDescription[] currWallProperties;
//	private ProceduralPropertyDescription[] currPathProperties;

	// Creates Maze boundaries, then generates maze with Prim's Algorithm, then texturizes maze cells.
	void Start () {
		CreateMazeBoundaryGrid();
		CreateGrid();
		SetRandomNumbers();
		SetAdjacents();
		SetStart();
		FindNext();
		StartCoroutine(PlaceMonsters());
		StartCoroutine(PlaceBatteries());
	}

	// Creates the boundary for the maze by instantiating cell walls and materials.
	void CreateMazeBoundaryGrid() {

		// List of boundary cells for the outer realms of the maze.
		List<Transform> BoundaryGrid = new List<Transform> ();

		// Generates walls along x = -1.
		for (int z = 0; z < Size.z; z++) {
			Transform newCell;
            newCell = (Transform)Instantiate(CellPrefab,
                        new Vector3(-1 * MazeMultiplier, 0, z * MazeMultiplier),
                        Quaternion.identity);
			newCell.name = string.Format("({0},0,{1})", -1, z);
			newCell.parent = transform;
			newCell.GetComponent<CellScript>().IsOuterWallCell = true;
			newCell.GetComponent<CellScript>().Position = new Vector3(-1, 0, z);
			BoundaryGrid.Add (newCell);
		}

		// Generates walls along z = -1.
		for (int x = 0; x < Size.x; x++) {
			Transform newCell;
            newCell = (Transform)Instantiate(CellPrefab,
                        new Vector3(x * MazeMultiplier, 0, -1 * MazeMultiplier),
                        Quaternion.identity);
			newCell.name = string.Format("({0},0,{1})", x, -1);
			newCell.parent = transform;
			newCell.GetComponent<CellScript>().IsOuterWallCell = true;
			newCell.GetComponent<CellScript>().Position = new Vector3(x, 0, -1);
			BoundaryGrid.Add (newCell);
		}

		// Generates walls along z = Size.z.
		for (int x = 0; x < Size.x; x++) {
			Transform newCell;
            newCell = (Transform)Instantiate(CellPrefab,
                        new Vector3(x * MazeMultiplier, 0, Size.z * MazeMultiplier),
                        Quaternion.identity);
			newCell.name = string.Format("({0},0,{1})", x, Size.z);
			newCell.parent = transform;
			newCell.GetComponent<CellScript>().IsOuterWallCell = true;
			newCell.GetComponent<CellScript>().Position = new Vector3(x, 0, Size.z);
			BoundaryGrid.Add (newCell);
		}

		// Generates walls along x = Size.x.
		for (int z = 0; z < Size.z; z++) {
			Transform newCell;
            newCell = (Transform)Instantiate(CellPrefab,
                        new Vector3(Size.x * MazeMultiplier, 0, z * MazeMultiplier),
                        Quaternion.identity);
			newCell.name = string.Format("({0},0,{1})", Size.x, z);
			newCell.parent = transform;
			newCell.GetComponent<CellScript>().IsOuterWallCell = true;
			newCell.GetComponent<CellScript>().Position = new Vector3(Size.x, 0, z);
			BoundaryGrid.Add (newCell);
		}


		// Translates outer wall cells to 3D and applies material.
		foreach (Transform cell in BoundaryGrid) {
			// Removes displayed weight
			cell.GetComponentInChildren<TextMesh>().renderer.enabled = false;

			cell.Translate (new Vector3(0f, 2f, 0f));
			cell.localScale = new Vector3(cell.localScale.x * MazeMultiplier, cell.localScale.y, cell.localScale.z * MazeMultiplier);
			cell.gameObject.layer = LayerMask.NameToLayer("Obstacles");

				// We set the material of the walls.
				if (FirstWallGeneration) { // Check if first generation of wall material.
					cell.renderer.material = WallSubstance;
					WallInstance = cell.renderer.material as ProceduralMaterial;
					FirstWallGeneration = false;
				} else { //Otherwise reference wall material instance.
					
					// Sets the wall cell material to the material instance.
					cell.renderer.material = WallInstance;
				}
		}
	}

	// Creates the grid by instantiating provided cell prefabs.
	void CreateGrid () {
		Grid = new Transform[(int)Size.x,(int)Size.z];

		// Places the cells and names them according to their coordinates in the grid.
		for (int x = 0; x < Size.x; x++) {
			for (int z = 0; z < Size.z; z++) {
				Transform newCell;
                newCell = (Transform)Instantiate(CellPrefab,
                            new Vector3(x * MazeMultiplier, 0, z * MazeMultiplier),
                            Quaternion.identity);
				newCell.name = string.Format("({0},0,{1})", x, z);
				newCell.localScale = new Vector3(newCell.localScale.x * MazeMultiplier, newCell.localScale.y, newCell.localScale.z * MazeMultiplier);
				newCell.parent = transform;
				newCell.GetComponent<CellScript>().Position = new Vector3(x * MazeMultiplier, 0, z * MazeMultiplier);
				Grid[x,z] = newCell;
			}
		}

		// Centers the camera on the maze.
		// Feel free to adjust this as needed.
		Camera.main.transform.position = Grid[(int)(Size.x / 2f),(int)(Size.z / 2f)].position + Vector3.up * 20f;
		Camera.main.orthographicSize = Mathf.Max(Size.x * 0.55f, Size.z * 0.55f) * MazeMultiplier;
	}

	// Sets a random weight to each cell.
	void SetRandomNumbers () {
		foreach (Transform child in transform) {
			int weight = Random.Range(0,10);
			child.GetComponentInChildren<TextMesh>().text = weight.ToString();
			child.GetComponent<CellScript>().Weight = weight;
		}
	}

	// Determines the adjacent cells of each cell in the grid.
	void SetAdjacents () {
		for(int x = 0; x < Size.x; x++){
			for (int z = 0; z < Size.z; z++) {
				Transform cell;
				cell = Grid[x,z];
				CellScript cScript = cell.GetComponent<CellScript>();
				
				if (x - 1 >= 0) {
					cScript.Adjacents.Add(Grid[x - 1, z]);
				}
				if (x + 1 < Size.x) {
					cScript.Adjacents.Add(Grid[x + 1, z]);
				}
				if (z - 1 >= 0) {
					cScript.Adjacents.Add(Grid[x, z - 1]);
				}
				if (z + 1 < Size.z) {
					cScript.Adjacents.Add(Grid[x, z + 1]);
				}
				
				cScript.Adjacents.Sort(SortByLowestWeight);
			}
		}
	}

	// Sorts the weights of adjacent cells.
	// Check the link for more info on custom comparators and sorting.
	// http://msdn.microsoft.com/en-us/library/0e743hdt.aspx
	int SortByLowestWeight (Transform inputA, Transform inputB) {
		int a = inputA.GetComponent<CellScript>().Weight;
		int b = inputB.GetComponent<CellScript>().Weight;
		return a.CompareTo(b);
	}

	/*********************************************************************
	 * Everything after this point pertains to generating the actual maze.
	 * Look at the Wikipedia page for more info on Prim's Algorithm.
	 * http://en.wikipedia.org/wiki/Prim%27s_algorithm
	 ********************************************************************/ 
	public List<Transform> PathCells;			// The cells in the path through the grid.
	public List<List<Transform>> AdjSet;		// A list of lists representing available adjacent cells.
	/** Here is the structure:
	 *  AdjSet{
	 * 		[ 0 ] is a list of all the cells
	 *      that have a weight of 0, and are
	 *      adjacent to the cells in the path
	 *      [ 1 ] is a list of all the cells
	 *      that have a weight of 1, and are
	 * 		adjacent to the cells in the path
	 *      ...
	 *      [ 9 ] is a list of all the cells
	 *      that have a weight of 9, and are
	 *      adjacent to the cells in the path
	 * 	}
	 *
	 * Note: Multiple entries of the same cell
	 * will not appear as duplicates.
	 * (Some adjacent cells will be next to
	 * two or three or four other path cells).
	 * They are only recorded in the AdjSet once.
	 */  

	// Initializes the sets and the starting cell.
	void SetStart () {
		PathCells = new List<Transform>();
		AdjSet = new List<List<Transform>>();
		
		for (int i = 0; i < 10; i++) {
			AdjSet.Add(new List<Transform>());	
		}
		
		Grid[0, 0].renderer.material.color = Color.green;
		AddToSet(Grid[0, 0]);
	}

	// Adds a cell to the set of visited cells.
	void AddToSet (Transform cellToAdd) {
		PathCells.Add(cellToAdd);
		
		foreach (Transform adj in cellToAdd.GetComponent<CellScript>().Adjacents) {
			adj.GetComponent<CellScript>().AdjacentsOpened++;
			
			if (!PathCells.Contains(adj) && !(AdjSet[adj.GetComponent<CellScript>().Weight].Contains(adj))) {
				AdjSet[adj.GetComponent<CellScript>().Weight].Add(adj);
			}
		}
	}

	// Determines the next cell to be visited.
	void FindNext () {
		Transform next;
		
		// Tells us whether we generated the path material or not.
		bool firstPathGeneration = true;
		// Contains an instance of the path material.
		ProceduralMaterial PathInstance = null;

		do {
			bool isEmpty = true;
			int lowestList = 0;

			// We loop through each sub-list in the AdjSet list of lists, until we find one with a count of more than 0.
			// If there are more than 0 items in the sub-list, it is not empty.
			// We've found the lowest sub-list, so there is no need to continue searching.
			for (int i = 0; i < 10; i++) {
				lowestList = i;
				
				if (AdjSet[i].Count > 0) {
					isEmpty = false;
					break;
				}
			}

			// The maze is complete.
			if (isEmpty) { 
				Debug.Log("Generation completed in " + Time.timeSinceLevelLoad + " seconds."); 
				CancelInvoke("FindNext");
				PathCells[PathCells.Count - 1].renderer.material.color = Color.red;
				
				foreach (Transform cell in Grid) {
					// Removes displayed weight
					cell.GetComponentInChildren<TextMesh>().renderer.enabled = false;


					// ***These are the wall cells of the maze.*** 
					if (!PathCells.Contains(cell)) {

						// We make the maze 3D by translation.
						cell.Translate(new Vector3(0f, 2f, 0f));
	
						// Sets the wall call material to the material instance.
						cell.renderer.material = WallInstance;

						// Puts the cell into the "Obstacles" layer for pathfinding purposes.
						cell.gameObject.layer = LayerMask.NameToLayer("Obstacles");
					}
				}
				gridBeingGenerated = false;
				return;
			}

			// If we did not finish, then:
			// 1. Use the smallest sub-list in AdjSet as found earlier with the lowestList variable.
			// 2. With that smallest sub-list, take the first element in that list, and use it as the 'next'.
			next = AdjSet[lowestList][0];
			// Since we do not want the same cell in both AdjSet and Set, remove this 'next' variable from AdjSet.
			AdjSet[lowestList].Remove(next);

		} while (next.GetComponent<CellScript>().AdjacentsOpened >= 2);	// This keeps the walls in the grid, otherwise Prim's Algorithm would just visit every cell

		// ***Here we go through the path (floor) cells of the maze.***
		// We set the materials of the path cells.
		if (firstPathGeneration) { // Check if first path material generation.
			next.renderer.material = PathSubstance;
			PathInstance = next.renderer.material as ProceduralMaterial;
			firstPathGeneration = false;
		} else { // Otherwise reference path material instance.
			
			// Sets the path cell material to the material instance.
			next.renderer.material = PathInstance;
		}

		// We add this 'next' transform to the Set our function.
		AddToSet(next);
		// Recursively call this function as soon as it finishes.
		Invoke("FindNext", 0);
	}

	IEnumerator PlaceBatteries(){
		float waitTime = 0.25f;
		float timeWaited = 0f;
		while (gridBeingGenerated) {
			yield return new WaitForSeconds (waitTime);
			timeWaited += waitTime;
		}
		int batteriesPlaced = 0;
		List<Transform> cellsUsed = new List<Transform>();
		while (batteriesPlaced < MaxBatteries && batteriesPlaced < PathCells.Count) {
			int targetIndex = Random.Range (0, PathCells.Count - 1);
			Transform targetCell = PathCells [targetIndex];
			int placementAttempts = 0;
			while (cellsUsed.Contains(targetCell)) {
				targetCell = PathCells[(targetIndex + 7) % PathCells.Count];
				if(++placementAttempts > 1000){
					break;
				}
			}
			if(!(placementAttempts > 1000)){
				cellsUsed.Add (targetCell);
				Vector3 cellPosition = targetCell.position;
				cellPosition.Set (cellPosition.x, cellPosition.y + 1.5f, cellPosition.z);
				Transform battery = (Transform)Instantiate (Battery, cellPosition, Quaternion.identity);
			}
			batteriesPlaced++;
		}
	}

	IEnumerator PlaceMonsters(){
		float waitTime = 0.25f;
		float timeWaited = 0f;
		while(gridBeingGenerated){
			//Debug.Log ("Waiting " + waitTime + "s. Have waited " + timeWaited + "s.");
			yield return new WaitForSeconds(waitTime);
			timeWaited += waitTime;
		}
		//TODO: Make a the graph update properly so the game doesn't hang when a monster gets placed.
		AstarPath.active.Scan();
		Debug.Log ("AstarPath.active.Scan() called at " + Time.timeSinceLevelLoad);
		int monstersPlaced = 0;
		List<Transform> cellsUsed = new List<Transform>();
		// We also check against PathCells.Count so that we can't run out of cells to use.
		Debug.Log ("About to start placing monsters. We can place up to " + (MaxMonsters < PathCells.Count ? (MaxMonsters + "(MaxMonsters)") : (PathCells.Count + "(PathCells.Count)")) + ".");
		while(monstersPlaced < MaxMonsters && monstersPlaced < PathCells.Count){
			int targetIndex = Random.Range (0, PathCells.Count - 1);
			Transform targetCell = PathCells[targetIndex];
			int placementAttempts = 0;
			while(cellsUsed.Contains(targetCell)){
				targetCell = PathCells[(targetIndex + 7) % PathCells.Count];
				if(++placementAttempts > 1000){
					break;
				}
			}
			if(!(placementAttempts > 1000)){
				cellsUsed.Add (targetCell);
				Vector3 cellPosition = targetCell.position;
				cellPosition.Set (cellPosition.x, cellPosition.y + 1.5f, cellPosition.z);
				Transform monster = (Transform)Instantiate (Monster, cellPosition, Quaternion.identity);
			}
			monstersPlaced++;
		}
		Debug.Log ("Placed " + monstersPlaced + " enemies, finishing at " + Time.timeSinceLevelLoad + "s.");
	}

	// Called once per frame.
	void Update() {

		// Pressing 'F1' will generate a new maze.
		if (Input.GetKeyDown(KeyCode.F1)) {
			Application.LoadLevel(0);	
		}
	}
}

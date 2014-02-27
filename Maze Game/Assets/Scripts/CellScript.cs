using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * The variables of a single maze cell.
 * @author Timothy Sesler
 * @author tds45
 * @date 4 February 2014
 * 
 * Adapted from work provided online by Austin Takechi 
 * Contact: MinoruTono@Gmail.com
 */ 
public class CellScript : MonoBehaviour {
	
	// The cells adjacent to this one
	public List<Transform> Adjacents;
	// The position of this cell
	public Vector3 Position;
	// The weight of this cell for the purpose of maze generation
	public int Weight;
	// The number of cells adjacent to this one that the maze generator has checked
	public int AdjacentsOpened = 0;
	//Whether this cell is part of the outside wall or not.
	public bool IsOuterWallCell = false;
}
>>>>>>> 29a3991337d9ce4082ffa16e8747c3b202d44189

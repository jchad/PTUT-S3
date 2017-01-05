using UnityEngine;

[SelectionBase]
public class VoxelGrid : MonoBehaviour {

	public int resolution=50;
	public GameObject SpawnPrefab;
	public GameObject platformPrefab;
	int[][] m;
	private bool[] voxels;
	private Material[] voxelMaterials;
	private float size;
	public void Awake () {
		size = 1f / resolution;
		voxels = new bool[resolution * resolution];
		voxelMaterials = new Material[voxels.Length];
		GenMap generator = new GenMap ();
		m = generator.iniMat(resolution);
		setPossibleSpawns ();
		for (int y = 0; y < resolution; y++) {
			for (int x = 0; x < resolution; x++) {
				if (m [y] [x] >= 1) {
					CreatePlatform (x, y);
				}
			}
		}
	}

	private void setPossibleSpawns(){
		for (int i = 0; i < (resolution-1); i++) {
			for (int j = 0; j < (resolution- 1); j++) {
				if ((m[i+1][j] == 1) && (m[i][j+1] == 1) && (m[i][j]==1) && (m[i+1][j+1]==0))  {
					CreateSpawn (j+1,i+1);
				} 
			}
		}
	}

	private void CreatePlatform (int x, int y) {
		GameObject o = Instantiate(platformPrefab) as GameObject;
		o.transform.parent = transform;
		o.transform.localPosition = new Vector3((x+0.5f),(y+0.5f));
		//o.transform.localScale = Vector3.one * size;
		//voxelMaterials[i] = o.GetComponent<SpriteRenderer>().material;
	}

	private void CreateSpawn (int x, int y) {
		GameObject o = Instantiate(SpawnPrefab) as GameObject;
		o.transform.parent = transform;
		o.transform.localPosition = new Vector3((x+0.5f),(y+0.5f));
	}
}
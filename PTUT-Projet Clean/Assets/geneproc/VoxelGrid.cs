using UnityEngine;
using UnityEngine.Networking;

[SelectionBase]
public class VoxelGrid : NetworkBehaviour {

    public int resolution = 10;
    public GameObject SpawnPrefab;
    public GameObject platformPrefab;
	public int division=3;
	[SyncVar]
	public Vector3 realScale;
    int[][] m;
    private bool[] voxels;
    private Material[] voxelMaterials;
    private float size;
	private int a, b;
	public override void OnStartServer () {
		realScale = new Vector3 (1f / division, 1f / division);
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
		for (a = 0; a < division; a++) {
			for (b = 0; b < division; b++) {
				GameObject ro = Instantiate (platformPrefab) as GameObject;
				ro.transform.parent = transform;
				ro.transform.localPosition = new Vector3((float) (x)+(a* (1f/division)), (float) (y)+(b*(1f/division)));
				NetworkServer.Spawn(ro);
			}
		}
	}

	private void CreateSpawn (int x, int y) {
		GameObject o = Instantiate(SpawnPrefab) as GameObject;
		o.transform.parent = transform;
		o.transform.localPosition = new Vector3((x+0.5f),(y+0.5f));
        NetworkServer.Spawn(o);
	}
}
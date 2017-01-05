using UnityEngine;
using System.Collections;


public class chtruc{
	[SerializeField]
	private int minh=5;
	[SerializeField]
	private int	probup=15;
	[SerializeField]
	private int probleft=100;
	[SerializeField]
	private int minw=5;



	public int[][] iniMat(int dim){
		int [][] mat = new int [dim][];
		for (int a = 0; a < dim; a++) {
			mat [a] = new int[dim];
			for (int b = 0; b < dim; b++) {
				mat [a] [b] = 0;
			}
		}
		int i = 0;
		int j = 0;

		while (i<dim-1){
			mat [i] [j] = 1;
			mat [i] [dim - 1] = 1;
			i += 1;
		}
		i = 0;
		while (i<dim-1){
			mat [0] [i] = 1;
			mat [dim - 1] [i] = 1;
			i = i + 1;
		}
		mat = fill (mat, dim);
		return mat;
	}


	private int[][] fill(int[][] m,int dim,int mj=0,int avi=0,int avj=0,int max=0){
		Debug.Log (avi);
		int i = 0;
		int j = 0;
		while (mj < dim - minw) {
			while (avi < dim - minh) {
				j = avj;
				i = avi;
				//droite
				while (j < minw && j < dim - minw) {
					m [i] [j] = 1;
					j += 1;
				}
				int up = Random.Range (0, 100);
				while (up > probup && j < dim - minw) {
					m [i] [j] = 1;
					j += 1;
				}
				if (j > mj) {
					mj = j;
				}
				//
				//haut
				while (i < minh && avi < dim - minh) {
					m [i] [j] = 1;
					i += 1;
				}
				int left = Random.Range (0, 100);
				while (left > probleft && i < dim - minh) {
					m [i] [j] = 1;
					i += 1;
				}
				//
				//gauche
				while (m [i] [j] != 1 && j > 0) {
					m [i] [j] = 1;
					j -= 1;
				}
				//
				avi = i;
			}
			avi = 0;
			avj = mj;
		}
		return m;

	}
}


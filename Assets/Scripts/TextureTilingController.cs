using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TextureTilingController : MonoBehaviour {
	
	// Give us the texture so that we can scale proportianally the width according to the height variable below
	// We will grab it from the meshRenderer
	//public Texture texture;
	//public float textureToMeshZ = 2f; // Use this to contrain texture to a certain size

	//Vector3 prevScale = Vector3.one;
	//float prevTextureToMeshZ = -1f;
	
	// Use this for initialization
	void Start () {
		//this.prevScale = gameObject.transform.lossyScale;
		//this.prevTextureToMeshZ = this.textureToMeshZ;
		
		this.UpdateTiling();
	}
	
	// Update is called once per frame
	void Update () {
		// If something has changed
		//if(gameObject.transform.lossyScale != prevScale || !Mathf.Approximately(this.textureToMeshZ, prevTextureToMeshZ))
			this.UpdateTiling();
		
		// Maintain previous state variables
		//this.prevScale = gameObject.transform.lossyScale;
		//this.prevTextureToMeshZ = this.textureToMeshZ;
	}
	
	[ContextMenu("UpdateTiling")]
	void UpdateTiling()
	{
		// A Unity plane is 10 units x 10 units
		//float planeSizeX = gameObject.renderer.material.mainTextureScale.x;
		//float planeSizeZ = gameObject.renderer.material.mainTextureScale.y;
		
		// Figure out texture-to-mesh width based on user set texture-to-mesh height
		//float textureToMeshX = ((float)this.texture.width/this.texture.height)*this.textureToMeshZ;

		// gameObject.renderer.material.SetTextureScale ("", new Vector2 (1.0f .x, 1.0f / gameObject.transform.lossyScale.z));
		gameObject.renderer.sharedMaterial.mainTextureScale = gameObject.transform.lossyScale * -1;
		//transform.renderer.material.mainTextureScale = new Vector2(XScale , YScale );
		// gameObject.renderer.material.mainTextureScale = new Vector2(1.0f,1.0f);
	}
}
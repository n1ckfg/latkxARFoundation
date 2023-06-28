using UnityEngine;
using System.Collections;

public class BrushInputTouchARFoundation : MonoBehaviour {

	public LightningArtist lightningArtist;
	public ShowHideGeneric colorPalette;
	public enum DrawMode { FREE, FIXED }
	public DrawMode drawMode = DrawMode.FREE;
	public float zPos = 1f;

	[HideInInspector] public bool touchActive = false;
	[HideInInspector] public bool touchDown = false;
	[HideInInspector] public bool touchUp = false;

	private float lastZ = 0f;

	void Awake() {
		if (lightningArtist == null) lightningArtist = GetComponent<LightningArtist>();
	}

	void Start() {
		if (drawMode == DrawMode.FIXED)	lightningArtist.target.transform.SetParent(Camera.main.transform, true);
		lastZ = zPos;
	}

	void Update() {
		touchDown = false;
		touchUp = false;

		if (!colorPalette.isTracking) {
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && GUIUtility.hotControl == 0) {
				touchActive = true;
				touchDown = true;
			} else if (Input.touchCount < 1 || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) {
				touchActive = false;
				touchUp = true;
				lastZ = zPos;
			}

			if (touchActive) {
				Vector3 p = Vector3.zero;

				if (drawMode == DrawMode.FREE) {
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0f));
					RaycastHit hit;

					if (Physics.Raycast(ray, out hit)) {
						p = hit.point;
						lastZ = p.z;
					} else {
						p = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, lastZ));
					}
				} else if (drawMode == DrawMode.FIXED) {
					p = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, zPos));
				}

				lightningArtist.target.transform.position = p;
			}

			lightningArtist.clicked = touchActive;

			//if (touchDown) {
			//lightningArtist.inputPlay();
			//}
		}
	}

}

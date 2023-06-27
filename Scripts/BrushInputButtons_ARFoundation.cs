using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushInputButtons_ARFoundation : MonoBehaviour {

	public LightningArtist lightningArtist;
    //public WebcamPhoto webcamPhoto;
	//public UnityEngine.XR.iOS.UnityARVideo arVideo;
	//public UnityARCameraManager_Custom arCameraManager;
	public ShowHideGeneric showHideGeneric;
	public int fontSize = 25;
	public bool showButtons = true;

	[HideInInspector] public float LABEL_START_X = 15.0f;
	[HideInInspector] public float LABEL_START_Y = 15.0f;
	[HideInInspector] public float LABEL_SIZE_X = Screen.width;//1920.0f;
	[HideInInspector] public float LABEL_SIZE_Y = 35.0f;
	[HideInInspector] public float LABEL_GAP_Y = 3.0f;
	[HideInInspector] public float BUTTON_SIZE_X = 200f; //250.0f;
	[HideInInspector] public float BUTTON_SIZE_Y = 90f; //130.0f;
	[HideInInspector] public float BUTTON_GAP_X = 5.0f;
	[HideInInspector] public string FLOAT_FORMAT = "F3";
	[HideInInspector] public string FONT_SIZE;

	private GUIStyle guiStyle;

	private int menuCounter = 1;
	private int menuCounterMax = 2;
	//private bool playButtonBlock = false;

	void Awake() {
		if (lightningArtist == null) lightningArtist = GetComponent<LightningArtist>();

		FONT_SIZE = "<size=" + fontSize + ">";
		guiStyle = new GUIStyle(); //create a new variable
		guiStyle.fontSize = (int)(fontSize * 1.5f);
		guiStyle.normal.textColor = Color.white;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) showButtons = !showButtons;
	}

	void OnGUI() {
		if (showButtons) {
			string isOn = "";

			if (menuCounter == 1) {
				// 1-1.
				Rect writeButton = new Rect(BUTTON_GAP_X, Screen.height - (1 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				//isOn = lightningArtist.showOnionSkin ? "ON" : "OFF";
				if (GUI.Button(writeButton, FONT_SIZE + "Write" + "</size>")) {
					lightningArtist.armWriteFile = true;
				}

				// 1-2.
				Rect freezeButton = new Rect(BUTTON_GAP_X, Screen.height - (3 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				/*
				isOn = arCameraManager.freezeUpdate ? "ON" : "OFF";
				if (GUI.Button(freezeButton, FONT_SIZE + "Freeze " + isOn + "</size>")) {
					//arVideo.freezeUpdate = !arVideo.freezeUpdate;
					arCameraManager.freezeUpdate = !arCameraManager.freezeUpdate;
					if (arCameraManager.freezeUpdate) {
						arCameraManager.m_session.Pause();
					} else {
						arCameraManager.m_session.Run();
					}
				}
				*/

				// 1-3.
				Rect undoButton = new Rect(BUTTON_GAP_X, Screen.height - (4 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				//isOn = lightningArtist.showOnionSkin ? "ON" : "OFF";
				if (GUI.Button(undoButton, FONT_SIZE + "Undo" + "</size>")) {
					lightningArtist.inputEraseLastStroke();
				}

				// 1-4.
				Rect colorButton = new Rect(BUTTON_GAP_X, Screen.height - (5 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				isOn = showHideGeneric.target[0].activeSelf ? "ON" : "OFF";
				if (GUI.Button(colorButton, FONT_SIZE + "Palette " + isOn + "</size>")) {
					if (showHideGeneric.target[0].activeSelf) {
						showHideGeneric.hideColor();
					} else {
						showHideGeneric.showColor();
					}
				}

				// 1-5.
				Rect onionButton = new Rect(BUTTON_GAP_X, Screen.height - (6 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				isOn = lightningArtist.showOnionSkin ? "ON" : "OFF";
				if (GUI.Button(onionButton, FONT_SIZE + "Onion Skin " + isOn + "</size>")) {
					lightningArtist.inputOnionSkin();
				}

				// 1-6.
				Rect copyFrameButton = new Rect(BUTTON_GAP_X, Screen.height - (7 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				//isOn = m_arCameraPostProcess.enabled ? "ON" : "OFF";
				if (GUI.Button(copyFrameButton, FONT_SIZE + "Copy Frame" + "</size>")) {
					lightningArtist.inputNewFrameAndCopy();
				}

				// 1-7.
				Rect newFrameButton = new Rect(BUTTON_GAP_X, Screen.height - (8 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				//isOn = m_arCameraPostProcess.enabled ? "ON" : "OFF";
				if (GUI.Button(newFrameButton, FONT_SIZE + "New Frame" + "</size>")) {
					lightningArtist.inputNewFrame();
				}

				// 1-8.
				Rect playButton = new Rect(BUTTON_GAP_X, Screen.height - (9 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				isOn = lightningArtist.isPlaying ? "Stop" : "Play";
				if (GUI.Button(playButton, FONT_SIZE + isOn + "</size>")) {
					//if (!lightningArtist.isPlaying && !playButtonBlock) {
						lightningArtist.inputPlay();
						//playButtonBlock = true;
					//} else {
						//lightningArtist.inputFrameBack(); // this is simpler than solving with script execution order
						//playButtonBlock = false;
					//}
				}

				// 1-9.
				Rect rewButton = new Rect(BUTTON_GAP_X, Screen.height - (10 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X/2f, BUTTON_SIZE_Y);
				//isOn = m_arCameraPostProcess.enabled ? "ON" : "OFF";
				if (GUI.Button(rewButton, FONT_SIZE + "<|" + "</size>")) {
					lightningArtist.inputFrameBack();
				}

				Rect ffButton = new Rect(BUTTON_GAP_X + (BUTTON_SIZE_X/2f), Screen.height - (10 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X/2f, BUTTON_SIZE_Y);
				//isOn = m_arCameraPostProcess.enabled ? "ON" : "OFF";
				if (GUI.Button(ffButton, FONT_SIZE + "|>" + "</size>")) {
					lightningArtist.inputFrameForward();
				}
			} else if (menuCounter == 2) {
                // 2-8.
                Rect layerChangeButton = new Rect(BUTTON_GAP_X, Screen.height - (10 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
                //isOn = lightningArtist.showOnionSkin ? "ON" : "OFF";
                if (GUI.Button(layerChangeButton, FONT_SIZE + "Next Layer" + "</size>")) {
                    lightningArtist.inputNextLayer();
                }
                
                // 2-7.
                Rect newLayerButton = new Rect(BUTTON_GAP_X, Screen.height - (9 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
                //isOn = lightningArtist.showOnionSkin ? "ON" : "OFF";
                if (GUI.Button(newLayerButton, FONT_SIZE + "New Layer" + "</size>")) {
                    lightningArtist.inputNewLayer();
                }

                // 2-6.
                /*
                Rect webcamButton = new Rect(BUTTON_GAP_X, Screen.height - (7 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
                isOn = webcamPhoto.isShowing ? "ON" : "OFF";
                if (GUI.Button(webcamButton, FONT_SIZE + "Webcam " + isOn + "</size>")) {
                    webcamPhoto.toggleCam();
                }
                */

                // 2-1.
                Rect readButton = new Rect(BUTTON_GAP_X, Screen.height - (1 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				//isOn = lightningArtist.showOnionSkin ? "ON" : "OFF";
				if (GUI.Button(readButton, FONT_SIZE + "Demo" + "</size>")) {
					lightningArtist.armReadFile = true;
				}
			}

			// 10.
			Rect menuButton = new Rect(BUTTON_GAP_X, Screen.height - (11 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
			//isOn = m_arCameraPostProcess.enabled ? "ON" : "OFF";
			if (GUI.Button(menuButton, FONT_SIZE + "MENU " + menuCounter + "</size>")) {
				menuCounter++;
				if (menuCounter > menuCounterMax) menuCounter = 1;
			}

			GUI.Label(new Rect(BUTTON_GAP_X * 2f, Screen.height - (12 * (BUTTON_SIZE_Y - BUTTON_GAP_X)) + fontSize, BUTTON_SIZE_X * 2, BUTTON_SIZE_Y), lightningArtist.statusText, guiStyle);
		}
	}

}

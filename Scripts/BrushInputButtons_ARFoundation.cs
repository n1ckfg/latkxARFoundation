using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class BrushInputButtons_ARFoundation : MonoBehaviour {

	public LightningArtist lightningArtist;
	public BrushInputTouchARFoundation latkInput;
	//public WebcamPhoto webcamPhoto;
	public Inference_informative onnx;
	public ARSession arMgr;
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
				Rect rewButton = new Rect(BUTTON_GAP_X, Screen.height - (10 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X / 2f, BUTTON_SIZE_Y);
				if (GUI.Button(rewButton, FONT_SIZE + "<|" + "</size>")) {
					lightningArtist.inputFrameBack();
				}

				Rect ffButton = new Rect(BUTTON_GAP_X + (BUTTON_SIZE_X / 2f), Screen.height - (10 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X / 2f, BUTTON_SIZE_Y);
				if (GUI.Button(ffButton, FONT_SIZE + "|>" + "</size>")) {
					lightningArtist.inputFrameForward();
				}

				Rect playButton = new Rect(BUTTON_GAP_X, Screen.height - (9 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				isOn = lightningArtist.isPlaying ? "Stop" : "Play";
				if (GUI.Button(playButton, FONT_SIZE + isOn + "</size>")) {
					lightningArtist.inputPlay();
				}

				Rect newFrameButton = new Rect(BUTTON_GAP_X, Screen.height - (8 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				if (GUI.Button(newFrameButton, FONT_SIZE + "New Frame" + "</size>")) {
					lightningArtist.inputNewFrame();
				}

				Rect copyFrameButton = new Rect(BUTTON_GAP_X, Screen.height - (7 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				if (GUI.Button(copyFrameButton, FONT_SIZE + "Copy Frame" + "</size>")) {
					lightningArtist.inputNewFrameAndCopy();
				}

				Rect onionButton = new Rect(BUTTON_GAP_X, Screen.height - (6 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				isOn = lightningArtist.showOnionSkin ? "ON" : "OFF";
				if (GUI.Button(onionButton, FONT_SIZE + "Onion Skin " + isOn + "</size>")) {
					lightningArtist.inputOnionSkin();
				}

				Rect colorButton = new Rect(BUTTON_GAP_X, Screen.height - (5 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				isOn = showHideGeneric.target[0].activeSelf ? "ON" : "OFF";
				if (GUI.Button(colorButton, FONT_SIZE + "Palette " + isOn + "</size>")) {
					if (showHideGeneric.target[0].activeSelf) {
						showHideGeneric.hideColor();
					} else {
						showHideGeneric.showColor();
					}
				}

				Rect undoButton = new Rect(BUTTON_GAP_X, Screen.height - (4 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				if (GUI.Button(undoButton, FONT_SIZE + "Undo" + "</size>")) {
					lightningArtist.inputEraseLastStroke();
				}

				Rect OnnxButton = new Rect(BUTTON_GAP_X, Screen.height - (3 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				if (GUI.Button(OnnxButton, FONT_SIZE + "Contour" + "</size>")) {
					onnx.DoInference();
				}

				Rect writeButton = new Rect(BUTTON_GAP_X, Screen.height - (1 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				if (GUI.Button(writeButton, FONT_SIZE + "Write" + "</size>")) {
					lightningArtist.armWriteFile = true;
				}
			} else if (menuCounter == 2) {
				Rect layerChangeButton = new Rect(BUTTON_GAP_X, Screen.height - (10 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
                if (GUI.Button(layerChangeButton, FONT_SIZE + "Next Layer" + "</size>")) {
                    lightningArtist.inputNextLayer();
                }
                
                Rect newLayerButton = new Rect(BUTTON_GAP_X, Screen.height - (9 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
                if (GUI.Button(newLayerButton, FONT_SIZE + "New Layer" + "</size>")) {
                    lightningArtist.inputNewLayer();
                }

				Rect freezeButton = new Rect(BUTTON_GAP_X, Screen.height - (8 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				isOn = !arMgr.enabled ? "ON" : "OFF";
				if (GUI.Button(freezeButton, FONT_SIZE + "Freeze " + isOn + "</size>")) {
					arMgr.enabled = !arMgr.enabled;
				}

				/*
                Rect webcamButton = new Rect(BUTTON_GAP_X, Screen.height - (7 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
                isOn = webcamPhoto.isShowing ? "ON" : "OFF";
                if (GUI.Button(webcamButton, FONT_SIZE + "Webcam " + isOn + "</size>")) {
                    webcamPhoto.toggleCam();
                }
                */

				Rect drawModeButton = new Rect(BUTTON_GAP_X, Screen.height - (7 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				isOn = latkInput.drawMode == BrushInputTouchARFoundation.DrawMode.FREE ? "ON" : "OFF";
				if (GUI.Button(drawModeButton, FONT_SIZE + "Raycast " + isOn + "</size>")) {
					if (latkInput.drawMode == BrushInputTouchARFoundation.DrawMode.FREE) {
						latkInput.drawMode = BrushInputTouchARFoundation.DrawMode.FIXED;
					} else if (latkInput.drawMode == BrushInputTouchARFoundation.DrawMode.FIXED) {
						latkInput.drawMode = BrushInputTouchARFoundation.DrawMode.FREE;
					}
				}

				Rect deleteButton = new Rect(BUTTON_GAP_X, Screen.height - (6 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
				if (GUI.Button(deleteButton, FONT_SIZE + "Delete Frame" + "</size>")) {
					lightningArtist.inputDeleteFrame();
				}

				Rect readButton = new Rect(BUTTON_GAP_X, Screen.height - (1 * (BUTTON_SIZE_Y - BUTTON_GAP_X)), BUTTON_SIZE_X, BUTTON_SIZE_Y);
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

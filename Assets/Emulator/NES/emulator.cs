using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public class emulator : MonoBehaviour {

	NesEngine engine;
	Thread oThread;
	Texture2D texture;

	int _joypad;

	public Renderer[] observers;

	// Use this for initialization
	void Start () {
		texture = new Texture2D(256, 240, TextureFormat.ARGB32, false);

		foreach (Renderer rd in observers) {
			rd.material.mainTexture = texture;
		}

		engine = new NesEngine();
		engine.LoadCart(System.IO.Path.Combine(Application.streamingAssetsPath, "Super_mario_brothers.nes"));

		oThread = new Thread(new ThreadStart(engine.RunCart));
		oThread.Start();
	}

	void UpdateTexture () {
		uint [] buffer = engine.myPPU.offscreenBuffer;
		Color[] colors = new Color[buffer.Length];
		for (int i=0; i<buffer.Length; i++) {
			colors[i] = UIntToColor(buffer[i]);
		}

		texture.SetPixels(colors);
		texture.Apply(false);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTexture();

		_joypad = 0;

		if (Input.GetKey(KeyCode.UpArrow)) {
			_joypad = _joypad | (int)Joypad.Button.BUTTON_UP;
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			_joypad = _joypad | (int)Joypad.Button.BUTTON_DOWN;
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			_joypad = _joypad | (int)Joypad.Button.BUTTON_LEFT;
		}

		if (Input.GetKey(KeyCode.RightArrow)) {
			_joypad = _joypad | (int)Joypad.Button.BUTTON_RIGHT;
		}

		if (Input.GetKey(KeyCode.Return)) {
			_joypad = _joypad | (int)Joypad.Button.BUTTON_START;
		}

		if (Input.GetKey(KeyCode.Space)) {
			_joypad = _joypad | (int)Joypad.Button.BUTTON_SELECT;
		}

		if (Input.GetKey(KeyCode.Z)) {
			_joypad = _joypad | (int)Joypad.Button.BUTTON_A;
		}

		if (Input.GetKey(KeyCode.X)) {
			_joypad = _joypad | (int)Joypad.Button.BUTTON_B;
		}

		engine.myJoypad.WritePad((byte)_joypad);
	}

	void OnDestroy () {
		oThread.Abort();

		engine = null;
	}

	private Color UIntToColor(uint color)
	{
		if (color <= 16777215) {
			byte r = (byte)(color >> 16);
		    byte g = (byte)(color >> 8);
		    byte b = (byte)(color >> 0);
		    return new Color(r/255f, g/255f, b/255f, 1);
		} else {
			return new Color(0, 0, 0, 0);
		}
	}
}

using UnityEngine;
using System.Collections;

//This file implements an emulation of the Touch interface, except that in the editor, it just replaces them with 
//touch commands from the mouse. 
public class TouchHandler
{
    public static SimpleTouch[] touches
    {
        get
        {
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR

            SimpleTouch[] t = new SimpleTouch[Input.touches.Length];

            for (int i = 0; i < Input.touches.Length; ++i)
            {
                t[i].deltaPosition = -Input.GetTouch(i).deltaPosition;
                t[i].deltaTime = Input.GetTouch(i).deltaTime;
                t[i].fingerId = Input.GetTouch(i).fingerId;
                t[i].position = Input.GetTouch(i).position;
                t[i].tapCount = Input.GetTouch(i).tapCount;
                t[i].phase = Input.GetTouch(i).phase;
            }
            return t;
#else

            if ( instance == null )
			{
				//make the singleton
				GameObject go = new GameObject("TouchHandler");
				go.hideFlags = HideFlags.HideAndDontSave;
				instance = go.AddComponent<TouchHandlerGameObject>();
			}
			
			SimpleTouch[] t;

			if ( Input.GetMouseButton(0))
			{
				t = new SimpleTouch[1];
				t[0].deltaPosition = 	instance.prevMousePos - Input.mousePosition;
				t[0].deltaTime = 		Time.deltaTime;
				t[0].fingerId = 		10;
				t[0].position = 		Input.mousePosition;
				t[0].tapCount = 		1;
				t[0].phase = 			Input.GetMouseButtonDown(0) ? TouchPhase.Began : 
										(t[0].deltaPosition.sqrMagnitude != 0 ? TouchPhase.Moved : TouchPhase.Stationary);
			}
			else
			{
				t = new SimpleTouch[0];
			}
			return t;
#endif
        }
    }

    static public SimpleTouch GetTouch(int a_i)
    {
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
        SimpleTouch t = new SimpleTouch();

        t.deltaPosition = -Input.GetTouch(a_i).deltaPosition;
        t.deltaTime = Input.GetTouch(a_i).deltaTime;
        t.fingerId = Input.GetTouch(a_i).fingerId;
        t.position = Input.GetTouch(a_i).position;
        t.tapCount = Input.GetTouch(a_i).tapCount;
        t.phase = Input.GetTouch(a_i).phase;

        return t;

#else
		
		if ( instance == null )
		{
			//make the singleton
			GameObject go = new GameObject("TouchHandler");
			go.hideFlags = HideFlags.HideAndDontSave;
			instance = go.AddComponent<TouchHandlerGameObject>();
		}
		
		SimpleTouch t = new SimpleTouch();
		
		t.deltaPosition = 	instance.prevMousePos - Input.mousePosition;
		t.deltaTime = 		Time.deltaTime;
		t.fingerId = 		10;
		t.position = 		Input.mousePosition;
		t.tapCount = 		1;
		t.phase = 			Input.GetMouseButtonDown(0) ? TouchPhase.Began :
							(t.deltaPosition.sqrMagnitude != 0 ? TouchPhase.Moved : TouchPhase.Stationary);

		if ( !Input.GetMouseButton(0) )
		{
			t.phase = TouchPhase.Ended;
		}

		return t;
#endif
    }

    static public int touchCount
    {
        get
        {
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
            return Input.touchCount;
#else
            int i = 0;
            i += (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)) ? 1 : 0;
            i += (Input.GetMouseButton(1) || Input.GetMouseButtonUp(1)) ? 1 : 0;
            i += (Input.GetMouseButton(2) || Input.GetMouseButtonUp(2)) ? 1 : 0;

            return i;
#endif
        }
    }
    [System.NonSerialized]
    private static TouchHandlerGameObject instance = null;
}

public struct SimpleTouch
{
    public Vector2 deltaPosition;
    public float deltaTime;
    public int fingerId;
    public Vector2 position;
    public int tapCount;
    public TouchPhase phase;
};


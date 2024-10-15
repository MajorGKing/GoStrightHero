using UnityEngine;
using System.Collections;

public partial class TouchPoint : TMonoSingleton<TouchPoint> 
{
	public Vector3 fingerPos;
    //private TouchPointCollider touchPointCollider;

	public override void Initialize()
    {
        //Debug.Log("init!!");
        
        gameObject.name = "TouchPoint";

        gameObject.transform.position = new Vector3(10f, 10f, 0);

        gameObject.AddComponent<BoxCollider2D>();

        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, 0.1f);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;

        //gameObject.AddComponent<TouchPointCollider>();

        //touchPointCollider = (TouchPointCollider)gameObject.GetComponent(typeof(TouchPointCollider));
    }

	// Update is called once per frame
	void Update ()
    {
        //#if UNITY_EDITOR_WIN
        if (false != Input.GetMouseButton(0))
        {
            fingerPos = Input.mousePosition;
            Vector2 pos = fingerPos;
            Vector2 realWorldPos = Camera.main.ScreenToWorldPoint(pos);

            gameObject.transform.position = realWorldPos;

            //Debug.Log("POS X : " + gameObject.transform.position.x);
            //Debug.Log("POS Y : " + gameObject.transform.position.y);
        }
        else
        {
            gameObject.transform.position = new Vector3(10f, 10f, 0);
            //touchPointCollider.InputStopReset();
        }
        //#endif
	}

    public void PosReset()
    {
        gameObject.transform.position = new Vector3(10f, 10f, 0);
    }

	public override void Destroy()
	{
		
	}
}

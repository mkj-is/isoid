using UnityEngine;

public class Marquee : MonoBehaviour
{
    public string message = "";
    public float scrollSpeed = 50;
    public GUIStyle textStyle;
   
    Rect messageRect;
    Rect groupRect;

    void Start()
    {
        groupRect = new Rect(Screen.width / 4, 80, Screen.width / 2, textStyle.CalcSize(new GUIContent(message)).y);
    }
   
    void OnGUI ()
    {
        GUI.BeginGroup(groupRect);
        // Set up the message's rect if we haven't already
        if (messageRect.width == 0) {
            Vector2 dimensions = textStyle.CalcSize(new GUIContent(message));
           
            // Start the message past the left side of the screen
            messageRect.x = Screen.width / 2;
            messageRect.width  =  dimensions.x;
            messageRect.height =  dimensions.y;
        }
       
        messageRect.x -= Time.deltaTime * scrollSpeed;

        // If the message has moved past the left side, move it back to the right
        if ((messageRect.x + messageRect.width) < 0)
        {
            messageRect.x = groupRect.width;
        }

        GUI.Label(messageRect, message, textStyle);


        GUI.EndGroup();
    }
}
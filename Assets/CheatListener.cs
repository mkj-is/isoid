using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheatListener : MonoBehaviour {
    public List<string> m_Codes = new List<string>();
    public float m_ClearDelay = 2.0f;
    public GameObject m_Receiver = null;

    private string m_Keys = "";
    private float m_LastKeyTime;
 
    void Start ()
    {
        m_LastKeyTime = Time.time;
        for (int i = 0; i < m_Codes.Count; i++)
        {
            m_Codes [i] = m_Codes [i].ToLower ();
        }
    }
 
    void Update ()
    {
        if (Input.anyKey)
        {
            m_LastKeyTime = Time.time;
        }
        else if (Time.time - m_LastKeyTime > m_ClearDelay)
        {
            m_Keys = "";
        }

        m_Keys += Input.inputString.ToLower ();
 
        if (m_Codes.Contains (m_Keys))
        {
            string message = "On" + char.ToUpper (m_Keys [0]) + m_Keys.Substring (1) + "Code";
 
            if (m_Receiver == null)
            {
                SendMessage (message);
            }
            else
            {
                m_Receiver.SendMessage (message);
            }

            m_Keys = "";
        }
    }
 
    /*
    void OnTestCode ()
    // Handle codes in scripts on the target GameObject (or the current one if
    // no target is set) by implementing functions named On{Name}Code - note
    // that the codes are set to be all-lowercase except for the first letter.
    {
        Debug.Log ("We caught a code!");
    }
    */
}

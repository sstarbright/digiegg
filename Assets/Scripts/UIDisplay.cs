using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplay : MonoBehaviour
{
    string _crabName = "";
    string _parentName = "";
    string _crabQuote = "";
    string _crabKind = "";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string CrabName {
        get { return _crabName; }
        set { _crabName = value; }
    }
    public string ParentName {
        get { return _parentName; }
        set { _parentName = value; }
    }
    public string CrabQuote {
        get { return _crabQuote; }
        set { _crabQuote = value; }
    }
    public string CrabKind {
        get { return _crabKind; }
        set { _crabKind = value; }
    }
}

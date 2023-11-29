using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using TMPro;


public class NodeParser1 : MonoBehaviour
{

    public DialogueGraph graph;
    Coroutine _parser;
    public TMP_Text speaker;
    public TMP_Text dialogue;
    public Image speakerImage;

    // Start is called before the first frame update
    private void Start()
    {
        foreach (BaseNode b in graph.nodes)
        {
            if (b.GetString() == "Start")
            {
                graph.current = b;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());
    }

    IEnumerator ParseNode()
    {
        BaseNode b = graph.current;
        string data = b.GetString();
        string[] dataParts = data.Split('/');
        if (dataParts[0] == "DialogueNode")
        {
            //Run dialogue processing 
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            speakerImage.sprite = b.GetSprite();
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            NextNode("exit");
        }
    }

    public void NextNode(string fieldName)
    {
        if (_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }

        foreach (NodePort p in graph.current.Ports)
        {
            if (p.fieldName == fieldName)
            {
                graph.current = p.Connection.node as BaseNode;
                break;
            }

        }
        _parser = StartCoroutine(ParseNode());
    }

    // Update is called once per frame
    void Update()
    {

    }
}

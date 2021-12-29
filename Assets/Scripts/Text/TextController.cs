using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour, ISubscribe
{
    [System.Serializable]
    public class ScreenText
    {
        public bool finishedDisplaying = false;
        public string text;
        public float displayTimer = 0;
        public float timeToDisplay;

        public ScreenText(string text)
        {
            this.text = text;            
            timeToDisplay = GetDisplayTime();
            Debug.Log($"Display Time: {timeToDisplay} , Text : {text}");
        }
        
        public float GetDisplayTime()
        {
            float charTime = 0.10f; //each char represent 1/4 seconds
            return text.ToCharArray().Length * charTime;
        }

        public void UpdateTimer()
        {
            if (displayTimer >= timeToDisplay)
            {
                finishedDisplaying = true;
            }
            else
            {
                displayTimer += Time.deltaTime;
            }
        }
    }
    
    public Text mainTextField;
    public ScreenText currentText;
    Queue<ScreenText> mainTextQueue = new Queue<ScreenText>();

    [SerializeField] protected float displayTimer = 0;
    [SerializeField] protected float displayTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentText != null && !currentText.finishedDisplaying)
        {
            mainTextField.text = currentText.text;
            currentText.UpdateTimer();
        }
        else if (mainTextQueue.Count > 0)
        {
            DequeueText();
        }
        else
        {
            mainTextField.text = "";
        }
    }
    
    protected void OnEnable()
    {
        Subscribe();
    }

    protected void OnDisable()
    {
        Unsubscribe();
    }

    public void Subscribe()
    {
        EventHandler.OnTextControllerMsg += QueueText;
    }

    public void Unsubscribe()
    {
        EventHandler.OnTextControllerMsg -= QueueText;
    }

    public void QueueText(string text)
    {
        mainTextQueue.Enqueue(new ScreenText(text));
        DebugQueue();
    }

    public void DequeueText()
    {
        currentText = mainTextQueue.Dequeue();
        DebugQueue();
    }

    void DebugQueue()
    {
        Debug.Log($"Queue Count is : {mainTextQueue.Count} ");
    }
}

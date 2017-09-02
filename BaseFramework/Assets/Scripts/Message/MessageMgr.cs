using System;
using System.Collections;

public delegate void  MessagetListenerDelegate(MessageBase evt);

public class MessageMgr
{
    static MessageMgr instance;
    public static MessageMgr GetInstance()
    {
        if (instance == null)
            instance = new MessageMgr();
        return instance;
    }

    private Hashtable listeners = new Hashtable();

    public void AddEventListener(MessageType eventType, MessagetListenerDelegate listener)
    {
        MessagetListenerDelegate MessagetListenerDelegate = this.listeners[eventType] as MessagetListenerDelegate;
        MessagetListenerDelegate = (MessagetListenerDelegate)Delegate.Combine(MessagetListenerDelegate, listener);
        this.listeners[eventType] = MessagetListenerDelegate;
    }

    public void RemoveEventListener(MessageType eventType, MessagetListenerDelegate listener)
    {
        MessagetListenerDelegate MessagetListenerDelegate = this.listeners[eventType] as MessagetListenerDelegate;
        if (MessagetListenerDelegate != null)
            MessagetListenerDelegate = (MessagetListenerDelegate)Delegate.Remove(MessagetListenerDelegate, listener);
        this.listeners[eventType] = MessagetListenerDelegate;
    }

    public void RemoveAll()
    {
        this.listeners.Clear();
    }

    public void DispatchEvent(MessageBase evt)
    {
        MessagetListenerDelegate MessagetListenerDelegate = this.listeners[evt.Type] as MessagetListenerDelegate;
        if (MessagetListenerDelegate != null)
        {
            try
            {
                MessagetListenerDelegate(evt);
            }
            catch(Exception ex)
            {
                throw new Exception(string.Concat(new string[]
                {
                    "Error dispatching event", 
                    evt.Type.ToString(),
                    ": ",
                    ex.Message,
                    " ",
                    ex.StackTrace
                }), ex);
            }
        }
    }

}


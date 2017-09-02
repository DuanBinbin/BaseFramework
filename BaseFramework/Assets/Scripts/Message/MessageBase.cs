using System;
using System.Collections;

public class MessageBase
{
    protected Hashtable arguments;
    protected MessageType type;
    protected object sender;
    public MessageType Type
    {
        get
        {
            return this.type;
        }
        set
        {
            this.type = value;
        }
    }

    public IDictionary Params
    {
        get
        {
            return this.arguments;
        }
        set
        {
            this.arguments = (value as Hashtable);
        }
    }

    public object Sender
    {
        get
        {
            return this.sender;
        }
        set
        {
            this.sender = value;
        }
    }

    public override string ToString()
    {
        return this.type + "[" + ((this.sender == null) ? "null" : this.sender.ToString()) + "]";
    }

    public MessageBase Clone()
    {
        return new MessageBase(this.type, this.arguments, Sender);
    }

    public MessageBase(MessageType type, Object sender)
    {
        this.Type = type;
        Sender = sender;
        if (this.arguments == null)
        {
            this.arguments = new Hashtable();
        }
    }

    public MessageBase(MessageType type, Hashtable args, Object sender)
    {
        this.Type = type;
        this.arguments = args;
        Sender = sender;
        if (this.arguments == null)
            this.arguments = new Hashtable();
    }

}



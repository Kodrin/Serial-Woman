using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPublish
{
    public void Publish();
}

public interface ISubscribe
{
    public void Subscribe();
    public void Unsubscribe();
}


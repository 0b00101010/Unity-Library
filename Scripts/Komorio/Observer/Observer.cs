using System;
using System.Collections.Generic;

/// <summary>
/// The Observer is responsible for receiving values after registering somewhere.
/// </summary>
public interface Observer{
    void Notification(params object[] args);
}
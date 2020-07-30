using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public class Event : UnityEvent { }

[Serializable]
public class Event<T> : UnityEvent<T> { }

[Serializable]
public class Event<T1, T2> : UnityEvent<T1, T2> { }

[Serializable]
public class Event<T1, T2, T3> : UnityEvent<T1, T2, T3> { }

[Serializable]
public class Event<T1, T2, T3, T4> : UnityEvent<T1, T2, T3, T4> { }
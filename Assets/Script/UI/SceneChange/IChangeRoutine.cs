using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChangeRoutine
{
    IEnumerator Execute(string sceneName);
}

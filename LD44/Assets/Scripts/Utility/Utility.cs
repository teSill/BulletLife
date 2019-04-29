using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utility : MonoBehaviour {

    public static string GetRandomString(int length) {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[Random.Range(0, s.Length)]).ToArray());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOjb : MonoBehaviour
{
    public void DeleteCharacter()
    {
        Destroy(transform.root.gameObject);
    }
}

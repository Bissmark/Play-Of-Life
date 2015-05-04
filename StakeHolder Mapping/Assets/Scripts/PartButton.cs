using UnityEngine;
using System.Collections;

public class PartButton : MonoBehaviour {

    public PartComponent attached_part;

    public PartType type = PartType.NONE;
    public PartUseage constraint = PartUseage.NONE;
}

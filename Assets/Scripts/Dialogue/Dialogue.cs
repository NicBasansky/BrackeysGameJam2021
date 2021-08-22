using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jam.Dialogue
{
    [System.Serializable]
    public class Dialogue
    {
        //public string speakerName;

        //public Sprite speakerImage;

        //public bool isNPC = false;

        [TextAreaAttribute(3, 10)]
        public string[] sentences;

    }

}
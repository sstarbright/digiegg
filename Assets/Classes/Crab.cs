using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

enum CrabRarity {
    Common,
    Rare,
    Legendary,
    Gold,
    Diamond,
    Platinum
}

enum CrabEffect {
    None,
    Flaming,
    Bubbly,
    Intense, //Cartoon action lines
    Lovely,
    Important, //Cut to black on hatch/view, then switch on spotlight
    Winged, //Float up and down with low poly wings
    Evil //Evil pulsing red glow from eyes
}

enum CrabSize {
    Normal,
    Tiny,
    Large,
    XL
}

namespace Crab {
    const string[] cAnimations = {};
    const Texture2D[,] cTextures = {};
    public class Crab {
        CrabRarity cRarity;
        string cClass;
        (Color, Color, Color, Color) cColors;
        string cName;
        (string, string) cParents;
        string cHatcher;
        string cFirstWords;
        int cAnimation;
        (int, int) cAccessories;
        int cRide;
        Texture2D[] cTexture;
        CrabEffect[] cEffect;
        CrabSize cSize;

        //If we already have the crab generated and wanna recreate it
        public Crab(
            CrabRarity cRarity,
            string cClass,
            (Color, Color, Color, Color) cColors,
            string cName,
            (string, string) cParents,
            string cHatcher,
            string cFirstWords,
            int cAnimation,
            (int, int) cAccessories,
            int cRide,
            int cTextureIndex,
            CrabSize cSize
        ) {

        }

        public Crab(
            (string, string) cParents,
            string cHatcher,
            string cFirstWords
        ) {

        }
    }
}

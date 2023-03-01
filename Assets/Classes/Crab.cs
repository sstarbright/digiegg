using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Adjectives;

public enum CrabRarity {
    Common,
    Rare, //Has white sparkles
    Legendary, //Has purple sparkles
    Gold, //Has yellow sparkles
    Diamond, //Has blue sparkles
    Platinum //Has every sparkle color, along with fast tiny white sparkles
};

public enum CrabEffect {
    None,
    Flaming, //Fire effect
    Bubbly, //Bubble effect
    Intense, //Cartoon action lines
    Lovely, //Hearts effect
    Important, //Cut to black on hatch/view, then switch on spotlight
    Winged, //Float up and down with low poly wings
    Evil, //Evil pulsing red glow from eyes,
    Digital //Glitchy effect on crab
};

public enum CrabSize {
    Normal,
    Tiny, //Half size
    Large, //1.5x size
    XL //2x size
};

public enum EggType {
    Normal, //Colored beige/yellowish
    Silver, //Will always be at least rare, higher chance of higher rarities as well - Has a grey reflective texture with white sparkles
    Gold, //Will always be at least Gold, even higher chance of higher rarities - Has a gold reflective texture with yellow sparkles
    Sparkling, //Basically silver, but also guaranteed to have an effect in first name of class - Has a white specular texture with white sparkles
    Shimmering, //Basically gold, but also guaranteed to have an effect in first name of class - Has a blue specular texture with yellow sparkles
    Rainbow //Guaranteed Platinum, with an effect in first name of class - RGB rainbow fade, with Platinum crab sparkles
};




namespace Crab {
    public class Crab {
        readonly Dictionary<string, CrabEffect> StringToEffect = new Dictionary<string, CrabEffect>(){
            {"Flaming", CrabEffect.Flaming},
            {"Bubbly", CrabEffect.Bubbly},
            {"Intense", CrabEffect.Intense},
            {"Lovely", CrabEffect.Lovely},
            {"Important", CrabEffect.Important},
            {"Winged", CrabEffect.Winged},
            {"Evil", CrabEffect.Evil},
            {"Digital", CrabEffect.Digital}
        };
        readonly Dictionary<CrabEffect, string> EffectToString = new Dictionary<CrabEffect, string>(){
            {CrabEffect.Flaming, "Flaming"},
            {CrabEffect.Bubbly, "Bubbly"},
            {CrabEffect.Intense, "Intense"},
            {CrabEffect.Lovely, "Lovely"},
            {CrabEffect.Important, "Important"},
            {CrabEffect.Winged, "Winged"},
            {CrabEffect.Evil, "Evil"},
            {CrabEffect.Digital, "Digital"}
        };
        readonly string[] cAnimations = {};
        readonly Texture2D[,] cTextures = {};
        readonly int[][] rarityProbabilitySquare = {
            new int[6]{1, 2, 5, 10, 25, 57}, //Normal Egg
            new int[5]{2, 5, 10, 25, 58}, //Silver
            new int[3]{10, 25, 65}, //Gold
            new int[5]{2, 5, 10, 25, 58}, //Sparkling
            new int[3]{10, 25, 65} //Shimmering
        };
        CrabRarity cRarity = CrabRarity.Platinum; //Increases crab value, adds a sparkle effect
        HashSet<string> cClass; //Randomized adjectives, some of which cause cEffects
        (Color, Color, Color, Color) cColors; //Randomized colors for each texture of the crab
        string cName; //Player-given name of the crab
        (string, string) cParents; //Player-given names of the crab's parents (will say Unknown if egg isn't the result of breeding)
        string cHatcher; //Chat member name who hatched the egg, randomly spliced together if their crabs are bred
        string cFirstWords; //Chat message that hatched the egg, randomly spliced together if their crabs are bred
        int cAnimation; //Random animation from a set of available animations for the crab model
        (int, int) cAccessories; //Random front and back accessories
        int cRide; //Random ride accessory
        Texture2D[] cTexture; //Random set of textures
        List<CrabEffect> cEffects = new List<CrabEffect>(); //Random effects caused by cClass
        CrabSize cSize; //Random crab size

        //
        public Crab(
            (string, string) cParents,
            string cHatcher,
            string cFirstWords,
            EggType cEggType
        ) {
            cClass = new HashSet<string>();
            int randIndex;
            int randRarity;
            CrabEffect randEffect;
            int probAmount = 0;
            int probTotal;
            int numAdjectives = 3;
            string currentAdjective;
            switch (cEggType) {
                case EggType.Sparkling:
                case EggType.Shimmering:
                    randEffect = (CrabEffect)Random.Range(1,8);
                    numAdjectives = 2;
                    cClass.Add(EffectToString.GetValueOrDefault(randEffect));
                    cEffects.Add(randEffect);
                    goto case EggType.Normal;
                case EggType.Normal:
                case EggType.Silver:
                case EggType.Gold:
                    randIndex = Random.Range(0, AdjectiveList.List.Length-1);
                    currentAdjective = AdjectiveList.List[randIndex];
                    for (int i=0;i<numAdjectives;i++) {
                        while (cClass.Contains(currentAdjective)) {
                            randIndex = Random.Range(0, AdjectiveList.List.Length-1);
                            currentAdjective = AdjectiveList.List[randIndex];
                            currentAdjective = currentAdjective.Substring(0,1).ToUpper() + currentAdjective.Substring(1);
                        }
                        cClass.Add(currentAdjective);
                        CrabEffect possibleEffect;
                        if (StringToEffect.TryGetValue(currentAdjective, out possibleEffect))
                            cEffects.Add(possibleEffect);
                    }
                    randRarity = Random.Range(1, 100);
                    probAmount = 0;
                    probTotal = rarityProbabilitySquare[(int)cEggType].Sum();
                    for (int i=0; i<rarityProbabilitySquare[(int)cEggType].Length; i++) {
                        probAmount += rarityProbabilitySquare[(int)cEggType][i];
                        if (randRarity <= probAmount) {
                            break;
                        } else
                            cRarity--;
                    }
                    break;
                case EggType.Rainbow:
                    randEffect = (CrabEffect)Random.Range(1,8);
                    cClass.Add(EffectToString.GetValueOrDefault(randEffect));
                    cEffects.Add(randEffect);
                    randIndex = Random.Range(0, AdjectiveList.List.Length-1);
                    currentAdjective = AdjectiveList.List[randIndex];
                    for (int i=0;i<numAdjectives;i++) {
                        while (cClass.Contains(currentAdjective)) {
                            randIndex = Random.Range(0, AdjectiveList.List.Length-1);
                            currentAdjective = AdjectiveList.List[randIndex];
                            currentAdjective = currentAdjective.Substring(0,1).ToUpper() + currentAdjective.Substring(1);
                        }
                        cClass.Add(currentAdjective);
                        CrabEffect possibleEffect;
                        if (StringToEffect.TryGetValue(currentAdjective, out possibleEffect))
                            cEffects.Add(possibleEffect);
                    }
                    break;
            }
        }
    }
}

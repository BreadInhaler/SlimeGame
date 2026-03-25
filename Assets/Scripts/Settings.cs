using UnityEngine;

public static class Settings{
    public static float gravityModifier=15.8f;

    public static bool XOR(bool a, bool b){ return a!=b; }//used for destinksion and usability
    public static bool OR(bool a, bool b){ return a||b; }//used for destinksion
    public static bool AND(bool a, bool b){ return a&&b; }//most likely never to be used
    public static bool NOT(bool a){ return !a; }//most likely never to be used
    public static bool NAND(bool a, bool b){ return !(a&&b); }//most likely never to be used
    public static bool NOR(bool a, bool b){ return !(a||b); }//most likely never to be used
    public static bool NXOR(bool a, bool b){ return a==b; }//most likely never to be used
    public static bool IsInLayerMask(GameObject obj, LayerMask mask){
        return ((1 << obj.layer) & mask) != 0;
    }
}

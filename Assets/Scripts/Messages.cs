using UnityEngine;

public class Messages : MonoBehaviour
{
  
    public static readonly string[] Miss = new string[] 
    {
        "Szefie, nie trafiłem, następnym razem",
        "Ahhh pudlo",
        "Moze nastepnym razem",
        "Jak to sie stalo, ze nie trafilem"
    };

    public static readonly string[] Kill = new string[]
    {
        "Zdychaj!!! ",
        "Haa! zabilem ",
        "Spoczywaj w pokoju "
    };

    public static readonly string[] Move = new string[]
    {
        "Lece kapitanie",
        "Do boju kapitanie"  
    };

    public static readonly string[] BlockMove = new string[]
    {
        //nie  moge sie ruszyc
        //zablokowali mnie
    };
    public static readonly string[] BlockAttack = new string[]
    {
        //zablokowali mi atak
    };

    //ShowMessage($"Czas na Szmaragdy"); - zmiana tury na Szmaragdy
    // ShowMessage($"Do boju Zloci"); - zmiana tury na Zlotych
    // uleczony, zdrowiej - po uleczeniu przez lecznice

    //$"{enemy.name} Dostał za np 50 dmg

    // Dla  zablokowania ruchu i ataku
    // Nie moge zablokowac {enemy.name}" - gdy nie moge zablokowac jednostki bo ma juz blokade
    // Kapitanie, {enemy.name} został zablokowany" - gdy nie moge zablokowac jednostki bo ma juz blokade
    //Krol Szmaragdow zostal pokonany, Zloci wygrali
    //Krol Zlotych zostal pokonany, Szmaragdy wygraly

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellcasting : MonoBehaviour
{
    private KeyBinds keybinds;
    
    public enum Spell1 {Firebolt};
    private Spell1 spell1;

    public enum Spell2 {SuperJump};
    private Spell2 spell2;

    public enum Spell3 {TimeSlow};
    private Spell3 spell3;

    private void Start()
    {
        keybinds = GetComponent<KeyBinds>();
    }

    void Update()
    {
        if(Input.GetKey(keybinds.spellMode))
        {
            if (Input.GetKeyDown(keybinds.spell1))
            {
                if (spell1 == Spell1.Firebolt)
                {
                    FireBolt();
                }
            }

            if (Input.GetKeyDown(keybinds.spell2))
            {
                if (spell2 == Spell2.SuperJump)
                {
                    SuperJump();
                }
            }

            if (Input.GetKeyDown(keybinds.spell3))
            {
                if (spell3 == Spell3.TimeSlow)
                {
                    TimeSlow();
                }
            }
        }
    }

    private void FireBolt()
    {

    }

    private void SuperJump()
    {

    }

    private void TimeSlow()
    {

    }
}

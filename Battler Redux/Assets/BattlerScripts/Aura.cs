using UnityEngine;
using System.Collections;

public enum Element { Physical, Shock, Fire, Aqua, Order, Chaos }

[System.Serializable]
public class Aura
{
    
    public float Shock = 0;
    public float Fire = 0;
    public float Aqua = 0;
    public float Order = 0;
    public float Chaos = 0;
    

    public float Get(Element _ele)
    {
        switch (_ele)
        {
            case Element.Fire:
                return Fire;

            case Element.Aqua:
                return Aqua;

            case Element.Shock:
                return Shock;
                
            case Element.Order:
                return Order;

            case Element.Chaos:
                return Chaos;
                
            default:
                return 0;

        }
    }

    public void Add(Element _ele, float _amount)
    {
        switch (_ele)
        {
            case Element.Fire:
                Fire += _amount;
                //Frost -= _amount;
                break;
            case Element.Aqua:
                Aqua += _amount;
                //Shock -= _amount;
                break;
            case Element.Shock:
                Shock += _amount;
                //Fire -= _amount;
                break;
            case Element.Order:
                Order += _amount;
                break;
            case Element.Chaos:
                Chaos += _amount;
                break;
            default:
                break;

        }
    }

    public void Zero()
    {
        Shock = 0;
        Fire = 0;
        Aqua = 0;
        Order = 0;
        Chaos = 0;
    }

}

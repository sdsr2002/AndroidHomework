using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public int Strength => _Strength.Value;
    public int Agility => _Agility.Value;
    public int Constitution => _Constitution.Value;

    public Stat _Strength     = new Stat("Strength"     , 1, Stat.Type.Strength     );
    public Stat _Agility      = new Stat("Agility"      , 1, Stat.Type.Agility      );
    public Stat _Constitution = new Stat("Constitution" , 1, Stat.Type.Constitution );
}

[System.Serializable]
public class Stat
{
    public Stat(string name, int baseValue, Type type)
    {
        StatName = name;
        _baseValue = baseValue;
        this.type = type;
    }

    public string StatName                  = "Not Assigned";
    [SerializeField] private int _baseValue = 1;
    [SerializeField] public Type type       = Type.notAssigned;

    private int _value = 0;
    private float _mutiplyValue = 1f;

    private List<Modifier> _Modifiers = new List<Modifier>();

    public int Value
    {
        get {
            if (_isDirty) RecalculateStat();
            return _value;
        }
    }

    private bool _isDirty = true;

    private void RecalculateStat()
    {
        _value = _baseValue;
        _mutiplyValue = 0f;
        _Modifiers.Sort((c,x) => c.type.CompareTo(x.type));
        foreach(Modifier mod in _Modifiers)
        {
            if (mod.type != Modifier.Type.AddMultiple)
                continue;
            else
            {
                _mutiplyValue += mod.Value;
            }
        }
        foreach (Modifier mod in _Modifiers)
        {
            if (mod.type != Modifier.Type.AddMultiple)
                mod.Apply(ref _value);
        }

        _value += Mathf.RoundToInt(_baseValue * _mutiplyValue);
        _isDirty = false;

        return;
    }

    public void AddModifier(Modifier mod)
    {
        _Modifiers.Add(mod);
        _isDirty = true;
    }

    public void RemoveModifier(Modifier mod)
    {
        _Modifiers.Remove(mod);
        _isDirty = true;
    }

    [System.Serializable]
    public class Modifier
    {
        public float Value;
        public Type type;
        public Stat.Type statType;

        public void Apply(ref int value)
        {
            switch (type)
            {
                case Type.Additive:
                    Additive(ref value);
                    break;
                case Type.Multiple:
                    Multiply(ref value);
                    break;
                case Type.AddMultiple:
                    Debug.LogError("F Something went wrong here");
                    break;
            }

        }

        private void Additive(ref int value)
        {
            value += Mathf.RoundToInt(Value);
        }

        private void Multiply(ref int value)
        {
            float newValue = value * (1f + Value);

            value = Mathf.RoundToInt(newValue);
        }
        public enum Type {
            notAssigned,
            AddMultiple,
            Additive,
            Multiple
        }
    }
    public enum Type
    {
        notAssigned,
        Strength,
        Agility,
        Constitution
    }
}

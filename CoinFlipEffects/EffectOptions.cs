using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Exiled.API.Enums;
using UnityEngine;

namespace JacobsToolbox.CoinFlipEffects;

/// <summary>
/// Typed readers for per-effect options registered by effect files via
/// <c>public static Dictionary&lt;string, object&gt; DefaultOptions</c> and
/// overridden in config under <c>CoinEffectOptions: { EffectName: { Option: value } }</c>.
/// Values coming from the YAML config arrive as strings/numbers/lists/maps and are converted leniently.
/// </summary>
public static class EffectOptions
{
    private static object Raw(string effect, string key)
    {
        var all = Plugin.Instance?.Config?.CoinEffectOptions;
        if (all != null && all.TryGetValue(effect, out var opts) && opts != null && opts.TryGetValue(key, out var value))
            return value;
        return null;
    }

    public static int GetInt(string effect, string key, int def)
    {
        object v = Raw(effect, key);
        if (v == null)
            return def;
        if (v is int i)
            return i;
        if (v is long l)
            return (int)l;
        if (v is double d)
            return (int)d;
        if (v is float f)
            return (int)f;
        if (v is string s && int.TryParse(s.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsed))
            return parsed;
        return def;
    }

    public static float GetFloat(string effect, string key, float def)
    {
        object v = Raw(effect, key);
        if (v == null)
            return def;
        if (v is float f)
            return f;
        if (v is double d)
            return (float)d;
        if (v is long l)
            return l;
        if (v is int i)
            return i;
        if (v is string s && float.TryParse(s.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float parsed))
            return parsed;
        return def;
    }

    public static double GetDouble(string effect, string key, double def)
    {
        object v = Raw(effect, key);
        if (v == null)
            return def;
        if (v is double d)
            return d;
        if (v is float f)
            return f;
        if (v is long l)
            return l;
        if (v is int i)
            return i;
        if (v is string s && double.TryParse(s.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double parsed))
            return parsed;
        return def;
    }

    public static bool GetBool(string effect, string key, bool def)
    {
        object v = Raw(effect, key);
        if (v == null)
            return def;
        if (v is bool b)
            return b;
        if (v is string s && bool.TryParse(s.Trim(), out bool parsed))
            return parsed;
        if (v is long l)
            return l != 0;
        if (v is int i)
            return i != 0;
        return def;
    }

    public static HashSet<ItemType> GetItemSet(string effect, string key, HashSet<ItemType> def)
    {
        object v = Raw(effect, key);
        if (v == null)
            return def;
        var result = new HashSet<ItemType>();
        if (v is IEnumerable enumerable && !(v is string))
        {
            foreach (object item in enumerable)
            {
                if (item is ItemType t)
                {
                    result.Add(t);
                }
                else if (item is string s && Enum.TryParse(s.Trim(), true, out ItemType parsed))
                {
                    result.Add(parsed);
                }
                else if (TryToLong(item, out long n) && Enum.IsDefined(typeof(ItemType), (int)n))
                {
                    result.Add((ItemType)n);
                }
            }
            return result;
        }
        if (v is string single && Enum.TryParse(single.Trim(), true, out ItemType one))
            result.Add(one);
        return result.Count > 0 ? result : def;
    }

    public static HashSet<RoomType> GetRoomSet(string effect, string key, HashSet<RoomType> def)
    {
        object v = Raw(effect, key);
        if (v == null)
            return def;
        var result = new HashSet<RoomType>();
        if (v is IEnumerable enumerable && !(v is string))
        {
            foreach (object item in enumerable)
            {
                if (item is RoomType t)
                {
                    result.Add(t);
                }
                else if (item is string s && Enum.TryParse(s.Trim(), true, out RoomType parsed))
                {
                    result.Add(parsed);
                }
            }
            return result;
        }
        if (v is string single && Enum.TryParse(single.Trim(), true, out RoomType one))
            result.Add(one);
        return result.Count > 0 ? result : def;
    }

    public static Vector3 GetVector3(string effect, string key, Vector3 def)
    {
        object v = Raw(effect, key);
        if (v == null)
            return def;
        if (v is Vector3 vec)
            return vec;
        if (v is IDictionary dict)
        {
            float x = GetComponent(dict, "x", def.x);
            float y = GetComponent(dict, "y", def.y);
            float z = GetComponent(dict, "z", def.z);
            return new Vector3(x, y, z);
        }
        if (v is string s)
        {
            string[] parts = s.Split(new[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 3
                && float.TryParse(parts[0].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float x)
                && float.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float y)
                && float.TryParse(parts[2].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float z))
                return new Vector3(x, y, z);
        }
        return def;
    }

    private static float GetComponent(IDictionary dict, string axis, float def)
    {
        foreach (object dictKey in dict.Keys)
        {
            if (string.Equals(dictKey?.ToString(), axis, StringComparison.OrdinalIgnoreCase))
                return ToFloat(dict[dictKey], def);
        }
        return def;
    }

    private static float ToFloat(object v, float def)
    {
        if (v is float f)
            return f;
        if (v is double d)
            return (float)d;
        if (v is long l)
            return l;
        if (v is int i)
            return i;
        if (v is string s && float.TryParse(s.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float parsed))
            return parsed;
        return def;
    }

    private static bool TryToLong(object v, out long result)
    {
        if (v is long l)
        {
            result = l;
            return true;
        }
        if (v is int i)
        {
            result = i;
            return true;
        }
        if (v is string s && long.TryParse(s.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out long parsed))
        {
            result = parsed;
            return true;
        }
        result = 0;
        return false;
    }
}

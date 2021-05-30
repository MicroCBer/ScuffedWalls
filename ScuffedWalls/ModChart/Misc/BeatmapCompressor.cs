﻿using ModChart.Wall;
using ScuffedWalls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModChart
{
    static class BeatmapCompressor
    {
        public static BeatMap SimplifyAllPointDefinitions(this BeatMap Map)
        {

            //simplify custom event point definitions
            if (Map._customData != null && Map._customData["_customEvents"] != null && Map._customData.at<IEnumerable<object>>("_customEvents").Count() > 0)
            {
                
                Map._customData["_customEvents"] = Map._customData.at<IEnumerable<TreeDictionary>>("_customEvents").Select(mapobj =>
                {
                    try
                    {
                        if (mapobj["_data"] != null) mapobj["_data"] = mapobj.at("_data").SimplifyAnimationPointDefinitions();
                        return mapobj;
                    }
                    catch (Exception e)
                    {
                        ScuffedLogger.Error.Log($"Error on _customEvent at beat {mapobj["_time"]}");
                        throw e;
                    }
                }).ToArray();
            }



            //simplify wall point definitions
            if (Map._obstacles != null && Map._obstacles.Length > 0)
            {
                Map._obstacles = Map._obstacles.Select(mapobj =>
                {
                    try
                    {
                        if (mapobj._customData != null && mapobj._customData["_animation"] != null) mapobj._customData["_animation"] = mapobj._customData.at("_animation").SimplifyAnimationPointDefinitions();
                        return mapobj;
                    }
                    catch(Exception e)
                    {
                        ScuffedLogger.Error.Log($"Error on _obstacle at beat {mapobj._time}");
                        throw e;
                    }
                }).ToArray();
            }

            //simplify note point definitions
            if (Map._notes != null && Map._notes.Length > 0)
            {
                Map._notes = Map._notes.Select(mapobj =>
                {
                    try
                    {
                        if (mapobj._customData != null && mapobj._customData["_animation"] != null) mapobj._customData["_animation"] = mapobj._customData.at("_animation").SimplifyAnimationPointDefinitions();
                        return mapobj;
                    }
                    catch (Exception e)
                    {
                        ScuffedLogger.Error.Log($"Error on _note at beat {mapobj._time}");
                        throw e;
                    }
                }).ToArray();
            }

            return Map;
        }
        public static IDictionary<string, int> AnimationSigFigs = new Dictionary<string,int>()
        {
            ["_color"] = 3, 
            ["_dissolve"] = 1, 
            ["_dissolveArrow"] = 1, 
            ["_definitePosition"] = 3,
            ["_position"] = 3,
            ["_scale"] = 3,
            ["_rotation"] = 3,
            ["_localPosition"] = 3,
            ["_localRotation"] = 3
        };
        /*
        public static int GetImportantValuesFromAnimationProperty(this PropertyInfo property)
        {
            string name = property.Name;
            if (name == "_color") return 4;
            else if (name == "_dissolve" || name == "_dissolveArrow") return 1;
            else return 3;
        }
        */
        public static TreeDictionary SimplifyAnimationPointDefinitions(this TreeDictionary _animation)
        {
            TreeDictionary newAnimation = new TreeDictionary();
            foreach (KeyValuePair<string, object> item in _animation.Where(prop => AnimationSigFigs.Keys.Any(animationprop => animationprop == prop.Key)))
            {
                if (item.Value is IEnumerable<object> array && AnimationSigFigs.TryGetValue(item.Key, out int sigfig))
                {
                    newAnimation[item.Key] = array.SimplifyPointDefinition(sigfig);
                }
                else newAnimation[item.Key] = item.Value;
            }
            return _animation;
        }
        public static bool EqualsArray(this object[] array1, object[] array2)
        {
            if (array2.Length != array1.Length) return false;

            for (int length = 0; length < array1.Length; length++)
            {
                if (array1[length].ToFloat() != array2[length].ToFloat()) return false;
            }
            return true;
        }
        public static IEnumerable<IEnumerable<object>> SimplifyPointDefinition(this IEnumerable<object> points, int importantvalues)
        {
            if (points.Any(obj => !(obj is IEnumerable<object>))) throw new FormatException("A point definition is missing one or more of the SubArrays");

            var _pointDefinition = points.Select(p => ((IEnumerable<object>)p).ToArray()).ToArray();

            List<IEnumerable<object>> NewPoints = new List<IEnumerable<object>>();
            for (int i = 0; i < _pointDefinition.Count(); i++)
            {
                if (i != 0 &&
                i < _pointDefinition.Count() - 1 &&
                (_pointDefinition[i].Slice(0, importantvalues).ToArray().EqualsArray(_pointDefinition[i - 1].Slice(0, importantvalues).ToArray())) &&
                (_pointDefinition[i].Slice(0, importantvalues).ToArray().EqualsArray(_pointDefinition[i + 1].Slice(0, importantvalues).ToArray()))) continue;


                NewPoints.Add(_pointDefinition.ElementAt(i));
            }
            if (NewPoints.Last().ElementAt(importantvalues).ToFloat() < 1f)
            {
                var lastPoint = NewPoints.Last().CloneArray().ToArray();
                lastPoint[importantvalues] = 1;
                NewPoints.Add(lastPoint);
            }
            else if (NewPoints.Last().ElementAt(importantvalues).ToFloat() > 1f)
            {
                ScuffedLogger.Warning.Log($"[Warning] Noodle Extensions point definitions don't end with values higher than 1");

            }
            return NewPoints.ToArray();
        }
    }
}

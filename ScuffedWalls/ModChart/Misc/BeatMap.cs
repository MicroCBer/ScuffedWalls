﻿using System;
using System.Text.Json.Serialization;

namespace ModChart
{
    public interface ICustomDataMapObject : ITimeable, ICloneable
    {
        [JsonConverter(typeof(TreeDictionaryJsonConverter))]
        public TreeDictionary _customData { get; set; }
    }
    public interface ITimeable
    {
        public float GetTime();
        public object _time { get; set; }
    }
    public class BeatMap
    {
        public void Prune()
        {
            _customData.DeleteNullValues();
            foreach (var mapobj in _events) if (mapobj._customData != null) mapobj._customData.DeleteNullValues();
            foreach (var mapobj in _notes) if (mapobj._customData != null) mapobj._customData.DeleteNullValues();
            foreach (var mapobj in _obstacles) if(mapobj._customData != null) mapobj._customData.DeleteNullValues();
        }
        public static BeatMap Empty => new BeatMap
        {
            _version = "2.2.0",
            _events = new Event[] { },
            _notes = new Note[] { },
            _obstacles = new Obstacle[] { },
            _waypoints = new object[] { },
            _customData = new TreeDictionary()
        };


        public string _version { get; set; }
        [JsonConverter(typeof(TreeDictionaryJsonConverter))]
        public TreeDictionary _customData { get; set; }
        public Event[] _events { get; set; }
        public Note[] _notes { get; set; }
        public Obstacle[] _obstacles { get; set; }
        public object[] _waypoints { get; set; }


        public class Event : ICustomDataMapObject, ICloneable
        {
            public float GetTime() => float.Parse(_time.ToString());
            public object _time { get; set; }
            public object _type { get; set; }
            public object _value { get; set; }
            [JsonConverter(typeof(TreeDictionaryJsonConverter))]
            public TreeDictionary _customData { get; set; }

            public object Clone()
            {
                return new Event()
                {
                    _time = _time,
                    _type = _type,
                    _value = _value,
                    _customData = (TreeDictionary)_customData.Clone()
                };
            }
        }
        public class Note : ICustomDataMapObject, ICloneable
        {
            public float GetTime() => float.Parse(_time.ToString());
            public object _time { get; set; }
            public object _lineIndex { get; set; }
            public object _lineLayer { get; set; }
            public object _type { get; set; }
            public object _cutDirection { get; set; }
            [JsonConverter(typeof(TreeDictionaryJsonConverter))]
            public TreeDictionary _customData { get; set; }

            public object Clone()
            {
                return new Note()
                {
                    _time = _time,
                    _lineIndex = _lineIndex,
                    _type = _type,
                    _cutDirection = _cutDirection,
                    _lineLayer = _lineLayer,
                    _customData = (TreeDictionary)_customData.Clone()
                };
            }
        }
        public class Obstacle : ICustomDataMapObject, ICloneable
        {
            public float GetTime() => float.Parse(_time.ToString());
            public object _time { get; set; }
            public object _lineIndex { get; set; }
            public object _type { get; set; }
            public object _duration { get; set; }
            public object _width { get; set; }
            [JsonConverter(typeof(TreeDictionaryJsonConverter))]
            public TreeDictionary _customData { get; set; }

            public object Clone()
            {
                return new Obstacle()
                {
                    _time = _time,
                    _lineIndex = _lineIndex,
                    _type = _type,
                    _duration = _duration,
                    _width = _width,
                    _customData = (TreeDictionary)_customData.Clone()
                };
            }
        }
        /*
        public class CustomData
        {
            public Environment[] _environment { get; set; }
            public class Environment
            {
                public object _id { get; set; }
                public object _lookupMethod { get; set; }
                public object _duplicate { get; set; }
                public object _active { get; set; }
                public object[] _scale { get; set; }
                public object[] _position { get; set; }
                public object[] _localPosition { get; set; }
                public object[] _rotation { get; set; }
                public object[] _localRotation { get; set; }
                public object _track { get; set; }
            }
            public object _time { get; set; } //float

            //noodle
            public object _track { get; set; } //string
            public object _cutDirection { get; set; } //int
            public object _noteJumpStartBeatOffset { get; set; } //float
            public object _noteJumpMovementSpeed { get; set; } //float
            public object _interactable { get; set; } //bool
            public object[] _position { get; set; }
            public object[] _localRotation { get; set; }
            public dynamic _rotation { get; set; }
            public object[] _scale { get; set; }
            public object[] _flip { get; set; }
            public object _fake { get; set; } //bool
            public object _disableNoteGravity { get; set; } //bool
            public object _disableNoteLook { get; set; } //bool
            public Animation _animation { get; set; }

            public class Animation
            {
                public dynamic _position { get; set; }
                public dynamic _scale { get; set; }
                public dynamic _rotation { get; set; }
                public dynamic _localRotation { get; set; }
                public dynamic _dissolve { get; set; }
                public dynamic _dissolveArrow { get; set; }
                public dynamic _definitePosition { get; set; }
                public dynamic _color { get; set; }
                public dynamic _time { get; set; }
                public dynamic _interactable { get; set; }
            }

            public CustomEvent[] _customEvents { get; set; }

            public class CustomEvent : ITimeable
            {
                public object _time { get; set; } //float
                public object _type { get; set; } //string
                public Data _data { get; set; }
                public class Data
                {
                    public object _track { get; set; } //string
                    public object _duration { get; set; }  //float
                    public object _easing { get; set; } //string
                    public object[] _childrenTracks { get; set; }
                    public object _parentTrack { get; set; }
                    public dynamic _position { get; set; }
                    public dynamic _scale { get; set; }
                    public dynamic _rotation { get; set; }
                    public dynamic _localRotation { get; set; }
                    public dynamic _dissolve { get; set; }
                    public dynamic _dissolveArrow { get; set; }
                    public dynamic _definitePosition { get; set; }
                    public dynamic _color { get; set; }
                    public dynamic _time { get; set; }
                    public dynamic _interactable { get; set; }
                    public dynamic _localPosition { get; set; }
                    public dynamic _localScale { get; set; }

                }


                /// <summary>
                /// wip mod dont touch
                /// </summary>
                /*
                public Shader[] _shaders { get; set; }
                public class Shader
                {
                    public object _id { get; set; }
                    public object _ref { get; set; }
                    public object _clearAfterDone { get; set; }
                    public object _clearID { get; set; }
                    public Prop[] _props { get; set; }
                    public class Prop
                    {
                        public object _prop { get; set; }
                        public object _duration { get; set; }
                        public object _easing { get; set; }
                        public dynamic _value { get; set; }

                    }
                }
                
            }

            //chroma
            public object _propID { get; set; } // int
            public object _lightID { get; set; } // int
            public object _duration { get; set; } //float
            public object[] _color { get; set; }
            public object _lockPosition { get; set; } //bool
            public object _preciseSpeed { get; set; } //float
            public object _direction { get; set; } //int
            public object _nameFilter { get; set; } //string
            public object _reset { get; set; } //bool
            public object _step { get; set; } //float
            public object _prop { get; set; } //float
            public object _speed { get; set; } //float
            public object _disableSpawnEffect { get; set; } //bool
            public object _counterSpin { get; set; } //bool
            public object _stepMult { get; set; } //float
            public object _propMult { get; set; } //float
            public object _speedMult { get; set; } //float
            public LightGradient _lightGradient { get; set; }
            public class LightGradient
            {
                public object _duration { get; set; }
                public object _startColor { get; set; }
                public object _endColor { get; set; }
                public object _easing { get; set; }
            }


            //extra custom data
            public BPMChanges[] _BPMChanges { get; set; }

            public class BPMChanges
            {
                public object _time { get; set; }
                public object _BPM { get; set; }
                public object _beatsPerBar { get; set; }
                public object _metronomeOffset { get; set; }
            }

            public PointDefinition[] _pointDefinitions { get; set; }
            public class PointDefinition
            {
                public object _name { get; set; }
                public dynamic _points { get; set; }
            }
            public Bookmark[] _bookmarks { get; set; }

            [Serializable]
            public class Bookmark : ITimeable
            {
                public object _time { get; set; } //float
                public object _name { get; set; } //string
            }

        

        }
        */
    }

}
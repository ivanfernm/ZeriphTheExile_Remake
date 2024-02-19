using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class saveData
    {
        public Vector3 playerPosition;
        public string sceneName;
        public SerializableDictionary<string, bool> sceneData;
    }

    public class SerializableDictionary<Tkey, TValue> : Dictionary<Tkey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<Tkey> _keys = new List<Tkey>();
        [SerializeField] private List<TValue> _values = new List<TValue>();

        //Explain the code bellow
        public void OnBeforeSerialize()
        {
            //clean the existing values;
            _keys.Clear();
            _values.Clear();
            //add the new values
            foreach (var pair in this)
            {
                _keys.Add(pair.Key);
                _values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            
            if(_keys.Count != _values.Count) throw new  Exception("Theres different amount of values and keys");

            for (int i = 0; i < _keys.Count; i++)
            {
                this.Add(_keys[i], _values[i]);
            }
        }
    }
}
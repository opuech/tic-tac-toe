using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TicTacToe.Domain.Core
{
    [Serializable]
    public abstract class Immutable<T> : IEquatable<T>
            where T : Immutable<T>
    {
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            T other = obj as T;

            return Equals(other);
        }



        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            int startValue = 17;
            int multiplier = 59;
            int hashCode = startValue;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }

            return hashCode;
        }



        public virtual bool Equals(T other)
        {
            if (other == null)
                return false;

            FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                object value1 = field.GetValue(other);
                object value2 = field.GetValue(this);

                if (!CompareObject(value1, value2)) return false;
            }

            return true;
        }

        private bool CompareObject(object value1, object value2)
        {
            if (value1 == null)
            {
                if (value2 != null)
                    return false;
            }
            else
            {
                if (value1 is IEnumerable && value2 is IEnumerable)
                {
                    if (!CompareEnumeration(value1 as IEnumerable, value2 as IEnumerable)) return false;
                }
                else
                {
                    if (!value1.Equals(value2)) return false;
                }
            }

            return true;
        }

        private bool CompareEnumeration(IEnumerable col1, IEnumerable col2)
        {
            IEnumerator enumerator1 = col1.GetEnumerator();
            IEnumerator enumerator2 = col2.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                object value1 = enumerator1.Current;
                object value2 = enumerator2.Current;

                if(!CompareObject(value1, value2)) return false;
            }
            if (enumerator1.MoveNext() || enumerator2.MoveNext()) return false;

            return true;
        }
        
        private IEnumerable<FieldInfo> GetFields()
        {
            Type t = GetType();

            List<FieldInfo> fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

                t = t.BaseType;
            }

            return fields;
        }



        public static bool operator ==(Immutable<T> x, Immutable<T> y)
        {
            return x.Equals(y);
        }



        public static bool operator !=(Immutable<T> x, Immutable<T> y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <returns>The copied object.</returns>
        private T Clone()
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(this, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <returns>The copied object.</returns>
        protected TToClone Clone<TToClone>(TToClone objectToClone)
        {
            if (!typeof(TToClone).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(objectToClone, null))
            {
                return default(TToClone);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, objectToClone);
                stream.Seek(0, SeekOrigin.Begin);
                return (TToClone)formatter.Deserialize(stream);
            }
        }
        protected T With(Action<T> action)
        {
            var newObject = this.Clone();
            action(newObject);
            return newObject;
        }
    }
}
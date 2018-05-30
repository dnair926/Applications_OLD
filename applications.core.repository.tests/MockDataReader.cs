using System;
using System.Collections.Generic;
using System.Data;

namespace Applications.Core.Repository.Tests
{
    public class MockDataReader : IDataReader
    {
        private string[] fields = new string[] { "FirstName", "LastName", "MiddleInitial", "DateOfBirth" };
        private List<List<KeyValuePair<string, object>>> entities = null;
        private int index = -1;
        private bool closed = false;
        public bool Closed
        {
            get
            {
                return closed;
            }
        }

        public MockDataReader()
        {
            InitializeFields();
        }

        private void InitializeFields()
        {
            entities = new List<List<KeyValuePair<string, object>>>
            {
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>(fields[0], "Deleep"),
                    new KeyValuePair<string, object>(fields[1], "Nair"),
                    new KeyValuePair<string, object>(fields[2], "K."),
                },
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>(fields[0], "Jane"),
                    new KeyValuePair<string, object>(fields[1], "Doe"),
                    new KeyValuePair<string, object>(fields[2], DBNull.Value),
                }
            };
        }

        public object this[int i] => throw new NotImplementedException();

        public object this[string name] => throw new NotImplementedException();

        public int Depth => throw new NotImplementedException();

        public bool IsClosed => throw new NotImplementedException();

        public int RecordsAffected => throw new NotImplementedException();

        public int FieldCount => fields.Length;

        public void Close()
        {
            closed = true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            return fields[i];
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            if (entities.Count < index + 1 ||
                entities[index].Count < i + 1)
            {
                return null;
            }
            return entities[index][i].Value;
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            index += 1;

            return index <= entities.Count - 1;
        }
    }
}

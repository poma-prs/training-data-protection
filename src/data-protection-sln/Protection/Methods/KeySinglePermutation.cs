using System;
using System.Linq;

namespace Protection.Methods
{
    public class TableKey
    {
        public TableKey(int rowCount, int columnCount)
        {
            if (rowCount < 1 || columnCount < 1)
                throw new ArgumentException();
            RowCount = rowCount;
            ColumnCount = columnCount;
        }

        public int RowCount { get; }
        public int ColumnCount { get; }
    }
    
    public class KeySinglePermutation : IEncryptionMethod<KeySinglePermutation.Input, KeySinglePermutation.Output>
    {
        public class Input
        {
            public Input(string value, Key key)
            {
                if (key == null || value == null)
                    throw new ArgumentException();
                Value = value;
                Key = key;
            }

            public string Value { get; }
            public Key Key { get; }
        }

        public class Key : TableKey
        {
            public Key(int rowCount, string keyWord)
                : base(rowCount, keyWord.Length)
            {
                if (string.IsNullOrWhiteSpace(keyWord))
                    throw new ArgumentException();
                KeyWord = keyWord.Trim();
            }

            public string KeyWord { get; }
        }

        public class Output
        {
            public Output(string value, Key key)
            {
                if (value == null)
                    throw new ArgumentException();
                Value = value;
                Key = key;
            }

            public string Value { get; }
            public Key Key { get; }
        }

        public Output Encrypt(Input data)
        {
            var encryptedValue = data.Value
                .Select((x, i) => new
                {
                    X = i%data.Key.RowCount,
                    Y = Tuple.Create(data.Key.KeyWord[i/data.Key.RowCount], i/data.Key.RowCount),
                    Value = x
                })
                .OrderBy(item => Tuple.Create(item.X, item.Y))
                .Aggregate("", (str, item) => str + item.Value);
            return new Output(encryptedValue, data.Key);
        }

        public Input Decrypt(Output data)
        {
            var sortKey = data.Key.KeyWord
                .Select((x, i) => new {Index = i, Value = x})
                .OrderBy(item => Tuple.Create(item.Value, item.Index))
                .ToList();

            var decryptedValue = data.Value
                .Select((x, i) => new {X = i/data.Key.ColumnCount, Y = i%data.Key.ColumnCount, Value = x})
                .OrderBy(item => Tuple.Create(sortKey[item.Y].Index, item.X))
                .Aggregate("", (str, item) => str + item.Value);

            return new Input(decryptedValue, data.Key);
        }
    }
}
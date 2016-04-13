namespace Web.User.Models
{
    public class KeySinglePermutationModel
    {
        public class InputModel
        {
            public KeyModel Key { get; set; }
            public string Value { get; set; }
        }

        public class OutputModel
        {
            public KeyModel Key { get; set; }
            public string Value { get; set; }
        }

        public class KeyModel
        {
            public int RowCount { get; set; }
            public string KeyWord { get; set; }
        }

        public InputModel Input { get; set; }
        public OutputModel Output { get; set; }
    }
}
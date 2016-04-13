using NUnit.Framework;
using Protection.Methods;

namespace Tests
{
    [TestFixture]
    public class KeySinglePermutationTests
    {
        private KeySinglePermutation Method { get; set; }

        [SetUp]
        public void SetUp()
        {
            Method = new KeySinglePermutation();
        }

        [Test]
        public void Encrypt1_Test()
        {
            var key = new KeySinglePermutation.Key(3, "ПАМИР");
            var input = new KeySinglePermutation.Input("ВРАГБУДЕТРАЗБИТ", key);
            var output = Method.Encrypt(input);
            Assert.AreEqual("ГРДВББАЕРИУЗТАТ", output.Value);
        }

        [Test]
        public void Encrypt2_Test()
        {
            var key = new KeySinglePermutation.Key(4, "СКАНЕР");
            var input = new KeySinglePermutation.Input("СИСТЕМНЫЙ ПАРОЛЬ ИЗМЕНЕН", key);
            var output = Method.Encrypt(input);
            Assert.AreEqual("Й ЕРЕС ИМОНИПЗНЛЕСАМЫЬНТ", output.Value);
        }

        [Test]
        public void Decrypt1_Test()
        {
            var key = new KeySinglePermutation.Key(3, "ПАМИР");
            var output = new KeySinglePermutation.Output("ГРДВББАЕРИУЗТАТ", key);
            var input = Method.Decrypt(output);
            Assert.AreEqual("ВРАГБУДЕТРАЗБИТ", input.Value);
        }

        [Test]
        public void Decrypt2_Test()
        {
            var key = new KeySinglePermutation.Key(3, "СКАНЕР");
            var output = new KeySinglePermutation.Output("Й ЕРЕС ИМОНИПЗНЛЕСАМЫЬНТ", key);
            var input = Method.Decrypt(output);
            Assert.AreEqual("СИСТЕМНЫЙ ПАРОЛЬ ИЗМЕНЕН", input.Value);
        }
    }
}
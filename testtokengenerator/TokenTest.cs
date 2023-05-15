using common.Model;
using NUnit.Framework;
using System;

namespace TestCardRegister
{
    public class TokenTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestShouldGenerateTokenMod0()
        {
            long cardNumber = 1111111111117890;
            int cvv = 160;
            long tokenGenerated = Token.GenerateToken(cardNumber, cvv);
            Assert.AreEqual(7890, tokenGenerated);
        }

        [Test]
        public void TestShouldGenerateTokenMod1()
        {
            long cardNumber = 1111111111117890;
            int cvv = 153;
            long tokenGenerated = Token.GenerateToken(cardNumber, cvv);
            Assert.AreEqual(0789, tokenGenerated);
        }

        [Test]
        public void TestShouldGenerateTokenMod2()
        {
            long cardNumber = 1111111111117890;
            int cvv = 154;
            long tokenGenerated = Token.GenerateToken(cardNumber, cvv);
            Assert.AreEqual(9078, tokenGenerated);
        }

        [Test]
        public void TestShouldGenerateTokenMod3()
        {
            long cardNumber = 1111111111117890;
            int cvv = 151;
            long tokenGenerated = Token.GenerateToken(cardNumber, cvv);
            Assert.AreEqual(8907, tokenGenerated);
        }

        [Test]
        [TestCase(0)]
        [TestCase(123)]
        public void TestShouldGenerateTokenInvalidCardNumber(long cardNumber)
        {
            int cvv = 160;
            Assert.Throws<Exception>(() => Token.GenerateToken(cardNumber, cvv));
        }

        [Test]
        [TestCase(0)]
        public void TestShouldGenerateTokenInvalidCVV(int cvv)
        {
            long cardNumber = 1111111111117890;
            long tokenGenerated = Token.GenerateToken(cardNumber, cvv);
            Assert.AreEqual(7890, tokenGenerated);
        }

    }
}
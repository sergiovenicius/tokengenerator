using common.Mapper;
using NUnit.Framework;
using tokengenerator.Database;
using tokengenerator.Model;

namespace TestCardRegister
{
    public class MapTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestShouldMapDBCardToResponse()
        {
            IMapper<CardResponse, DBCard> mapper = new MapperDBCardToCardResponse();

            DBCard input = new DBCard
            {
                CardNumber = 1234567812345678,
                CustomerId = 1,
                CVV = 123,
                RegistrationDate = System.DateTime.UtcNow,
                CardId = 1,
                Token = 1
            };

            var cardresponse = mapper.Map(input);

            Assert.AreEqual(cardresponse.Token, input.Token);
            Assert.AreEqual(cardresponse.RegistrationDate, input.RegistrationDate);
            Assert.AreEqual(cardresponse.CardId, input.CardId);
        }

        [Test]
        public void TestShouldMapCardInputToDBCard()
        {
            IMapper<DBCard, CardInput> mapper = new MapperCardInputToDBCard();

            CardInput input = new CardInput
            {
                CardNumber = 1234567812345678,
                CustomerId = 1,
                CVV = 123
            };

            var dbcard = mapper.Map(input);

            Assert.AreEqual(input.CardNumber, dbcard.CardNumber);
            Assert.AreEqual(input.CustomerId, dbcard.CustomerId);
            Assert.AreEqual(input.CVV, dbcard.CVV);
        }
    }
}
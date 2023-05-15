using common.Exceptions;
using common.Mapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using tokengenerator.Database;
using tokengenerator.Model;
using tokengenerator.Repository;
using tokengenerator.Service;

namespace TestCardRegister
{
    public class CardServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCardServiceShouldSaveCard()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                CardRepository repo = new CardRepository(dbcontext);

                CardService svc = new CardService(repo, new MapperCardInputToDBCard(), new MapperDBCardToCardResponse());

                var input = new CardInput()
                {
                    CardNumber = 1234123412341234,
                    CustomerId = 1,
                    CVV = 123
                };

                var savedCard = svc.Save(input);

                Assert.AreEqual(savedCard.CardId, 1);
            }
        }

        [Test]
        public void TestCardServiceShouldListCards()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                CardRepository repo = new CardRepository(dbcontext);

                CardService svc = new CardService(repo, new MapperCardInputToDBCard(), new MapperDBCardToCardResponse());

                var input1 = new CardInput()
                {
                    CardNumber = 1234123412341234,
                    CustomerId = 1,
                    CVV = 123
                };

                var saved1 = svc.Save(input1);

                var input2 = new CardInput()
                {
                    CardNumber = 1234123412341239,
                    CustomerId = 2,
                    CVV = 111
                };

                var saved2 = svc.Save(input2);

                var cardList = svc.List().ToList();

                Assert.AreEqual(cardList.Count(), 2);
                Assert.AreEqual(cardList[0].CardId, saved1.CardId);
                Assert.AreEqual(cardList[1].CardId, saved2.CardId);
            }
        }

        [Test]
        public void TestCardServiceShouldGetCardById()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                CardRepository repo = new CardRepository(dbcontext);

                CardService svc = new CardService(repo, new MapperCardInputToDBCard(), new MapperDBCardToCardResponse());

                var input = new CardInput()
                {
                    CardNumber = 1234123412341234,
                    CustomerId = 1,
                    CVV = 123
                };

                svc.Save(input);

                var card = svc.Get(1);

                Assert.AreEqual(card.CardId, 1);
            }
        }

        [Test]
        public void TestCardServiceShouldGetCardByIdNotFound()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                CardRepository repo = new CardRepository(dbcontext);

                CardService svc = new CardService(repo, new MapperCardInputToDBCard(), new MapperDBCardToCardResponse());

                Assert.Throws<NotFoundException>(() => svc.Get(1));
            }
        }

        [Test]
        [TestCase(123456)]
        public void TestCardServiceShouldSaveCardInvalidCVV(int cvv)
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                CardRepository repo = new CardRepository(dbcontext);

                CardService svc = new CardService(repo, new MapperCardInputToDBCard(), new MapperDBCardToCardResponse());

                var input = new CardInput()
                {
                    CardNumber = 1234123412341234,
                    CustomerId = 1,
                    CVV = cvv
                };

                Assert.Throws<ValidationException>(() => svc.Save(input));
            }
        }

        [Test]
        [TestCase(12341234123412341)]
        public void TestCardServiceShouldSaveCardInvalidCardNumber(long cardNumber)
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                CardRepository repo = new CardRepository(dbcontext);

                CardService svc = new CardService(repo, new MapperCardInputToDBCard(), new MapperDBCardToCardResponse());

                var input = new CardInput()
                {
                    CardNumber = cardNumber,
                    CustomerId = 1,
                    CVV = 123
                };

                Assert.Throws<ValidationException>(() => svc.Save(input));
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(123)]
        public void TestCardServiceShouldSaveCardInvalidCardNumberLessThan4Digits(long cardNumber)
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                CardRepository repo = new CardRepository(dbcontext);

                CardService svc = new CardService(repo, new MapperCardInputToDBCard(), new MapperDBCardToCardResponse());

                var input = new CardInput()
                {
                    CardNumber = cardNumber,
                    CustomerId = 1,
                    CVV = 123
                };

                Assert.Throws<Exception>(() => svc.Save(input));
            }
        }
    }
}
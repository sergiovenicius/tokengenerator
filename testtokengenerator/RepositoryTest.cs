using common.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using tokengenerator.Database;
using tokengenerator.Repository;

namespace TestCardRegister
{
    public class RepositoryTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestRepositoryShouldSaveCard()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                var dbCardRepo = new CardRepository(dbcontext);

                var savedCard = dbCardRepo.Save(new DBCard() { CardNumber = 1234567812345678, CustomerId = 1, CVV = 123, Token = 1 });

                Assert.AreEqual(1, savedCard.CardId);
                Assert.AreEqual(1234567812345678, savedCard.CardNumber);
                Assert.AreEqual(1, savedCard.CustomerId);
                Assert.AreEqual(123, savedCard.CVV);
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(123)]
        [TestCase(12341234123412341)]
        public void TestRepositoryShouldSaveCardInvalidCardNumber(long cardNumber)
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                var dbCardRepo = new CardRepository(dbcontext);

                Assert.Throws<ValidationException>(() => dbCardRepo.Save(new DBCard() { CardNumber = cardNumber, CustomerId = 1, CVV = 123, Token = 1 }));
            }
        }

        [Test]
        [TestCase(123456)]
        public void TestRepositoryShouldSaveCardInvalidCVV(int cvv)
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                var dbCardRepo = new CardRepository(dbcontext);

                Assert.Throws<ValidationException>(() => dbCardRepo.Save(new DBCard() { CardNumber = 1234567812345678, CustomerId = 1, CVV = cvv, Token = 1 }));
            }
        }

        [Test]
        public void TestRepositoryShouldListCards()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                var dbCardRepo = new CardRepository(dbcontext);

                dbCardRepo.Save(new DBCard() { CardNumber = 1234567812345678, CustomerId = 1, CVV = 123, Token = 1 });
                dbCardRepo.Save(new DBCard() { CardNumber = 1234567812345679, CustomerId = 1, CVV = 123, Token = 1 });
                dbCardRepo.Save(new DBCard() { CardNumber = 1234567812345670, CustomerId = 1, CVV = 123, Token = 1 });

                List<DBCard> found = dbCardRepo.List().ToList();

                Assert.AreEqual(3, found.Count());
                Assert.AreEqual(1, found[0].CardId);
                Assert.AreEqual(2, found[1].CardId);
                Assert.AreEqual(3, found[2].CardId);
            }
        }

        [Test]
        public void TestRepositoryShouldGetCardById()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                var dbCardRepo = new CardRepository(dbcontext);

                dbCardRepo.Save(new DBCard() { CardId = 1, CardNumber = 1234567812345678, CustomerId = 1, CVV = 123, Token = 1 });
                dbCardRepo.Save(new DBCard() { CardId = 2, CardNumber = 1234567812345679, CustomerId = 1, CVV = 123, Token = 1 });
                dbCardRepo.Save(new DBCard() { CardId = 3, CardNumber = 1234567812345670, CustomerId = 1, CVV = 123, Token = 1 });

                var found = dbCardRepo.Get(2);

                Assert.AreEqual(2, found.CardId);
                Assert.AreEqual(1234567812345679, found.CardNumber);
                Assert.AreEqual(1, found.CustomerId);
                Assert.AreEqual(123, found.CVV);
            }
        }

        [Test]
        public void TestRepositoryShouldGetCardByIdNotFound()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                var dbCardRepo = new CardRepository(dbcontext);

                Assert.Throws<NotFoundException>(() => dbCardRepo.Get(0));
            }
        }
    }
}
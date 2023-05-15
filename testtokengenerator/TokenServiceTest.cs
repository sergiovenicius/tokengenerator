using common.Exceptions;
using common.Mapper;
using common.Model;
using common.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using testcardregister;
using tokengenerator.Database;
using tokengenerator.Model;
using tokengenerator.Repository;
using tokengenerator.Service;

namespace TestCardRegister
{
    public class TokenServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestTokenServiceShouldValidateSuccessfully()
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

                var logger = new TestLoggerFake<TokenService>();

                TokenService tokenSvc = new TokenService(repo, new MapperDBCardToCardResponse(), logger);

                ValidateInput validateInput = new ValidateInput()
                {
                    CardId = 1,
                    CustomerId = 1,
                    CVV = 123,
                    Token = savedCard.Token
                };

                var validationResult = tokenSvc.ValidateToken(validateInput);

                Assert.AreEqual(true, validationResult);
                Assert.AreEqual(logger.lastLogMessage, input.CardNumber.ToString());
            }
        }

        [Test]
        public void TestTokenServiceShouldValidateExpired()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                CardRepository repo = new CardRepository(dbcontext);

                TokenService tokenSvc = new TokenService(repo, null, null);

                var input = new CardInput()
                {
                    CardNumber = 1234123412341234,
                    CustomerId = 1,
                    CVV = 123
                };

                DBCard dbcard = (new MapperCardInputToDBCard()).Map(input);

                dbcard.RegistrationDate = DateTime.UtcNow.AddMinutes(-31);

                Assert.Throws<NotValidException>(() => tokenSvc.ValidateTokenExpired(dbcard));
            }
        }

        [Test]
        public void TestTokenServiceShouldValidateCustomerInvalid()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                CardRepository repo = new CardRepository(dbcontext);

                TokenService tokenSvc = new TokenService(repo, null, null);

                var input = new CardInput()
                {
                    CardNumber = 1234123412341234,
                    CustomerId = 1,
                    CVV = 123
                };

                DBCard dbcard = (new MapperCardInputToDBCard()).Map(input);

                ValidateInput validateInput = new ValidateInput()
                {
                    CardId = 1,
                    CustomerId = 2,
                    CVV = 123,
                    Token = 0
                };

                Assert.Throws<NotValidException>(() => tokenSvc.ValidateInvalidCustomer(dbcard, validateInput));
            }
        }

        [Test]
        public void TestTokenServiceShouldValidateInvalidToken()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                CardRepository repo = new CardRepository(dbcontext);

                TokenService tokenSvc = new TokenService(repo, null, null);

                var input = new CardInput()
                {
                    CardNumber = 1234123412341234,
                    CustomerId = 1,
                    CVV = 123
                };

                DBCard dbcard = (new MapperCardInputToDBCard()).Map(input);

                ValidateInput validateInput = new ValidateInput()
                {
                    CardId = 1,
                    CustomerId = 2,
                    CVV = 123,
                    Token = 0
                };

                Assert.AreEqual(false, tokenSvc.IsValidToken(dbcard, validateInput));
            }
        }

        [Test]
        public void TestTokenServiceShouldValidateValidToken()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                CardRepository repo = new CardRepository(dbcontext);

                TokenService tokenSvc = new TokenService(repo, null, null);

                var input = new CardInput()
                {
                    CardNumber = 1234123412341234,
                    CustomerId = 1,
                    CVV = 123
                };

                DBCard dbcard = (new MapperCardInputToDBCard()).Map(input);

                ValidateInput validateInput = new ValidateInput()
                {
                    CardId = 1,
                    CustomerId = 2,
                    CVV = 123,
                    Token = Token.GenerateToken(input.CardNumber, input.CVV)
                };

                Assert.AreEqual(true, tokenSvc.IsValidToken(dbcard, validateInput));
            }
        }

        [Test]
        public void TestTokenServiceShouldValidateInvalidCVV()
        {
            using (var dbcontext = new DBCardContext(new DbContextOptionsBuilder<DBCardContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options))
            {
                CardRepository repo = new CardRepository(dbcontext);

                TokenService tokenSvc = new TokenService(repo, null, null);

                var input = new CardInput()
                {
                    CardNumber = 1234123412341234,
                    CustomerId = 1,
                    CVV = 123
                };

                DBCard dbcard = (new MapperCardInputToDBCard()).Map(input);

                ValidateInput validateInput = new ValidateInput()
                {
                    CardId = 1,
                    CustomerId = 2,
                    CVV = 124,
                    Token = Token.GenerateToken(input.CardNumber, 123)
                };

                Assert.AreEqual(false, tokenSvc.IsValidToken(dbcard, validateInput));
            }
        }
    }
}
using common.Exceptions;
using common.Mapper;
using common.Model;
using Microsoft.Extensions.Logging;
using tokengenerator.Database;
using tokengenerator.Model;
using tokengenerator.Repository;
using tokengenerator.Service;

namespace common.Service
{
    public class TokenService : ITokenService
    {
        private readonly ICardRepository _repository;
        private readonly IMapper<CardResponse, DBCard> _mapperCardDBtoCardResponse;
        private readonly ILogger<TokenService> _logger;

        public TokenService(ICardRepository repository,
            IMapper<CardResponse, DBCard> mapperCardDBtoCardResponse,
            ILogger<TokenService> logger)
        {
            _repository = repository;
            _mapperCardDBtoCardResponse = mapperCardDBtoCardResponse;
            _logger = logger;
        }

        public bool ValidateToken(ValidateInput input)
        {
            var dbCard = _repository.Get(input.CardId);

            ValidateTokenExpired(dbCard);

            ValidateInvalidCustomer(dbCard, input);

            _logger.LogInformation($"CARD NUMBER FOR TOKEN {input.Token}");
            _logger.LogInformation($"{dbCard.CardNumber}");

            return IsValidToken(dbCard, input);
        }

        public void ValidateTokenExpired(DBCard card)
        {
            if (card.RegistrationDate < DateTime.UtcNow.AddMinutes(-30))
                throw new NotValidException("Token is not valid");
        }
        public void ValidateInvalidCustomer(DBCard card, ValidateInput input)
        {
            if (card.CustomerId != input.CustomerId)
                throw new NotValidException("Customer is not valid");
        }
        public bool IsValidToken(DBCard card, ValidateInput input)
        {
            return input.Token == Token.GenerateToken(card.CardNumber, input.CVV);
        }
    }
}

using common.Mapper;
using tokengenerator.Database;
using tokengenerator.Model;
using tokengenerator.Repository;

namespace tokengenerator.Service
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _repository;
        private readonly IMapper<DBCard, CardInput> _mapperCardInputToCardDB;
        private readonly IMapper<CardResponse, DBCard> _mapperCardDBtoCardResponse;

        public CardService(ICardRepository repository, IMapper<DBCard, CardInput> mapperCardInputToCardDB,
            IMapper<CardResponse, DBCard> mapperCardDBtoCardResponse)
        {
            _repository = repository;
            _mapperCardInputToCardDB = mapperCardInputToCardDB;
            _mapperCardDBtoCardResponse = mapperCardDBtoCardResponse;
        }

        public IEnumerable<CardResponse> List()
        {
            var list = _repository.List();
            foreach (var dbcard in list)
                yield return _mapperCardDBtoCardResponse.Map(dbcard);
        }

        public CardResponse Get(int id)
        {
            var dbcard = _repository.Get(id);
            return _mapperCardDBtoCardResponse.Map(dbcard);
        }

        public CardResponse Save(CardInput card)
        {
            var inputcard = _mapperCardInputToCardDB.Map(card);
            var dbcard = _repository.Save(inputcard);
            return _mapperCardDBtoCardResponse.Map(dbcard);
        }
    }
}

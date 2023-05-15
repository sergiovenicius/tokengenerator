using tokengenerator.Database;
using tokengenerator.Model;

namespace common.Mapper
{
    public class MapperDBCardToCardResponse : IMapper<CardResponse, DBCard>
    {
        public CardResponse Map(DBCard dbcard)
        {
            return new CardResponse() { CardId = dbcard.CardId, RegistrationDate = dbcard.RegistrationDate, Token = dbcard.Token };
        }
    }
}

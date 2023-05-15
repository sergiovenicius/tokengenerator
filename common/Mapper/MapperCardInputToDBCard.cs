using common.Model;
using tokengenerator.Database;
using tokengenerator.Model;

namespace common.Mapper
{
    public class MapperCardInputToDBCard : IMapper<DBCard, CardInput>
    {
        public DBCard Map(CardInput card)
        {
            return new DBCard()
            {
                CustomerId = card.CustomerId,
                CardNumber = card.CardNumber,
                CVV = card.CVV,
                Token = Token.GenerateToken(card.CardNumber, card.CVV)
            };
        }
    }
}

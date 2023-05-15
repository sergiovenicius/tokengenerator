using tokengenerator.Model;

namespace tokengenerator.Service
{
    public interface ICardService
    {
        public IEnumerable<CardResponse> List();

        public CardResponse Get(int id);

        public CardResponse Save(CardInput card);
    }
}

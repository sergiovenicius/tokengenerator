using tokengenerator.Database;

namespace tokengenerator.Repository
{
    public interface ICardRepository
    {
        public IEnumerable<DBCard> List();

        public DBCard Get(int id);

        public DBCard Save(DBCard card);
    }
}

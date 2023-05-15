using common.Exceptions;
using tokengenerator.Database;

namespace tokengenerator.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly DBCardContext db;
        public CardRepository(DBCardContext db)
        {
            this.db = db;
        }

        public IEnumerable<DBCard> List()
        {
            return db.Card.ToList();
        }

        public DBCard Get(int id)
        {
            var card = db.Card.Find(id);
            if (card == null)
                throw new NotFoundException("Card not found");
            return card;
        }

        public DBCard Save(DBCard card)
        {
            var newCard = db.Card.Add(card);

            db.SaveChanges();

            return newCard.Entity;
        }
    }
}

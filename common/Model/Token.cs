namespace common.Model
{
    public class Token
    {
        private const int LAST_DIGIT_LENGTH = 4;
        public static long GenerateToken(long cardNumber, int cvv)
        {
            if (cardNumber.ToString().Length < LAST_DIGIT_LENGTH)
                throw new Exception("Card number should have at least 4 digits");

            string lastDigits = cardNumber.ToString().GetLast(LAST_DIGIT_LENGTH);

            return Rotate(lastDigits, cvv);
        }

        private static int Rotate(string source, int cvv)
        {
            if (source.Length == 0)
                return 0;

            if (source.Length == 1)
                return Int32.Parse(source);

            int rotatesLast = cvv;

            if (source.Length == LAST_DIGIT_LENGTH)
            {
                rotatesLast = cvv % LAST_DIGIT_LENGTH;
                if (rotatesLast == 0)
                    return Int32.Parse(source);
            }

            List<char> newSource = source.ToList();
            for (int a = 1; a <= rotatesLast; a++)
            {
                char lastDigit = newSource.Last();
                newSource.Remove(newSource.Last());
                newSource.Insert(0, lastDigit);
            }

            return Int32.Parse(String.Join("", newSource));
        }
    }
}

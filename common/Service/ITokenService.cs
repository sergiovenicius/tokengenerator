using tokengenerator.Model;

namespace tokengenerator.Service
{
    public interface ITokenService
    {
        public bool ValidateToken(ValidateInput input);
    }
}

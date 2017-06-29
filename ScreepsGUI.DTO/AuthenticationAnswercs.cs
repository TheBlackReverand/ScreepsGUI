using ScreepsGUI.DTO.Enum;

namespace ScreepsGUI.DTO
{
    public class AuthenticationAnswer
    {
        public bool Success;

        public string Token;

        public AuthenticationErrorType ErrorType;
    }
}
using ScreepsGUI.DTO.Enum;

namespace ScreepsGUI.ClientAPI.DTO
{
    public class AuthenticationAnswer
    {
        public bool Success;

        public string Token;

        public AuthenticationErrorType ErrorType;
    }
}
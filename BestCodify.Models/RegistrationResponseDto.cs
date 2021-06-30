using System.Collections.Generic;

namespace BestCodify.Models
{
    public class RegistrationResponseDto
    {
        public bool IsRegisterationSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}

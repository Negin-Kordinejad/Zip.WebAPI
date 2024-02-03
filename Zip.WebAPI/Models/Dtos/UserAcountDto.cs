using System.Collections.Generic;

namespace Zip.WebAPI.Models.Dto
{
    public class UserAcountDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<AcountDto> Acounts { get; set; }

    }
}

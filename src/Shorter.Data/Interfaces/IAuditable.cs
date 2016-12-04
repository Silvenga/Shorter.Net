using System;

namespace Shorter.Data.Interfaces
{
    public interface IAuditable
    {
        DateTimeOffset? CreatedOn { get; set; }

        DateTimeOffset? ModifiedOn { get; set; }
    }
}
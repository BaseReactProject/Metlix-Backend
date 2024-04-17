
using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContentOutro : Entity<int>
{
    public ContentOutro()
    {
        StartTime = DateTime.MinValue;
        EndTime = DateTime.MinValue;
    }

    public ContentOutro(DateTime startTime, DateTime endTime, int id) : base(id)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public virtual Content? Content { get; set; }
}

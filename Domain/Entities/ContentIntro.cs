
using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContentIntro:Entity<int>
{
    public ContentIntro()
    {
        StartTime = DateTime.MinValue;
        EndTime = DateTime.MinValue;
    }

    public ContentIntro(DateTime startTime, DateTime endTime,int id):base(id)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public virtual Content? Content { get; set; }
}

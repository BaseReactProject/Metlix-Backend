

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContentNotice : Entity<int>
{
    public ContentNotice(){
        ContentId = 0;
        NoticeId=0;

    }

    public ContentNotice(int contentId, int noticeId,int id):base(id)
    {
        NoticeId = noticeId;
        ContentId =contentId ;
    }

    public int ContentId { get; set; }
    public int NoticeId { get; set; }
    public virtual Content? Content { get; set; }
    public virtual Notice? Notice { get; set; }
}

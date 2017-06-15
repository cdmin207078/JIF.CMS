using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Data;

namespace JIF.CMS.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IRepository<Attachment> _attachmentRepository;

        private readonly IWorkContext _workContext;

        public AttachmentService(IRepository<Attachment> attachmentRepository
            , IWorkContext workContext)
        {
            _attachmentRepository = attachmentRepository;
            _workContext = workContext;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Attachment> GetArticles(string q, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public void Insert(Attachment model)
        {
            throw new NotImplementedException();
        }
    }
}

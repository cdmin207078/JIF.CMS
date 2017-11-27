using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Data;
using JIF.CMS.Services.Attachments.Dtos;

namespace JIF.CMS.Services.Attachments
{
    public class AttachmentService : BaseService, IAttachmentService
    {
        private readonly IRepository<SysAdmin> _sysAdminRepository;
        private readonly IRepository<Attachment> _attachmentRepository;

        private readonly IWorkContext _workContext;

        public AttachmentService(IWorkContext workContext
            , IRepository<SysAdmin> sysAdminRepository
            , IRepository<Attachment> attachmentRepository)
        {
            _sysAdminRepository = sysAdminRepository;
            _attachmentRepository = attachmentRepository;

            _workContext = workContext;
        }

        public void Delete(int id)
        {
            var entity = _attachmentRepository.Get(id);
            if (entity != null)
            {
                _attachmentRepository.Delete(entity);
            }
        }

        public IPagedList<AttachmentSearchListOutput> Get(string q, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var query = from a in _attachmentRepository.Table
                        join u in _sysAdminRepository.Table
                        on a.CreateUserId equals u.CreateUserId
                        where a.Name.Contains(q)
                        select new AttachmentSearchListOutput
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CreateTime = a.CreateTime,
                            SavePath = a.SavePath,
                            Size = a.Size,
                            CreateUserName = u.Account
                        };

            return new PagedList<AttachmentSearchListOutput>(query, pageIndex, pageIndex);
        }

        public void Insert(InsertAttachmentInput model)
        {
            var entity = new Attachment
            {
                Name = model.Name,
                SavePath = model.SavePath,
                Size = model.Size,
                CreateTime = DateTime.Now,
                CreateUserId = _workContext.CurrentUser.Id
            };

            _attachmentRepository.Insert(entity);
        }
    }
}

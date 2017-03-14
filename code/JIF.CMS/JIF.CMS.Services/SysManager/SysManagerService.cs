using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core;
using JIF.CMS.Core.Data;
using JIF.CMS.Core.Security;
using System.Security.Cryptography;
using JIF.CMS.Services.SysManager.Dtos;

namespace JIF.CMS.Services.SysManager
{
    public class SysManagerService : ISysManagerService
    {
        private readonly IRepository<SysAdmin> _sysAdminRepository;

        public SysManagerService(IRepository<SysAdmin> sysAdminRepository)
        {
            _sysAdminRepository = sysAdminRepository;
        }

        public void Add(SysAdmin model)
        {
            if (model == null)
            {
                throw new JIFException("信息不能为空");
            }

            if (string.IsNullOrWhiteSpace(model.Password)
                || string.IsNullOrWhiteSpace(model.Email))
            {
                throw new JIFException("信息不完整");
            }

            var exists = _sysAdminRepository.Table.Any(d => model.Email.ToLower().Trim() == d.Email.ToLower().Trim());
            if (exists)
            {
                throw new JIFException("帐号: " + model.Email + ", 已存在");
            }

            var now = DateTime.Now;

            var entity = new SysAdmin
            {
                Email = model.Email,
                CellPhone = model.CellPhone,
                Password = EncyptHelper.Encrypt(MD5.Create(), string.Format("{0}-{1}", model.Password, now.ToString(JIFConsts.DATETIME_NORMAL))),
                Enable = model.Enable,
                CreateTime = now,
                CreateUserId = JIFConsts.SYS_DEFAULTUID
            };

            _sysAdminRepository.Insert(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, SysAdminUpdateBasicInfo model)
        {
            if (model == null)
            {
                throw new JIFException("信息不能为空");
            }

            var entity = _sysAdminRepository.Get(id);
            if (entity == null)
            {
                throw new JIFException("用户不存在");
            }

            entity.Email = model.Email;
            entity.CellPhone = model.CellPhone;
            entity.Enable = model.Enable;

            _sysAdminRepository.Update(entity);

        }
    }
}
